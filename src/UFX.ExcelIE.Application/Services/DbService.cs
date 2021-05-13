using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.ExcelIE.Application.Contracts.Helper;
using UFX.ExcelIE.Application.Contracts.interfaces;
using UFX.ExcelIE.Domain;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.Redis.Interfaces;

namespace UFX.ExcelIE.Application.Services
{
    /// <summary>
    /// 所有平台同步数据操作的基类
    /// </summary>
    public class DbService : IDbService
    {
        private readonly IUnitOfWork<SCMExcelIEContext> _scmUnit;
        private readonly IUnitOfWork<UFX_MASTERContext> _amUnit;
        private readonly IRedisOperation _redis;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="basicDataRepo"></param>
        public DbService(IUnitOfWork<SCMExcelIEContext> scmUnit, IUnitOfWork<UFX_MASTERContext> amUnit, IRedisOperation redis)
        {
            _scmUnit = scmUnit;
            _amUnit = amUnit;
            _redis = redis;
        }

        /// <summary>
        /// 修改数据库连接字符串
        /// 只有saas版需要切换，非saas版读取本地连接
        /// </summary>
        /// <param name="name">方法/key名称</param>
        /// <returns></returns>
        public async Task ChangeConnectionString(ExcelIEDto ieDto)
        {
            if (ieDto.TenantId == Guid.Empty) throw new Exception("输入的tntid错误");
            var tntKey = ExcelIEConsts.ExcelIERedisPre + ieDto.TenantId.ToString();
            var tntStr = await _redis.StringGetAsync(tntKey);
            if (string.IsNullOrEmpty(tntStr))
            {
                var tnt = await _amUnit.GetRepository<AmTenant>().GetFirstOrDefaultAsync(m => m.Id == ieDto.TenantId);
                if (tnt == null) throw new Exception($"未找到对应的租户配置，请检查输入的tntid：{ieDto.TenantId}是否有误");
                if (!string.IsNullOrEmpty(tnt.TntDbStr) && !string.IsNullOrEmpty(tnt.TntCode))
                {
                    tntStr = JsonHelper.ToJsonString(new
                    {
                        tnt.TntCode,
                        tnt.TntDbStr
                    });
                    ieDto.TenantCode = tnt.TntCode;
                    ieDto.TenantDBStr = tnt.TntDbStr;
                    await _redis.StringSetAsync(tntKey, tntStr, TimeSpan.FromMinutes(30));
                    await _scmUnit.ChangeConnectionStringAsync(tnt.TntDbStr);
                }
            }
            else
            {
                var dbDto = JsonHelper.ToJson<dynamic>(tntStr);
                await _scmUnit.ChangeConnectionStringAsync(Convert.ToString(dbDto.TntDbStr));
            }
        }
    }
}
