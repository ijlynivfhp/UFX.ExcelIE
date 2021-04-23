using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Models;
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
        public List<ExcelEItemDto> Equal { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 不等于
        /// </summary>
        public List<ExcelEItemDto> NotEqual { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 大于
        /// </summary>
        public List<ExcelEItemDto> Greater { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 大于等于
        /// </summary>
        public List<ExcelEItemDto> GreaterEqual { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 小于
        /// </summary>
        public List<ExcelEItemDto> Less { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 小于等于
        /// </summary>
        public List<ExcelEItemDto> LessEqual { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// In
        /// </summary>
        public List<ExcelEItemDto> In { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// NotIn
        /// </summary>
        public List<ExcelEItemDto> NotIn { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// Like
        /// </summary>
        public List<ExcelEItemDto> Like { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// NotLike
        /// </summary>
        public List<ExcelEItemDto> NotLike { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// CommonLike
        /// </summary>
        public List<ExcelEItemDto> CommonLike { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 以StartWith开始
        /// </summary>
        public List<ExcelEItemDto> StartWith { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 以EndWith结束
        /// </summary>
        public List<ExcelEItemDto> EndWith { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// Is Null
        /// </summary>
        public List<ExcelEItemDto> FitNULL { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// Is Not Null
        /// </summary>
        public List<ExcelEItemDto> NotFitNULL { get; set; } = new List<ExcelEItemDto>();
        /// <summary>
        /// 导出模板编码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 租户TntId
        /// </summary>
        public Guid TenantId { get; set; } = default;
        /// <summary>
        /// 操作人Id
        /// </summary>
        public Guid UserId { get; set; } = default;
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 导出类型
        /// </summary>
        public int ExportType { get; set; } = 0;
        /// <summary>
        /// 是否过滤角色权限
        /// </summary>
        public bool IsFilterRole { get; set; } = false;

        #region 扩充字段
        /// <summary>
        /// 消息类型，拉取或推送
        /// </summary>
        public MqMsgType Type { get; set; } = MqMsgType.Push;
        /// <summary>
        /// 导出模板
        /// </summary>
        public CoExcelExportSql Template { get; set; } = new CoExcelExportSql();
        /// <summary>
        /// 导出模板记录
        /// </summary>
        public CoExcelExportSqllog TemplateLog { get; set; } = new CoExcelExportSqllog();
        /// <summary>
        /// 计时器
        /// </summary>
        public Stopwatch Watch { get; set; } = new Stopwatch();
        /// <summary>
        /// 本地导出路径
        /// </summary>
        public string LocalUrl { get; set; }
        #endregion

    }
    /// <summary>
    /// Excel导出判断条件运行符对象集合
    /// </summary>
    public partial class ExcelEItemDto
    {
        /// <summary>
        /// Key:前端别名，Value:后端对应数据库表字段名称
        /// </summary>
        public List<FieldMapDto> FieldName { get; set; } = new List<FieldMapDto>();
        /// <summary>
        /// 属性值
        /// </summary>
        public List<string> FieldValue { get; set; } = new List<string>();
    }

    /// <summary>
    /// 页面参数和后台查询参数关系映射对象
    /// </summary>
    public partial class FieldMapDto
    {
        /// <summary>
        /// 前端接收参数Key名称
        /// </summary>
        public string NeckName { get; set; }
        /// <summary>
        /// 后端对应NickName模板查询条件列名
        /// </summary>
        public string FieldName { get; set; }
    }
}
