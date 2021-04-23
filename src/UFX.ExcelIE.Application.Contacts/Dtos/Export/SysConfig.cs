using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Dtos.Export
{
    public class SysConfig
    {
        public ExcelEDownLoad ExcelEDownLoad{ get; set; }
    }
    public class ExcelEDownLoad {
        public string LocalUrl { get; set; }
        public string RemoteUrl { get; set; }
    }
}
