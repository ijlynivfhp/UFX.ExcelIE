using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Shared.Enums.RabbitMq;
using UFX.Infra.Attributes;

namespace UFX.ExcelIE.Application.Contracts.Dtos.Export
{
    /// <summary>
    /// Excel导出判断条件运行符对象
    /// </summary>
    public partial class ExcelIEDto
    {
        /// <summary>
        /// 等于
        /// </summary>
        public List<ExcelIEItemDto> JustEqual { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 大于
        /// </summary>
        public List<ExcelIEItemDto> BigThen { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 大于等于
        /// </summary>
        public List<ExcelIEItemDto> BigEqualThen { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 小于
        /// </summary>
        public List<ExcelIEItemDto> SmallThen { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 小于等于
        /// </summary>
        public List<ExcelIEItemDto> SmallEqualThen { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 单个like条件
        /// </summary>
        public List<ExcelIEItemDto> Justlike { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 多个like条件
        /// </summary>
        public List<ExcelIEItemDto> MultLike { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// In条件
        /// </summary>
        public List<ExcelIEItemDto> MultIn { get; set; } = new List<ExcelIEItemDto>();
        /// <summary>
        /// 租户TntId
        /// </summary>
        public Guid? TntId { get; set; }
        /// <summary>
        /// 操作人Id
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 导出类型
        /// </summary>
        public int ExportType { get; set; } = 0;
    }
    /// <summary>
    /// Excel导出判断条件运行符对象集合
    /// </summary>
    public partial class ExcelIEItemDto
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public List<string> FieldName { get; set; } = new List<string>();
        /// <summary>
        /// 属性值
        /// </summary>
        public List<string> FieldValue { get; set; } = new List<string>();
    }
}
