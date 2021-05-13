using Aliyun.OSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Shared.Const;

namespace UFX.ExcelIE.Application.Contracts.Dtos
{
    public class OssConfig
    {
        public string BucketName { get; set; } = "error";
        public string Endpoint { get; set; } = "error";
        public string AccessKeyId { get; set; } = "error.com";
        public string AccessKeySecret { get; set; } = "error";
        public ClientConfiguration ClientConfiguration { get; set; } = new ClientConfiguration();
    }
}
