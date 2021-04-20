using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE
{
    public interface IExcelExport : IMyScoped
    {
        /// <summary>
        /// Magicodes.IE模板导出(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileUrl"></param>
        /// <param name="data"></param>
        /// <param name="templateFileUrl"></param>
        /// <returns></returns>
        Task<ExportFileInfo> ExportExcel<T>(string fileUrl, T data, string templateFileUrl) where T : class;
        /// <summary>
        /// Magicodes.IE无模板导出（DataTable）
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="dt"></param>
        /// <param name="maxRowNumberOnASheet"></param>
        /// <returns></returns>
        Task<ExportFileInfo> ExportMultSheetExcel(string fileUrl, DataTable dt, int maxRowNumberOnASheet);
    }
}
