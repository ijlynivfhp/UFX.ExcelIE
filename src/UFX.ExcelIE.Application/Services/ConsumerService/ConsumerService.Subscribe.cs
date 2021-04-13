using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Dtos;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;

namespace UFX.ExcelIE.Application.Services
{
    [CapSubscribe(MqConst.ExcelIETopicName)]
    /// <summary>
    /// 消费者
    /// </summary>
    public partial class ConsumerService : IConsumerService
    {
        public async Task PullMessage(MqMsgDto mqMsgDto)
        {

            string errorMsg = string.Empty;
            _logger.LogInformation("开始导入！");
            try
            {
                await _excelIEService.ExcelExport(mqMsgDto);
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
