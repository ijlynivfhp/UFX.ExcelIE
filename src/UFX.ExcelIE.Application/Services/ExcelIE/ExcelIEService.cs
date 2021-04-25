using AutoMapper;
using DotNetCore.CAP;
using Magicodes.ExporterAndImporter.Core.Models;
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
using UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE;
using UFX.ExcelIE.Domain.Interfaces.ExcelIE;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;
using UFX.ExcelIE.Domain.Shared.Enums;
using UFX.ExcelIE.Domain.Shared.Enums.RabbitMq;
using UFX.Infra.Interfaces;
using static UFX.ExcelIE.Application.Contracts.Helper.ExcelIEHelper;

namespace UFX.ExcelIE.Application.Services.ExcelIE
{
    public class ExcelIEService : IExcelIEService
    {
        private readonly IExcelIEDomainService _excelIEDomainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IExcelExport _iExcelExport;

        public ExcelIEService(IExcelIEDomainService excelIEDomainService, ICapPublisher capPublisher, IExcelExport iExcelExport)
        {
            _excelIEDomainService = excelIEDomainService;
            _capPublisher = capPublisher;
            _iExcelExport = iExcelExport;
        }

        /// <summary>
        /// ExcelIE消息发送与较验
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
                var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.TemplateCode == ieDto.TemplateCode);
                if (template.Id == Guid.Empty)
                    error = "模板不存在！";
                else
                {
                    //消息发送(导出)
                    ieDto.Template = template;
                    ieDto.TemplateLog.Id = _excelIEDomainService.NewGuid();
                    await _capPublisher.PublishAsync(MqConst.ExcelIETopicName, ieDto);
                }
            }
            return error;
        }
        /// <summary>
        /// 具体导出操作
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        public async Task<string> ExcelExport(ExcelIEDto ieDto)
        {
            string exportMsg = string.Empty, downLoadUrl = string.Empty;
            var dataTable = new DataTable();
            var fileInfo = new ExportFileInfo();
            try
            {
                #region 保存路径和模板路径初始化和处理
                var root = Directory.GetCurrentDirectory() + "\\";
                var rootPath = root + ExcelIEConsts.ExcelIESufStr;
                var fileName = ieDto.Template.TemplateName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ExcelIEConsts.ExcelSufStr;
                var excelPath = rootPath + ExcelIEConsts.ExportSufStr;
                var excelFilePath = excelPath + fileName;

                //获取下载路径
                var downLoad = ConfigHelper.GetValue<SysConfig>();
                if (downLoad.ExcelEDownLoad.DeployType == 0)
                    downLoadUrl = ieDto.LocalUrl + ExcelIEConsts.ExcelIEDownUrlSuf;
                else
                {
                    //远程路径
                    //downLoadUrl = ieDto.LocalUrl;
                }

                var excelTemplatePath = rootPath + ExcelIEConsts.TemplateSufStr + ieDto.Template.TemplateName + ExcelIEConsts.ExcelSufStr;
                if (File.Exists(excelFilePath))
                    File.Delete(excelFilePath);
                if (!Directory.Exists(excelPath))
                    Directory.CreateDirectory(excelPath);
                #endregion

                #region 导出记录数据收集
                ieDto.TemplateLog.ParentId = ieDto.Template.Id;
                ieDto.TemplateLog.TemplateSql = ieDto.Template.ExecSql;
                ieDto.TemplateLog.ExportParameters = JsonHelper.ToJsonString(ieDto);
                ieDto.TemplateLog.CreateTime = DateTime.Now;
                ieDto.TemplateLog.TenantId = ieDto.TenantId;
                ieDto.TemplateLog.CreateUserId = ieDto.UserId;
                ieDto.TemplateLog.CreateUser = ieDto.UserName;

                GetSql(ieDto);

                //导入记录新增
                await _excelIEDomainService.EditAsyncExcelLogModel(ieDto.TemplateLog);

                #endregion


                //导出数据收集
                dataTable = await GetDataBySql(ieDto, new DataTable());



                //默认为0Magicodes.IE插件（分sheet导出:默认50000）
                if (ieDto.ExportType == ExportTypeEnum.MagicodesCommon)
                {
                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable, true);
                    //导出数据
                    ieDto.Watch.Start();
                    fileInfo = await _iExcelExport.ExportMultSheetExcel(excelFilePath, dataTable, ieDto.Template.ExecMaxCountPer);
                    ieDto.Watch.Stop();
                }
                //模板导出自定义表头：支持图片
                else if (ieDto.ExportType == ExportTypeEnum.MagicodesImgByTemplate)
                {

                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable);
                    var jarray = JArray.FromObject(dataTable);
                    ieDto.ExportObj.Add(new JProperty("DataList", jarray));

                    //导出数据
                    ieDto.Watch.Start();
                    fileInfo = await _iExcelExport.ExportExcel(excelFilePath, ieDto.ExportObj, excelTemplatePath);
                    ieDto.Watch.Stop();
                }
                else if (ieDto.ExportType == ExportTypeEnum.MiniExcelCommon)
                {

                    //格式DataTable表头
                    FormatterHead(ieDto, dataTable, true);
                    //导出数据
                    ieDto.Watch.Start();
                    MiniExcel.SaveAs(excelFilePath,dataTable);
                    ieDto.Watch.Stop();
                }
                #region 导出记录数据收集保存
                ieDto.TemplateLog.FileSize = CountSize(GetFileSize(excelFilePath));
                ieDto.TemplateLog.Status = 1;
                ieDto.TemplateLog.FileName = fileName;
                ieDto.TemplateLog.DownLoadUrl = downLoadUrl;
                exportMsg = "导出成功：" + ieDto.Watch.Elapsed.TotalSeconds + "秒";
                #endregion
            }
            catch (Exception ex)
            {
                ieDto.TemplateLog.Status = 2;
                exportMsg = "导出失败：" + ex.Message + ":" + ieDto.Watch.Elapsed.TotalSeconds + "秒";
                throw;
            }
            finally
            {
                ieDto.TemplateLog.ExportMsg = exportMsg;
                ieDto.TemplateLog.ModifyTime = DateTime.Now;
                ieDto.TemplateLog.ModifyUser = ieDto.UserName;
                ieDto.TemplateLog.ExportCount = dataTable.Rows.Count;
                ieDto.TemplateLog.ExportDuration = Convert.ToDecimal(ieDto.Watch.Elapsed.TotalSeconds);
                //导入记录更新
                await _excelIEDomainService.EditAsyncExcelLogModel(ieDto.TemplateLog);
            }
            return string.Empty;
        }

        /// <summary>
        /// 分页递归装载数据DataTable
        /// </summary>
        /// <param name="ieDto"></param>
        /// <param name="dt"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public async Task<DataTable> GetDataBySql(ExcelIEDto ieDto, DataTable dt, int rowNum = 0)
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
