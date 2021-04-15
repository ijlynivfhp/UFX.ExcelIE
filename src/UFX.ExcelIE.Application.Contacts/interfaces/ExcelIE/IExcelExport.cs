using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Application.Contracts.interfaces.ExcelIE
{
    public interface IExcelExport : IMyScoped
    {
        Task<ExportFileInfo> ExportExcel<T>(string fileUrl, T data, string templateFileUrl) where T : class;
    }
}
