using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Dtos;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;

namespace UFX.ExcelIE.Application.Services
{
    /// <summary>
    /// 消费者
    /// </summary>
    public partial class ConsumerService : IConsumerService
    {

        [CapSubscribe(MqConst.ExcelIETopicName)]
        public async Task PullMessage(ExcelIEDto ieDto)
        {
            await _excelIEService.ExcelExport(ieDto);
        }
    }
}
