using System;
using System.Collections.Generic;

#nullable disable

namespace UFX.ExcelIE.Domain.Models
{
    public partial class CoExcelExportSql
    {
        public Guid Id { get; set; }
        public byte TemplateType { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateUrl { get; set; }
        public int? SourceType { get; set; }
        public string SourceUrl { get; set; }
        public string ExecSql { get; set; }
        public byte Status { get; set; }
        public bool IsDelete { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
