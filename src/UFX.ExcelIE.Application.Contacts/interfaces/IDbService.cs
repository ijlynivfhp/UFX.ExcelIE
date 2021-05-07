using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.Infra.Interfaces;

namespace UFX.ExcelIE.Application.Contracts.interfaces
{
    /// <summary>
    /// 同步数据服务接口
    /// </summary>
    public interface IDbService : IMyScoped
    {
        /// <summary>
        /// 修改数据库连接
        /// </summary>
        /// <param name="tntId"></param>
        /// <returns></returns>
        Task ChangeConnectionString(Guid tntId);
    }
}
