using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Dtos.Export
{
    [ExcelExporter(Name = "CustomerQuotaManageFlow", TableStyle = OfficeOpenXml.Table.TableStyles.Light10, AutoFitAllColumn = true, MaxRowNumberOnASheet = 10000)]
    public class CustomerQuotaManageFlowDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BelongMainId { get; set; }
        public string PayNo { get; set; }
        public string PayAccount { get; set; }
        public DateTime? PayTime { get; set; }
        public Guid? PayType { get; set; }
        public Guid? RevenueAccount { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
