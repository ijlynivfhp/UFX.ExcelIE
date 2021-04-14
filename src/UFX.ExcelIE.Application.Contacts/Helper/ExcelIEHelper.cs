using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UFX.ExcelIE.Application.Contracts.Dtos.Export;
using UFX.ExcelIE.Domain.Models;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;

namespace UFX.ExcelIE.Application.Contracts.Helper
{
    public static class ExcelIEHelper
    {
        /// <summary>
        /// 获取导出查询sql
        /// </summary>
        /// <param name="templateLog"></param>
        /// <param name="ieDto"></param>
        /// <returns></returns>
        public static void GetSql(CoExcelExportSqllog templateLog, ExcelIEDto ieDto)
        {
            StringBuilder sb = new StringBuilder(templateLog.TemplateSql);
            sb.Append(" where 1=1 ");
            var type = typeof(ExcelIEDto);
            var properties = type.GetProperties().Where(o => o.PropertyType.Name == ExcelIEConsts.PropertitySignName).ToList();
            foreach (var propertity in properties)
            {
                var listItem = propertity.GetValue(ieDto, null) as List<ExcelIEItemDto>;
                var listFieldName = propertity.Name;
                foreach (var item in listItem)
                {
                    if (item.FieldName.Count > 0 && item.FieldValue.Count > 0 && !string.IsNullOrEmpty(item.FieldName.First()) && !string.IsNullOrEmpty(item.FieldValue.First()))
                    {
                        if (listFieldName == ExcelIEConsts.JustEqual)
                            sb.AppendFormat(" And {0}='{1}' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.BigThen)
                            sb.AppendFormat(" And {0}>'{1}' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.BigEqualThen)
                            sb.AppendFormat(" And {0}>='{1}' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.SmallThen)
                            sb.AppendFormat(" And {0}<'{1}' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.SmallEqualThen)
                            sb.AppendFormat(" And {0}<='{1}' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.Justlike)
                            sb.AppendFormat(" And {0} like '%{1}%' ", item.FieldName.First(), item.FieldValue.First());
                        else if (listFieldName == ExcelIEConsts.MultLike)
                        {
                            var likeValue = item.FieldValue.First();
                            sb.Append("And ( ");
                            foreach (var like in item.FieldName)
                            {
                                if (item.FieldName.IndexOf(like) == item.FieldName.Count - 1)
                                    sb.AppendFormat(" {0} like '%{1}%' ", like, likeValue);
                                else
                                {
                                    sb.AppendFormat(" {0} like '%{1}%' Or ", like, likeValue);
                                }
                            }
                            sb.Append(" ) ");
                        }
                        else if (listFieldName == ExcelIEConsts.MultIn)
                        {
                            item.FieldValue.ForEach(o => { o = "'" + o + "'"; });
                            sb.AppendFormat(" And {0} in '('{1}')' ", item.FieldName.First(), string.Join(',', item.FieldValue.ToArray()));
                        }
                    }
                }
            }
            templateLog.ExportSql = Regex.Replace(sb.ToString(), @"[\r\n\t]", "");
        }
    }
}
