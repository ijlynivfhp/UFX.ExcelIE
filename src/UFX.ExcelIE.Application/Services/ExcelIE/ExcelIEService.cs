using AutoMapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Common.Domain;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Application.Contracts;
using UFX.ExcelIE.Application.Contracts.Dtos;
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

namespace UFX.ExcelIE.Application.Services.ExcelIE
{
    public class ExcelIEService : IExcelIEService
    {
        private readonly IExcelIEDomainService _excelIEDomainService;
        private readonly ICapPublisher _capPublisher;

        public ExcelIEService(IExcelIEDomainService excelIEDomainService, ICapPublisher capPublisher)
        {
            _excelIEDomainService = excelIEDomainService;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// ExcelIE消息发送与较验
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<string> PushExcelExportMsg(MqMsgDto mqMsgDto)
        {
            string error = string.Empty;
            var dictParams = HttpHelper.ParseToDictionary(mqMsgDto.TemplateParams);
            if (dictParams.Count == 0 || !dictParams.ContainsKey(ExcelIEConsts.TemplateCode))
                error = "模板编码不能为空！";
            else
            {
                mqMsgDto.TemplateCode = dictParams[ExcelIEConsts.TemplateCode].First();
                var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.TemplateCode == mqMsgDto.TemplateCode);
                if (template is null)
                    error = "模板不存在！";
                else
                {
                    #region 导出记录数据
                    var templateLog = new CoExcelExportSqllog();
                    templateLog.Id = _excelIEDomainService.NewGuid();
                    templateLog.ParentId = template.Id;
                    templateLog.TemplateSql = template.ExecSql;
                    templateLog.ExportParameters = mqMsgDto.TemplateParams;
                    templateLog.CreateTime = DateTime.Now;
                    templateLog.CreateUserId = Guid.Empty;
                    if (dictParams.ContainsKey(ExcelIEConsts.UserId))
                        templateLog.CreateUserId = Guid.Parse(dictParams[ExcelIEConsts.UserId].First());
                    templateLog.CreateUser = string.Empty;
                    if (dictParams.ContainsKey(ExcelIEConsts.UserName))
                        templateLog.CreateUser = dictParams[ExcelIEConsts.UserName].First();
                    await _excelIEDomainService.AddAsyncExcelLogModel(templateLog);
                    #endregion

                    #region 导出消息收集
                    mqMsgDto.Id = templateLog.Id;
                    mqMsgDto.TntId = Guid.Empty;
                    if (dictParams.ContainsKey(ExcelIEConsts.TntId))
                        mqMsgDto.TntId = Guid.Parse(dictParams[ExcelIEConsts.TntId].First());
                    #endregion

                    //消息发送(导出)
                    await _capPublisher.PublishAsync(MqConst.ExcelIETopicName, mqMsgDto);
                }
            }
            return error;
        }
        /// <summary>
        /// 具体导出操作
        /// </summary>
        /// <param name="templateLogId"></param>
        /// <returns></returns>
        public async Task<string> ExcelExport(MqMsgDto mqMsgDto)
        {
            string errorMsg = string.Empty;
            var templateLog = await _excelIEDomainService.GetFirstExcelLogModelAsync(o => o.Id == mqMsgDto.Id);
            var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.Id == templateLog.ParentId);
            if (template.Id == Guid.Empty || templateLog.Id == Guid.Empty)
                errorMsg = "导出异常！";
            else
            {
                var dictParams = HttpHelper.ParseToDictionary(templateLog.ExportParameters);
                var execSql = GetSql(template.ExecSql, dictParams);
                var exportList = await _excelIEDomainService.QueryListSqlCommandAsync<dynamic>(execSql);

            }
            return string.Empty;
        }
        private string GetSql(string templateSql, Dictionary<string, List<string>> dictParams)
        {
            StringBuilder sb = new StringBuilder(templateSql);
            sb.Append("where 1=1");
            foreach (var item in dictParams)
            {
                if (item.Key == MqParams.Query)
                {
                    sb.Append(" And ( ");
                    foreach (var nvc in item.Value)
                    {
                        if (item.Value.IndexOf(nvc) == item.Value.Count - 1)
                            sb.AppendFormat(" {0} like '%{1}%' Or ", item.Key, item.Value);
                        else
                        {
                            sb.AppendFormat(" {0} like '%{1}%' ", item.Key, item.Value);
                        }
                    }
                    sb.Append(" ) ");
                }
                else
                {
                    sb.AppendFormat(" {0}='{1}' ", item.Key, item.Value);
                }
            }
            return sb.ToString();
        }
    }
}
