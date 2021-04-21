using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        /// <param name="ieDto"></param>
        /// <returns></returns>
        public static void GetSql(ExcelIEDto ieDto)
        {
            string tempName = string.Empty, tempValue = string.Empty, exportSql = string.Empty;
            var mainSql = new StringBuilder(ieDto.TemplateLog.TemplateSql);
            mainSql.Append(" where 1=1 ");
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
                            mainSql.AppendFormat("And {0}='{1}' ", tempName, tempValue);
                        if (listFieldName == ExcelIEConsts.NotEqual)
                            mainSql.AppendFormat("And {0}<>'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.Greater)
                            mainSql.AppendFormat("And {0}>'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.GreaterEqual)
                            mainSql.AppendFormat("And {0}>='{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.Less)
                            mainSql.AppendFormat("And {0}<'{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.LessEqual)
                            mainSql.AppendFormat("And {0}<='{1}' ", tempName, tempValue);
                        else if (listFieldName == ExcelIEConsts.In)
                        {
                            var multInValue = new List<string>();
                            item.FieldValue.ForEach(o =>
                            {
                                multInValue.Add("'" + o + "'");
                            });
                            mainSql.AppendFormat("And {0} In ({1}) ", item.FieldName.First().FieldName, string.Join(',', multInValue.ToArray()));
                        }
                        else if (listFieldName == ExcelIEConsts.NotIn)
                        {
                            var multInValue = new List<string>();
                            item.FieldValue.ForEach(o =>
                            {
                                multInValue.Add("'" + o + "'");
                            });
                            mainSql.AppendLine(string.Format("And {0} Not In ({1}) ", item.FieldName.First().FieldName, string.Join(',', multInValue.ToArray())));
                        }
                        else if (listFieldName == ExcelIEConsts.Like)
                            mainSql.AppendLine(string.Format("And {0} Like '%{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.NotLike)
                            mainSql.AppendLine(string.Format("And {0} Not Like '%{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.CommonLike)
                        {
                            var likeValue = tempValue;
                            mainSql.Append("And ( ");
                            foreach (var like in item.FieldName)
                            {
                                if (item.FieldName.IndexOf(like) == item.FieldName.Count - 1)
                                    mainSql.AppendLine(string.Format("{0} like '%{1}%' ", like.FieldName, likeValue));
                                else
                                {
                                    mainSql.AppendLine(string.Format("{0} like '%{1}%' Or ", like.FieldName, likeValue));
                                }
                            }
                            mainSql.Append(" ) ");
                        }
                        else if (listFieldName == ExcelIEConsts.StartWith)
                            mainSql.AppendLine(string.Format("And {0} Like '{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.EndWith)
                            mainSql.AppendLine(string.Format("And {0} Like '%{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.FitNULL)
                            mainSql.AppendLine(string.Format("And {0} Is Null", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.NotFitNULL)
                            mainSql.AppendLine(string.Format("And {0} Is Not Null ", tempName, tempValue));
                    }
                }
            }
            exportSql = ExcelIEConsts.WithSql
                .Replace($"#{ExcelIEConsts.TopCount}#", (Convert.ToInt32(ieDto.Template.ExecMaxCountPer) > 0 ? ieDto.Template.ExecMaxCountPer : ExcelIEConsts.ExecMaxCountPer).ToString())
                .Replace($"#{ExcelIEConsts.OrderBy}#", string.Format("Order By {0}.{1} {2}", ieDto.Template.MainTableSign, string.IsNullOrEmpty(ieDto.Template.OrderField) ? ExcelIEConsts.PrimarkKey : ieDto.Template.OrderField, Convert.ToBoolean(ieDto.Template.Sort) ? ExcelIEConsts.SortDesc : ExcelIEConsts.SortAsc))
                .Replace($"#{ExcelIEConsts.MainSql}#", mainSql.ToString());
            ieDto.TemplateLog.ExportSql = Regex.Replace(exportSql, @"[\r\n\t]", "");
        }
        /// <summary>
        /// 格式化DataTable表头
        /// </summary>
        /// <param name="headStr"></param>
        /// <param name="dt"></param>
        /// <param name="isEnToCh"></param>
        public static JObject FormatterHead(string headStr, DataTable dt, bool isEnToCh = false)
        {
            JObject obj = new JObject();
            if (dt.Columns.Contains(ExcelIEConsts.PrimarkKey))
                dt.Columns.Remove(ExcelIEConsts.PrimarkKey);
            if (dt.Columns.Contains(ExcelIEConsts.RowNumber))
                dt.Columns.Remove(ExcelIEConsts.RowNumber);
            JArray jarry = JsonHelper.StrToJarry(headStr);
            string fieldEnName = string.Empty, fieldChName = string.Empty, isHide = string.Empty;
            var dictHeads = new Dictionary<string, string>();
            var dictColumns = new List<string>();
            foreach (JObject item in jarry)
            {
                fieldEnName = item[ExcelIEConsts.FieldEnName].ToString().Trim();
                fieldChName = item[ExcelIEConsts.FieldChName].ToString().Trim();
                isHide = Convert.ToString(item[ExcelIEConsts.IsHide]) ?? "0";
                if (!dictHeads.Keys.Contains(fieldEnName) && isHide != "1")
                    dictHeads.Add(fieldEnName, fieldChName);
            }
            foreach (DataColumn item in dt.Columns)
            {
                dictColumns.Add(item.ColumnName);
            }
            var columns = dictHeads.Keys.Intersect(dictColumns);
            foreach (var item in dictColumns)
            {
                if (!columns.Contains(item))
                    dt.Columns.Remove(item);
                else
                {
                    obj.Add(new JProperty(item, dictHeads[item]));
                    if (isEnToCh)
                        dt.Columns[item].ColumnName = dictHeads[item];
                }
            }
            return obj;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="sFullName"></param>
        /// <returns></returns>
        public static long GetFileSize(string sFullName)
        {
            long lSize = 0;
            if (File.Exists(sFullName))
                lSize = new FileInfo(sFullName).Length;
            return lSize;
        }

        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="Size">初始文件大小</param>
        /// <returns></returns>
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }
    }
}
