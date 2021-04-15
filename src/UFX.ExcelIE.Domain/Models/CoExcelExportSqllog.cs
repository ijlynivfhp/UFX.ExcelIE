using System;
using System.Collections.Generic;

#nullable disable

namespace UFX.ExcelIE.Domain.Models
{
    public partial class CoExcelExportSqllog
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string TemplateSql { get; set; }
        public string ExportSql { get; set; }
        public string ExportParameters { get; set; }
        public int ExportDuration { get; set; }
        public byte Status { get; set; }
        public string ExportMsg { get; set; }
        public int ExecCount { get; set; }
        public Guid? TntId { get; set; }
        public Guid? CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
