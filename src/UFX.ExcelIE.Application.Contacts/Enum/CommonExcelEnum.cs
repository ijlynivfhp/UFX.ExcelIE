using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Enum
{
    /// <summary>
    /// 导出类型枚举
    /// </summary>
    public enum ExportTypeEnum
    {
        /// <summary>
        /// Magicodes通用类型（DataTable）
        /// </summary>
        MagicodesCommon = 1,
        /// <summary>
        /// Magicodes带图片（需用模板：动态数据）
        /// </summary>
        MagicodesImgByTemplate = 2,
        /// <summary>
        /// MiniExcel通用类型（DataTable）
        /// </summary>
        MiniExcelCommon = 3,
    }
}
