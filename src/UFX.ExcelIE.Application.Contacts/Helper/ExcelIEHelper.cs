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
        public static void GetSql(ExcelIEDto ieDto)
        {
            string tempName = string.Empty, tempValue = string.Empty;
            StringBuilder sb = new StringBuilder(ieDto.TemplateLog.TemplateSql);
            sb.Append(" where 1=1 ");
            var type = typeof(ExcelIEDto);
            var properties = type.GetProperties().Where(o => o.PropertyType.Name == ExcelIEConsts.PropertitySignName).ToList();
            foreach (var propertity in properties)
            {
                var listItem = propertity.GetValue(ieDto, null) as List<ExcelEItemDto>;
                var listFieldName = propertity.Name;
                foreach (var item in listItem)
                {
                    if (item.FieldName.Count > 0 && item.FieldValue.Count > 0)
                    {
                        tempName = item.FieldName.First().FieldName; tempValue = item.FieldValue.First();
                        if (string.IsNullOrEmpty(tempName))
                            continue;
                        if (listFieldName == ExcelIEConsts.Equal)
                            sb.AppendFormat("And {0}='{1}' ", tempName, tempValue);
                        if (listFieldName == ExcelIEConsts.NotEqual)
                            sb.AppendFormat("And {0}<>'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.Greater)
                            sb.AppendFormat("And {0}>'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.GreaterEqual)
                            sb.AppendFormat("And {0}>='{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.Less)
                            sb.AppendFormat("And {0}<'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.LessEqual)
                            sb.AppendFormat("And {0}<='{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.In)
                        {
                            var multInValue = new List<string>();
                            item.FieldValue.ForEach(o =>
                            {
                                multInValue.Add("'" + o + "'");
                            });
                            sb.AppendFormat("And {0} In ({1}) ", item.FieldName.First(), string.Join(',', multInValue.ToArray()));
                        }
                        else if (listFieldName == ExcelIEConsts.NotIn)
                        {
                            var multInValue = new List<string>();
                            item.FieldValue.ForEach(o =>
                            {
                                multInValue.Add("'" + o + "'");
                            });
                            sb.AppendFormat("And {0} Not In ({1}) ", item.FieldName.First(), string.Join(',', multInValue.ToArray()));
                        }
                        else if (listFieldName == ExcelIEConsts.Like)
                            sb.AppendFormat("And {0} Like '%{1}%' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.NotLike)
                            sb.AppendFormat("And {0} Not Like '%{1}%' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.CommonLike)
                        {
                            var likeValue = tempValue;
                            sb.Append("And ( ");
                            foreach (var like in item.FieldName)
                            {
                                if (item.FieldName.IndexOf(like) == item.FieldName.Count - 1)
                                    sb.AppendFormat(" {0} like '%{1}%' ", like.FieldName, likeValue);
                                else
                                {
                                    sb.AppendFormat(" {0} like '%{1}%' Or ", like.FieldName, likeValue);
                                }
                            }
                            sb.Append(" ) ");
                        }
                        else if (listFieldName == ExcelIEConsts.StartWith)
                            sb.AppendFormat("And {0} Like '{1}%' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.EndWith)
                            sb.AppendFormat("And {0} Like '%{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.FitNULL)
                            sb.AppendFormat("And {0} Is Null", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.NotFitNULL)
                            sb.AppendFormat("And {0} Is Not Null ", tempName, tempValue);
                    }
                }
            }
            ieDto.TemplateLog.ExportSql = Regex.Replace(sb.ToString(), @"[\r\n\t]", "");
        }
    }
}
