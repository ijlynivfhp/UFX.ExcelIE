using AutoMapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Common.Domain;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Application.Contracts;
using UFX.ExcelIE.Application.Contracts.Dtos;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
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
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<string> PushExcelExportMsg(ExcelIEDto ieDto)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(ieDto.TemplateCode))
                error = "模板编码不能为空！";
            else
            {
                var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.TemplateCode == ieDto.TemplateCode);
                if (template is null)
                    error = "模板不存在！";
                else
                {
                    //消息发送(导出)
                    ieDto.Template = template;
                    await _capPublisher.PublishAsync(MqConst.ExcelIETopicName, ieDto);
                }
            }
            return error;
        }
        /// <summary>
        /// 具体导出操作
        /// </summary>
        /// <param name="templateLogId"></param>
        /// <returns></returns>
        public async Task<string> ExcelExport(ExcelIEDto ieDto)
        {
            string errorMsg = string.Empty;
            try
            {
                if (ieDto.Template.Id == Guid.Empty || ieDto.TemplateLog.Id == Guid.Empty)
                    errorMsg = "导出异常！";
                else
                {
                    #region 保存路径和模板路径初始化和处理
                    var root = Directory.GetCurrentDirectory();
                    var rootPath = root + ExcelIEConsts.ExcelIE;
                    var excelPath = rootPath + ExcelIEConsts.Export + (string.IsNullOrEmpty(ieDto.UserName) ? "" : ieDto.UserName + "\\");
                    var excelFilePath = excelPath + ieDto.Template.TemplateName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ExcelIEConsts.ExcelSubStr;
                    var excelTemplatePath = rootPath + ExcelIEConsts.Template + ieDto.Template.TemplateName + ExcelIEConsts.ExcelSubStr;
                    if (File.Exists(excelFilePath))
                        File.Delete(excelFilePath);
                    if (!Directory.Exists(excelPath))
                        Directory.CreateDirectory(excelPath);
                    #endregion

                    #region 导出记录数据收集
                    ieDto.TemplateLog.Id = _excelIEDomainService.NewGuid();
                    ieDto.TemplateLog.ParentId = ieDto.Template.Id;
                    ieDto.TemplateLog.TemplateSql = ieDto.Template.ExecSql;
                    ieDto.TemplateLog.ExportParameters = JsonHelper.ToJsonString(ieDto);
                    ieDto.TemplateLog.CreateTime = DateTime.Now;
                    ieDto.TemplateLog.TntId = ieDto.TntId;
                    ieDto.TemplateLog.CreateUserId = ieDto.UserId;
                    ieDto.TemplateLog.CreateUser = ieDto.UserName;
                    GetSql(ieDto);
                    #endregion

                    //导出数据收集
                    var exportList = await _excelIEDomainService.QueryListSqlCommandAsync<CustomerQuotaManageFlowDto>(ieDto.TemplateLog.ExportSql);

                    //默认为0Magicodes.IE插件
                    if (ieDto.ExportType == 0)
                    {
                        //插件适配数据
                        var jarray = JArray.FromObject(exportList);
                        JObject Jobj = JsonHelper.ToJson<JObject>(ieDto.Template.ExportHead); Jobj.Add(new JProperty("DataList", jarray));

                        //导出数据
                        ieDto.Watch.Start();
                        var fileInfo = await _iExcelExport.ExportExcel(excelFilePath, Jobj, excelTemplatePath);
                        ieDto.Watch.Stop();

                        #region 导出记录数据收集保存
                        ieDto.TemplateLog.Status = 1;
                        ieDto.TemplateLog.ModifyTime = DateTime.Now;
                        ieDto.TemplateLog.ModifyUser = ieDto.UserName;
                        ieDto.TemplateLog.ExecCount += 1;
                        ieDto.TemplateLog.ExportDuration = Convert.ToDecimal(ieDto.Watch.Elapsed.TotalSeconds);
                        ieDto.TemplateLog.DownLoadUrl = excelFilePath;
                        ieDto.TemplateLog.ExportMsg = "导出成功：" + ieDto.Watch.Elapsed.TotalSeconds + "秒";
                        await _excelIEDomainService.AddAsyncExcelLogModel(ieDto.TemplateLog);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ieDto.TemplateLog.ExportMsg = "导出失败：" + ieDto.Watch.Elapsed.TotalSeconds + "秒";
                await _excelIEDomainService.AddAsyncExcelLogModel(ieDto.TemplateLog);
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
