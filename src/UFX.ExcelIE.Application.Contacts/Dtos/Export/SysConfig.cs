using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Dtos.Export
{
    public class SysConfig
    {
        public ExcelEDownLoad ExcelEDownLoad { get; set; }
    }
    public class ExcelEDownLoad
    {
        /// <summary>
        /// 布置方式：0-本地，1-远程
        /// </summary>
        public int DeployType { get; set; } = 0;
        /// <summary>
        /// RemoteUrl
        /// </summary>
        public string RemoteUrl { get; set; }
    }
}
