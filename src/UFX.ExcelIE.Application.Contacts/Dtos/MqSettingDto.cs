using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Dtos
{
    public class MqSettingDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string HostName { get; set; }
    }
}
