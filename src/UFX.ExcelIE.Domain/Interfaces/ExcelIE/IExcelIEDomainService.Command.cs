using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Models;

namespace UFX.ExcelIE.Domain.Interfaces.ExcelIE
{
    public partial interface IExcelIEDomainService
    {
        Task EditAsyncExcelLogModel(CoExcelExportSqllog excelLog);
    }
}
