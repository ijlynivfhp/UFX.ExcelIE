using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;

namespace UFX.ExcelIE.Application.Contracts.interfaces
{
    public partial interface IConsumerService
    {
        /// <summary>
        /// MQ消息接收
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        Task PullMessage(ExcelIEDto ieDto);
    }
}
