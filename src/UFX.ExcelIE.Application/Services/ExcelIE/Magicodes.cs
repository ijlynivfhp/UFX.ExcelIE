using GenFu;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Filters;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE;

namespace UFX.ExcelIE.Application.Services.ExcelIE
{
    public class Magicodes : IExcelExport
    {
        private readonly IExcelExporter _iExcelExporter;
        public Magicodes(IExcelExporter iEcelExporter)
        {
            _iExcelExporter = iEcelExporter;
        }
        /// <summary>
        /// Magicodes.IE模板导出(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileUrl"></param>
        /// <param name="data"></param>
        /// <param name="templateFileUrl"></param>
        /// <returns></returns>
        public async Task<ExportFileInfo> ExportExcel<T>(string fileUrl, T data, string templateFileUrl) where T : class
        {
            return await _iExcelExporter.ExportByTemplate<T>(fileUrl, data, templateFileUrl);
        }
        /// <summary>
        /// Magicodes.IE无模板导出（DataTable）
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="dt"></param>
        /// <param name="templateFileUrl"></param>
        /// <param name="maxRowNumberOnASheet"></param>
        /// <returns></returns>
        public async Task<ExportFileInfo> ExportMultSheetExcel(string fileUrl, DataTable dt, int maxRowNumberOnASheet, IExporterHeaderFilter exporterHeaderFilter = null)
        {
            return await _iExcelExporter.Export(fileUrl, dt, maxRowNumberOnASheet: maxRowNumberOnASheet, exporterHeaderFilter: exporterHeaderFilter);
        }
    }
}
