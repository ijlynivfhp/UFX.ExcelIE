using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Enum;
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
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; } = default;
        /// <summary>
        /// 租户DB数据源
        /// </summary>
        public string TenantDBStr { get; set; } = default;
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
        public ExportTypeEnum ExportType { get; set; } = ExportTypeEnum.MagicodesCommon;

        /// <summary>
        /// 是否过滤角色权限
        /// </summary>
        public bool IsFilterRole { get; set; } = false;
        /// <summary>
        /// 替换列（动态列）
        /// </summary>
        public Dictionary<string, string> ReplaceFields { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 处理特殊where条件
        /// </summary>
        public string WhereSql { get; set; }
        /// <summary>
        /// 处理特殊From Sql
        /// </summary>
        public string FromSql { get; set; }

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
        /// 导出写入计时器
        /// </summary>
        public Stopwatch WriteWatch { get; set; } = new Stopwatch();
        /// <summary>
        /// 导出数据查询计时器
        /// </summary>
        public Stopwatch QueryWatch { get; set; } = new Stopwatch();
        /// <summary>
        /// 导出任务计时器
        /// </summary>
        public Stopwatch TaskWatch { get; set; } = new Stopwatch();
        /// <summary>
        /// 本地导出路径
        /// </summary>
        public string LocalUrl { get; set; }
        /// <summary>
        /// 带图片自定义表头对象
        /// </summary>
        public JObject ExportObj { get; set; } = new JObject();
        /// <summary>
        /// 查询语句表列集合
        /// </summary>
        public List<FieldHeads> FieldList { get; set; } = new List<FieldHeads>();
        /// <summary>
        /// 数据是否初始化
        /// </summary>
        public bool IsDataInit { get; set; } = false;
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

    /// <summary>
    /// 构造导出SQL查询字段
    /// </summary>
    public partial class FieldHeads
    {
        public string FieldDbName { get; set; }
        public string FieldEnName { get; set; }
        public string FieldChName { get; set; }
        public int IsHide { get; set; } = 0;
    }

    /// <summary>
    /// ExcelIE缓存清除Dto
    /// </summary>
    public partial class ExcelIECacheDto
    {
        /// <summary>
        /// 清除类型
        /// </summary>
        public ExcelIECacheEnum CacheType { get; set; }
        /// <summary>
        /// TntId
        /// </summary>
        public Guid TenantId { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string TemplateCode { get; set; }
    }
}
