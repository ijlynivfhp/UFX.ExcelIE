using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Domain.Shared.Const
{
    /// <summary>
    /// ExcelIE常量类
    /// </summary>
    public class ExcelIEConsts
    {
        #region 导出表头常量
        /// <summary>
        /// 表头英文名称
        /// </summary>
        public const string FieldEnName = "FieldEnName";
        /// <summary>
        /// 表头中文名称
        /// </summary>
        public const string FieldChName = "FieldChName";
        /// <summary>
        /// 表头是否隐藏
        /// </summary>
        public const string IsHide = "IsHide";
        #endregion

        /// <summary>
        /// 默认主表别名
        /// </summary>
        public const string MainTableSign = "A";
        /// <summary>
        /// 排序默认主键字段
        /// </summary>
        public const string PrimarkKey = "Id";

        /// <summary>
        /// 导出分页标识
        /// </summary>
        public const string RowNumber = "RowNum";

        /// <summary>
        /// 排序（正序）
        /// </summary>
        public const string SortAsc = "ASC";

        /// <summary>
        /// 排序（倒序）
        /// </summary>
        public const string SortDesc = "DESC";
        /// <summary>
        /// 每次执行读取数据库最大行数
        /// </summary>
        public const int ExecMaxCountPer = 50000;
        /// <summary>
        /// TopCount
        /// </summary>
        public const string TopCount = "TopCount";
        /// <summary>
        /// OrderBy排序列
        /// </summary>
        public const string OrderBy = "OrderBy";
        /// <summary>
        /// WITH中主查询列
        /// </summary>
        public const string SelectSql = "SelectSql";
        /// <summary>
        /// WITH中主查询
        /// </summary>
        public const string MainSql = "MainSql";
        /// <summary>
        /// 处理特殊From表
        /// </summary>
        public const string FromSql = "FromSql";
        /// <summary>
        /// WITH语句
        /// </summary>
        public const string WithSql = @";WITH MainSql AS (#MainSql#) SELECT TOP #TopCount# * FROM MainSql WHERE 1= 1 ";

        #region Sql拼接条件常量
        /// <summary>
        /// 等于
        /// </summary>
        /// 
        public const string Equal = "Equal";
        /// <summary>
        /// 不等于
        /// </summary>
        public const string NotEqual = "NotEqual";
        /// <summary>
        /// 大于
        /// </summary>
        public const string Greater = "Greater";
        /// <summary>
        /// 大于等于
        /// </summary>
        public const string GreaterEqual = "GreaterEqual";
        /// <summary>
        /// 小于
        /// </summary>
        public const string Less = "Less";
        /// <summary>
        /// 小于等于
        /// </summary>
        public const string LessEqual = "LessEqual";
        /// <summary>
        /// In
        /// </summary>
        public const string In = "In";
        /// <summary>
        /// NotIn
        /// </summary>
        public const string NotIn = "NotIn";
        /// <summary>
        /// Like
        /// </summary>
        public const string Like = "Like";
        /// <summary>
        /// NotLike
        /// </summary>
        public const string NotLike = "NotLike";
        /// <summary>
        /// CommonLike
        /// </summary>
        public const string CommonLike = "CommonLike";
        /// <summary>
        /// 以StartWith开始
        /// </summary>
        public const string StartWith = "StartWith";
        /// <summary>
        /// 以EndWith结束
        /// </summary>
        public const string EndWith = "EndWith";
        /// <summary>
        /// Is Null
        /// </summary>
        public const string FitNULL = "FitNULL";
        /// <summary>
        /// Is Not Null
        /// </summary>
        public const string NotFitNULL = "NotFitNULL";
        
        #endregion

        /// <summary>
        /// 页面查询通用查询参数
        /// </summary>
        public const string CommonQuery= "query";

        /// <summary>
        /// 属性类型
        /// </summary>
        public const string PropertitySignName = "List`1";

        #region 导出路径和文件后缀常量
        /// <summary>
        /// 模板,下载根目录
        /// </summary>
        public const string ExcelIEDownUrlSuf = "ExcelIE/";
        /// <summary>
        /// 模板,下载根目录
        /// </summary>
        public const string ExcelIESufStr = "ExcelIE\\";
        /// <summary>
        /// 导出后缀Str
        /// </summary>
        public const string ExportSufStr = "Export\\";
        /// <summary>
        /// 导出后缀Str
        /// </summary>
        public const string TemplateSufStr = "Template\\";
        /// <summary>
        /// Excel后缀
        /// </summary>
        public const string ExcelSufStr = ".xlsx";
        #endregion

        /// <summary>
        /// ExcelIE操作redis前缀
        /// </summary>
        public const string ExcelIERedisPre = "ExcelIE:";
    }
}
