using System;
using System.Collections.Generic;

#nullable disable

namespace UFX.ExcelIE.Domain.Models
{
    public partial class AmTenant
    {
        public Guid Id { get; set; }
        public string TntCode { get; set; }
        public string TntName { get; set; }
        public string TntDesc { get; set; }
        public string TntContact { get; set; }
        public string TntEmail { get; set; }
        public string TntPhone { get; set; }
        public DateTime? TntExpire { get; set; }
        public int? TntUserLimit { get; set; }
        public decimal? TntPrice { get; set; }
        public string TntDbStr { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
