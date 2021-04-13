using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Domain.Shared.Const;

namespace UFX.ExcelIE.Application.Services
{
    [CapSubscribe(CommonConst.ExcelIETopicName)]
    /// <summary>
    /// 消费者
    /// </summary>
    public partial class ConsumerService : IConsumerService
    {
      
        async Task IConsumerService.Consumer(string t)
        {
            string errorMsg = string.Empty;
            _logger.LogInformation("开始导入！");
            try
            {
                await _excelIEService.PushExcelExportMsg(t);
                _logger.LogInformation("导入成功！");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
            };
            return;
        }
    }
}
