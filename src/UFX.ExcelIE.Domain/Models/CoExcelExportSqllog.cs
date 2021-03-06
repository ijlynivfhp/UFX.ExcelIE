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
        public decimal ExportDurationWrite { get; set; }
        public decimal ExportDurationQuery { get; set; }
        public decimal ExportDurationTask { get; set; }
        public byte Status { get; set; }
        public string ExportMsg { get; set; }
        public string DownLoadUrl { get; set; }
        public byte? DownLoadCount { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public int? ExportCount { get; set; }
        public int ExecCount { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
