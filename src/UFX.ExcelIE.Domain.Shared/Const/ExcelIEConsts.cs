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

        public const int ExecMaxCountPer = 50000;

        public const string TopCount = "TopCount";

        public const string OrderBy = "OrderBy";

        public const string MainSql = "MainSql";

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
        /// <summary>
        /// 模板,下载根目录
        /// </summary>
        public const string ExcelIE = "\\ExcelIE\\";
        /// <summary>
        /// 导出后缀Str
        /// </summary>
        public const string Export = "Export\\";
        /// <summary>
        /// 导出后缀Str
        /// </summary>
        public const string Template = "Template\\";
        /// <summary>
        /// Excel后缀
        /// </summary>
        public const string ExcelSubStr = ".xlsx";



    }
}
