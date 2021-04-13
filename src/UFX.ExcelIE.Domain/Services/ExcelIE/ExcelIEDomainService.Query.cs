﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Models;

namespace UFX.ExcelIE.Domain.Services.ExcelIE
{
    public partial class ExcelIEDomainService
    {
        public async Task<CoExcelExportSql> GetFirstExcelModelAsync(Expression<Func<CoExcelExportSql, bool>> filter)
        {
            return await _scmUnit.GetRepository<CoExcelExportSql>().GetFirstOrDefaultAsync(filter) ?? new CoExcelExportSql();
        }
        public async Task<CoExcelExportSqllog> GetFirstExcelLogModelAsync(Expression<Func<CoExcelExportSqllog, bool>> filter)
        {
            return await _scmUnit.GetRepository<CoExcelExportSqllog>().GetFirstOrDefaultAsync(filter) ?? new CoExcelExportSqllog();
        }

        public async Task<List<T>> QueryListSqlCommandAsync<T>(string sql, params object[] parameters) where T : class, new()
        {
            return await _scmUnit.QueryListSqlCommandAsync<T>(sql, parameters);
        }
    }
}
