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
        public async Task EditAsyncExcelLogModel(CoExcelExportSqllog excelLog)
        {
            var existExcelLog = await _scmUnit.GetRepository<CoExcelExportSqllog>().GetFirstOrDefaultAsync(o => o.Id == excelLog.Id) ?? new CoExcelExportSqllog();
            excelLog.ExecCount = existExcelLog.ExecCount + 1;
            if (existExcelLog.Id == Guid.Empty)
                await _scmUnit.GetRepository<CoExcelExportSqllog>().InsertAsync(excelLog);
            else
            {
                await _scmUnit.GetRepository<CoExcelExportSqllog>().UpdateAsync(excelLog);
            }
            await _scmUnit.SaveChangesAsync();
        }
    }
}
