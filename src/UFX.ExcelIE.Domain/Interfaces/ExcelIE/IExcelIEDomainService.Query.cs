using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Models;

namespace UFX.ExcelIE.Domain.Interfaces.ExcelIE
{
    public partial interface IExcelIEDomainService
    {
        Task<CoExcelExportSql> GetFirstExcelModelAsync(Expression<Func<CoExcelExportSql, bool>> filter);
        Task<CoExcelExportSqllog> GetFirstExcelLogModelAsync(Expression<Func<CoExcelExportSqllog, bool>> filter);
        Task<List<T>> QueryListSqlCommandAsync<T>(string sql, params object[] parameters) where T : class, new();
    }
}
