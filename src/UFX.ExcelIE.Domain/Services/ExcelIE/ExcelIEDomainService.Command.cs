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

        public async Task EditAsyncExcelLogModel(CoExcelExportSqllog excelLog, bool isDelExpire = false)
        {
            await _scmUnit.GetRepository<CoExcelExportSqllog>().UpdateAsync(excelLog);
            if (isDelExpire)
            {
                var sql = string.Format(@"UPDATE CO_ExcelExportSQLLog
                                SET Status = 2,
                                    ExportMsg = '导出失败：任务超时自动处理'
                                WHERE Status = 0
                                        AND CreateTime < DATEADD(MINUTE, -30, GETDATE())
                                        AND Id <> '{0}'", excelLog.Id);
                await _scmUnit.ExecuteSqlRawAsync(sql);
            }
            await _scmUnit.SaveChangesAsync();
        }
    }
}
