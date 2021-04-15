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
                    #region 导出记录数据
                    var templateLog = new CoExcelExportSqllog();
                    templateLog.Id = _excelIEDomainService.NewGuid();
                    templateLog.ParentId = template.Id;
                    templateLog.TemplateSql = template.ExecSql;
                    templateLog.ExportParameters = JsonHelper.ToJsonString(ieDto);
                    templateLog.CreateTime = DateTime.Now;
                    templateLog.CreateUserId = ieDto.UserId;
                    templateLog.CreateUser = ieDto.UserName;
                    GetSql(templateLog, ieDto);
                    await _excelIEDomainService.AddAsyncExcelLogModel(templateLog);
                    #endregion

                    #region 导出消息收集
                    ieDto.Template = template;
                    ieDto.TemplateLog = templateLog;
                    #endregion

                    //消息发送(导出)
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
                    var exportList = await _excelIEDomainService.QueryListSqlCommandAsync<CustomerQuotaManageFlowDto>(ieDto.TemplateLog.ExportSql);
                    var excelRootPath = Path.Combine(Directory.GetCurrentDirectory(), "\\ExcelIE\\");
                    var excelFilePath = Path.Combine(excelRootPath, "Export\\", string.IsNullOrEmpty(ieDto.UserName) ? "" : ieDto.UserName, ieDto.Template.TemplateName, DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".xlxs");
                    var excelTemplatePath = Path.Combine(excelRootPath, "Template\\" + ieDto.Template.TemplateName + ".xlxs");
                    if (File.Exists(excelFilePath))
                        File.Delete(excelFilePath);
                    if (ieDto.ExportType == 0)
                    {
                        JObject Jobj = JsonHelper.ToJson<JObject>(ieDto.UserName); Jobj.Add(new JProperty("DataList", exportList));
                        var fileInfo = await _iExcelExport.ExportExcel(excelFilePath, Jobj, excelTemplatePath);
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            return string.Empty;
        }
    }
}
