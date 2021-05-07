using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Domain;
using UFX.ExcelIE.Domain.Models;

namespace UFX.ExcelIE.Application.Services
{
    /// <summary>
    /// 所有平台同步数据操作的基类
    /// </summary>
    public class DbService : IDbService
    {
        private readonly IUnitOfWork<SCMExcelIEContext> _scmUnit;
        private readonly IUnitOfWork<UFX_MASTERContext> _amUnit;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="basicDataRepo"></param>
        public DbService(IUnitOfWork<SCMExcelIEContext> scmUnit, IUnitOfWork<UFX_MASTERContext> amUnit)
        {
            _scmUnit = scmUnit;
            _amUnit = amUnit;
        }

        /// <summary>
        /// 修改数据库连接字符串
        /// 只有saas版需要切换，非saas版读取本地连接
        /// </summary>
        /// <param name="name">方法/key名称</param>
        /// <returns></returns>
        public async Task ChangeConnectionString(Guid tntId)
        {
            if (tntId == Guid.Empty) throw new Exception("输入的tntid错误");
            var tnt = await _amUnit.GetRepository<AmTenant>().GetFirstOrDefaultAsync(m => m.Id == tntId);
            if (tnt == null) throw new Exception($"未找到对应的租户配置，请检查输入的tntid：{tntId}是否有误");
            if (!string.IsNullOrEmpty(tnt.TntDbStr))
                await _scmUnit.ChangeConnectionStringAsync(tnt.TntDbStr);
        }
    }
}
