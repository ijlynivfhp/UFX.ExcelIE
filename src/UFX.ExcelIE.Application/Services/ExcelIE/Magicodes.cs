using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE;

namespace UFX.ExcelIE.Application.Services.ExcelIE
{
    public class Magicodes : IExcelExport
    {
        private readonly IExcelExporter _iExcelExporter;
        public Magicodes(IExcelExporter iEcelExporter) {
            _iExcelExporter = iEcelExporter;
        }
        public async Task<ExportFileInfo> ExportExcel<T>(string fileUrl, T data, string templateFileUrl) where T : class
        {
            return await _iExcelExporter.ExportByTemplate<T>(fileUrl, data, templateFileUrl);
        }
    }
}
