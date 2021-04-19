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
        #region Sql拼接条件常量
        /// <summary>
        /// 等于
        /// </summary>
        public const string JustEqual = "JustEqual";
        /// <summary>
        /// 大于
        /// </summary>
        public const string BigThen = "BigThen";
        /// <summary>
        /// 大于等于
        /// </summary>
        public const string BigEqualThen = "BigEqualThen";
        /// <summary>
        /// 小于
        /// </summary>
        public const string SmallThen = "SmallThen";
        /// <summary>
        /// 小于等于
        /// </summary>
        public const string SmallEqualThen = "SmallEqualThen";
        /// <summary>
        /// Like
        /// </summary>
        public const string Justlike = "Justlike";
        /// <summary>
        /// 公用Like
        /// </summary>
        public const string MultLike = "MultLike";
        /// <summary>
        /// In条件
        /// </summary>
        public const string MultIn = "MultIn";
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
