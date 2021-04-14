﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE
{
    /// <summary>
    /// ExcelIE操作类
    /// </summary>
    public interface IExcelIEService : IMyScoped
    {
        /// <summary>
        /// ExcelIE消息发送
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        Task<string> PushExcelExportMsg(ExcelIEDto ieDto);
        /// <summary>
        /// 具体导出操作
        /// </summary>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        Task<string> ExcelExport(ExcelIEDto ieDto);
    }
}
