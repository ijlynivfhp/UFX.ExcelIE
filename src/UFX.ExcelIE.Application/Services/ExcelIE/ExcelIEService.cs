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
using UFX.ExcelIE.Domain.Interfaces.ExcelIE;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Enums;
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
        /// 导出初步较验与导出信息发送
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<string> PushExcelExportMsg(string queryString = "")
        {
            string error = string.Empty;
            var dictParams = HttpHelper.ParseToDictionary(queryString);
            if (dictParams.Count == 0 || !dictParams.Keys.Contains("templateCode"))
                error = "模板编码不能为空！";
            else
            {
                string templateCode = dictParams["templateCode"].First();
                var template = await _excelIEDomainService.GetFirstExcelModelAsync(o => o.TemplateCode == templateCode);
                if (template is null)
                    error = "模板不存在！";
                else
                {
                    var templateLog = new CoExcelExportSqllog()
                    {
                        Id = _excelIEDomainService.NewGuid(),
                        ParentId = template.Id,
                        TemplateSql = template.ExecSql,
                        ExportParameters = queryString,
                        CreateUserId = string.IsNullOrEmpty(dictParams["userId"].FirstOrDefault()) ? Guid.Empty : Guid.Parse(dictParams["userId"].FirstOrDefault()),
                        CreateTime = DateTime.Now,
                        CreateUser = dictParams["userName"].FirstOrDefault()
                    };
                    await _excelIEDomainService.AddAsyncExcelLogModel(templateLog);
                    var mqMsgDto = new MqMsgDto()
                    {
                        Id = templateLog.Id,
                        Type = Domain.Shared.Enums.MqMsgType.Pull,
                        TntId = string.IsNullOrEmpty(dictParams["tntId"].FirstOrDefault()) ? Guid.Empty : Guid.Parse(dictParams["tntId"].FirstOrDefault())
                    };
                    await _capPublisher.PublishAsync(CommonConst.ExcelIETopicName, mqMsgDto);
                }
            }
            return error;
        }

        public async Task<string> ExcelExport(Guid templateLogId)
        {
            string errorMsg = string.Empty;
            var templateLog = await _excelIEDomainService.GetFirstExcelLogModelAsync(o => o.Id == templateLogId);
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
                if (item.Key == QueryParams.Query)
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
