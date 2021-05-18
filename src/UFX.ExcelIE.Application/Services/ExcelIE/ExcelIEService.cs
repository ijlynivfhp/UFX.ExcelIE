using Aliyun.OSS;
using AutoMapper;
using DotNetCore.CAP;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UFX.Common.Domain;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Application.Contracts;
using UFX.ExcelIE.Application.Contracts.Dtos;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.ExcelIE.Application.Contracts.Enum;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Application.Contracts.interfaces.IExcelIE;
using UFX.ExcelIE.Domain.Interfaces.ExcelIE;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;
using UFX.ExcelIE.Domain.Shared.Enums;
using UFX.ExcelIE.Domain.Shared.Enums.RabbitMq;
using UFX.Infra.Interfaces;
using UFX.Redis.Interfaces;
using static UFX.ExcelIE.Application.Contracts.Helper.ExcelIEHelper;

namespace UFX.ExcelIE.Application.Services.ExcelIE
{
    public class ExcelIEService : IExcelIEService
    {
        private readonly IDbService _dbService;
        private readonly IExcelIEDomainService _excelIEDomainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IRedisOperation _redis;
        private readonly IExcelExporter _iExcelExporter;
        private readonly IOss _oss;
        private readonly ILogger<ExcelIEService> _logger;

        public ExcelIEService(IDbService dbService, IExcelIEDomainService excelIEDomainService, ICapPublisher capPublisher, IRedisOperation redis, IExcelExporter IExcelExporter, IOss oss, ILogger<ExcelIEService> logger)
        {
            _dbService = dbService;
            _excelIEDomainService = excelIEDomainService;
            _capPublisher = capPublisher;
            _redis = redis;
            _iExcelExporter = IExcelExporter;
            _oss = oss;
            _logger = logger;
        }

        /// <summary>
        /// 消息发送：ExcelIE消息发送与较验
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        public async Task<string> PushExcelExportMsg(ExcelIEDto ieDto)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(ieDto.TemplateCode))
                error = "模板编码不能为空！";
            else
            {
                // 切换数据库连接
                await _dbService.ChangeConnectionString(ieDto);

                var templateRedisKey = ExcelIEConsts.ExcelIERedisPre + ieDto.TemplateCode + ":" + ieDto.TenantId.ToString();
                var templateStr = await _redis.StringGetAsync(templateRedisKey);
                if (string.IsNullOrEmpty(templateStr))
                {
                    var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.TemplateCode == ieDto.TemplateCode);
                    if (template.Id == Guid.Empty)
                        return error = "模板不存在！";
                    else
                    {
                        await _redis.StringSetAsync(templateRedisKey, JsonHelper.ToJsonString(template), TimeSpan.FromMinutes(50));
                        ieDto.Template = template;
                    }
                }
                else
                {
                    ieDto.Template = JsonHelper.ToJson<CoExcelExportSql>(templateStr);
                }
                //消息发送(导出)
                ieDto.TemplateLog.ExportParameters = JsonHelper.ToJsonString(ieDto);
                ieDto.TemplateLog.ParentId = ieDto.Template.Id;
                ieDto.TemplateLog.TemplateSql = ieDto.Template.ExecSql;
                ieDto.TemplateLog.CreateTime = DateTime.Now;
                ieDto.TemplateLog.TenantId = ieDto.TenantId;
                ieDto.TemplateLog.CreateUserId = ieDto.UserId;
                ieDto.TemplateLog.CreateUser = ieDto.UserName;
                ieDto.TemplateLog.Id = _excelIEDomainService.NewGuid();
                await _excelIEDomainService.AddAsyncExcelLogModel(ieDto.TemplateLog);
                await _capPublisher.PublishAsync(MqConst.ExcelIETopicName, ieDto);
            }
            return error;
        }
        [CapSubscribe(MqConst.ExcelIETopicName)]
        /// <summary>
        /// 消息消费者：具体导出操作
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        public async Task<string> ExcelExport(ExcelIEDto ieDto)
        {
            string downLoadUrl = string.Empty, excelFilePath = string.Empty;
            var dataTable = new DataTable();
            var fileInfo = new ExportFileInfo();
            //获取配置信息
            var sysConfig = ConfigHelper.GetValue<SysConfig>();
            var ossConfig = ConfigHelper.GetValue<OssConfig>();
            ieDto.TaskWatch.Start();
            try
            {
                //切换数据库连接
                await _dbService.ChangeConnectionString(ieDto);

                _logger.LogInformation("开始导入！");
                #region 保存路径和模板路径初始化和处理
                var root = Directory.GetCurrentDirectory() + "\\";
                var rootPath = root + ExcelIEConsts.ExcelIESufStr;
                var fileName = ieDto.Template.TemplateName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ExcelIEConsts.ExcelSufStr;
                var excelPath = rootPath + ExcelIEConsts.ExportSufStr;
                excelFilePath = excelPath + fileName;

                //模板文件路径
                var excelTemplatePath = rootPath + ExcelIEConsts.TemplateSufStr + ieDto.Template.TemplateName + ExcelIEConsts.ExcelSufStr;
                //拷备模板路径
                var excelTemplateCopyPath = rootPath + ExcelIEConsts.TemplateSufStr + fileName;
                //导出前删除文件
                if (File.Exists(excelFilePath))
                    File.Delete(excelFilePath);
                //创建文件夹
                if (!Directory.Exists(excelPath))
                    Directory.CreateDirectory(excelPath);
                #endregion

                #region 导出记录数据收集
                var templateLog = await _excelIEDomainService.GetFirstExcelLogModelAsync(o => o.Id == ieDto.TemplateLog.Id);
                if (templateLog.Id != Guid.Empty)
                    ieDto.TemplateLog = templateLog;
                ieDto.TemplateLog.ExecCount++;
                if (ieDto.TemplateLog.ExecCount >= 3)
                    ieDto.TemplateLog.Status = 2;
                #endregion

                //构造导出Sql语句
                GetExportSql(ieDto);

                //收集导出数据
                ieDto.QueryWatch.Start();
                dataTable = await GetDataBySql(ieDto, new DataTable());
                ieDto.QueryWatch.Stop();
                #region 导出excel数据
                //默认为0Magicodes.IE插件（分sheet导出:默认10000）
                if (ieDto.Template.TemplateType > 0)
                    ieDto.ExportType = (ExportTypeEnum)ieDto.Template.TemplateType;
                if (ieDto.ExportType == ExportTypeEnum.MagicodesCommon)
                {
                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable, true);
                    //导出数据
                    ieDto.WriteWatch.Start();
                    fileInfo = await _iExcelExporter.Export(excelFilePath, dataTable, maxRowNumberOnASheet: ieDto.Template.ExecMaxCountPer);
                    ieDto.WriteWatch.Stop();
                }
                //模板导出自定义表头：支持图片
                else if (ieDto.ExportType == ExportTypeEnum.MagicodesImgByTemplate)
                {

                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable);
                    var jarray = JArray.FromObject(dataTable);
                    if (!ieDto.ExportObj.ContainsKey("DataList"))
                        ieDto.ExportObj.Add(new JProperty("DataList", jarray));

                    //复制模板，解决资源共享占用问题
                    if (File.Exists(excelTemplatePath))//必须判断要复制的文件是否存在
                    {
                        File.Copy(excelTemplatePath, excelTemplateCopyPath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                    }
                    //导出数据
                    ieDto.WriteWatch.Start();
                    fileInfo = await _iExcelExporter.ExportByTemplate<JObject>(excelFilePath, ieDto.ExportObj, excelTemplateCopyPath);
                    ieDto.WriteWatch.Stop();
                    //删除拷备模板文件
                    if (File.Exists(excelTemplateCopyPath))
                        File.Delete(excelTemplateCopyPath);
                }
                //MiniExcel导出
                else if (ieDto.ExportType == ExportTypeEnum.MiniExcelCommon)
                {

                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable, true);
                    //导出数据
                    ieDto.WriteWatch.Start();
                    MiniExcel.SaveAs(excelFilePath, dataTable);
                    ieDto.WriteWatch.Stop();
                }
                //判断是本地还是远程部署
                if (sysConfig.ExcelEDownLoad.DeployType == 0)
                    downLoadUrl = ieDto.LocalUrl + ExcelIEConsts.ExcelIEDownUrlSuf;
                else
                {
                    string ossFilePathName = string.Format(@"{0}{1}",
                        string.IsNullOrEmpty(sysConfig.ExcelEDownLoad.UrlSuf) ? (ExcelIEConsts.ExcelIEDownUrlSuf + ieDto.TenantCode + "/") : sysConfig.ExcelEDownLoad.UrlSuf,
                        fileName);
                    downLoadUrl = string.Format(@"https://{0}.{1}/{2}",
                        ossConfig.BucketName, ossConfig.Endpoint, string.IsNullOrEmpty(sysConfig.ExcelEDownLoad.UrlSuf) ? (ExcelIEConsts.ExcelIEDownUrlSuf + ieDto.TenantCode + "/") : sysConfig.ExcelEDownLoad.UrlSuf);
                    var ossResult = _oss.PutObject(ossConfig.BucketName, ossFilePathName, excelFilePath);
                    if (File.Exists(excelFilePath))
                        File.Delete(excelFilePath);
                }
                #endregion

                #region 导出记录数据收集保存
                ieDto.TemplateLog.FileSize = CountSize(GetFileSize(excelFilePath));
                ieDto.TemplateLog.Status = 1;
                ieDto.TemplateLog.FileName = fileName;
                ieDto.TemplateLog.DownLoadUrl = downLoadUrl;
                #endregion

                _logger.LogInformation("导入成功！");
            }
            catch (Exception ex)
            {
                ieDto.TemplateLog.Status = 2;
                ieDto.TemplateLog.ExportMsg = "导出失败：" + ex.Message + ":";
                _logger.LogError("导入失败：" + ex.Message);
                throw;
            }
            finally
            {
                ieDto.TemplateLog.ModifyTime = DateTime.Now;
                ieDto.TemplateLog.ModifyUser = ieDto.UserName;
                ieDto.TemplateLog.ExportCount = dataTable.Rows.Count;
                if (sysConfig.ExcelEDownLoad.DeployType != 0 && File.Exists(excelFilePath))
                    File.Delete(excelFilePath);
                ieDto.TaskWatch.Stop();
                ieDto.TemplateLog.ExportDurationTask = Convert.ToDecimal(ieDto.TaskWatch.Elapsed.TotalSeconds);
                ieDto.TemplateLog.ExportDurationQuery = Convert.ToDecimal(ieDto.QueryWatch.Elapsed.TotalSeconds);
                ieDto.TemplateLog.ExportDurationWrite = Convert.ToDecimal(ieDto.WriteWatch.Elapsed.TotalSeconds);
                if (ieDto.TemplateLog.Status == 1)
                    ieDto.TemplateLog.ExportMsg = "导出成功：" + ieDto.TaskWatch.Elapsed.TotalSeconds.ToString("0.00") + "秒";
                if (ieDto.TemplateLog.Status == 2)
                    ieDto.TemplateLog.ExportMsg += ieDto.TaskWatch.Elapsed.TotalSeconds.ToString("0.00") + "秒";
                await _excelIEDomainService.EditAsyncExcelLogModel(ieDto.TemplateLog, true);
            }
            return string.Empty;
        }

        /// <summary>
        /// 清除相关缓存：数据库链接串-模板
        /// </summary>
        /// <returns></returns>
        public async Task ClearExcelIECache(ExcelIECacheDto excelIECacheDto)
        {
            try
            {
                if (excelIECacheDto.CacheType == ExcelIECacheEnum.DbSource)
                    await _redis.KeyRemoveAsync(ExcelIEConsts.ExcelIERedisPre + excelIECacheDto.TenantId.ToString());
                else if (excelIECacheDto.CacheType == ExcelIECacheEnum.Template)
                {
                    await _redis.KeyRemoveAsync(ExcelIEConsts.ExcelIERedisPre + excelIECacheDto.TemplateCode + ":" + excelIECacheDto.TenantId.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("清除相关缓存：" + JsonHelper.ToJsonString(excelIECacheDto) + ":" + ex.Message);
            }
        }

        /// <summary>
        /// 分页递归装载数据DataTable
        /// </summary>
        /// <param name="ieDto"></param>
        /// <param name="dt"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        private async Task<DataTable> GetDataBySql(ExcelIEDto ieDto, DataTable dt, int rowNum = 0)
        {
            string execSql = ieDto.TemplateLog.ExportSql + string.Format("And {0} > {1}", ExcelIEConsts.RowNumber, rowNum);
            var dtItem = await _excelIEDomainService.GetDataTableBySqlAsync(execSql);
            dt.Merge(dtItem);
            if (dtItem.Rows.Count == ieDto.Template.ExecMaxCountPer)
            {
                var tempDt = await GetDataBySql(ieDto, dt, Convert.ToInt32(dt.AsEnumerable().Last<DataRow>()[ExcelIEConsts.RowNumber]));
                dt.Merge(tempDt);
            }
            return dt;
        }
    }
}
