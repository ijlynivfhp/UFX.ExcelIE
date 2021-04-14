using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Models;

namespace UFX.ExcelIE.Domain.Services.ExcelIE
{
    public partial class ExcelIEDomainService
    {
        public async Task AddAsyncExcelLogModel(CoExcelExportSqllog excelLog)
        {
            await _scmUnit.GetRepository<CoExcelExportSqllog>().InsertAsync(excelLog);
            await _scmUnit.SaveChangesAsync();
        }
    }
}
