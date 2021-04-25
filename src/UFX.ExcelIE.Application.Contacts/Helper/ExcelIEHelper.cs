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
    /// <summary>
    /// 
    /// </summary>
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
            StringBuilder selectFields = new StringBuilder(), mainSql = new StringBuilder(ieDto.TemplateLog.TemplateSql);
            if (string.IsNullOrEmpty(ieDto.Template.ExportHead))
                throw new Exception("导出模板列头不能为空！");
            else
            {
                ieDto.FieldList = JsonHelper.ToJson<List<FieldHeads>>(ieDto.Template.ExportHead);
                ieDto.FieldList.ForEach(o =>
                {
                    if (Convert.ToInt32(o.IsHide) == 0)
                        selectFields.AppendLine(string.Format("{0} AS {1},", ieDto.ReplaceFields.Keys.Contains(o.FieldEnName) ? ieDto.ReplaceFields[o.FieldEnName] : o.FieldDbName, o.FieldEnName));
                });
                selectFields.AppendLine("ROW_NUMBER() OVER (ORDER BY A.Id ASC) AS RowNum ");
            }
            mainSql.AppendLine("where 1=1 ");
            var type = typeof(ExcelIEDto);
            var properties = type.GetProperties().Where(o => o.PropertyType.Name == ExcelIEConsts.PropertitySignName).ToList();
            foreach (var propertity in properties)
            {
                var listItem = propertity.GetValue(ieDto, null) as List<ExcelEItemDto>;
                var listFieldName = propertity.Name;
                if (listItem == null)
                    continue;
                foreach (var item in listItem)
                {
                    if (item.FieldName.Count > 0 && item.FieldValue.Count > 0)
                    {
                        tempName = item.FieldName.First().FieldName; tempValue = item.FieldValue.First();
                        if (string.IsNullOrEmpty(tempName))
                            continue;
                        if (listFieldName == ExcelIEConsts.Equal)
                            mainSql.AppendLine(string.Format("And {0}='{1}' ", tempName, tempValue));
                        if (listFieldName == ExcelIEConsts.NotEqual)
                            mainSql.AppendLine(string.Format("And {0}<>'{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.Greater)
                            mainSql.AppendLine(string.Format("And {0}>'{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.GreaterEqual)
                            mainSql.AppendLine(string.Format("And {0}>='{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.Less)
                            mainSql.AppendLine(string.Format("And {0}<'{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.LessEqual)
                            mainSql.AppendLine(string.Format("And {0}<='{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.In)
                        {
                            var multInValue = new List<string>();
                            item.FieldValue.ForEach(o =>
                            {
                                multInValue.Add("'" + o + "'");
                            });
                            mainSql.AppendLine(string.Format("And {0} In ({1}) ", item.FieldName.First().FieldName, string.Join(',', multInValue.ToArray())));
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
                .Replace($"#{ExcelIEConsts.MainSql}#", mainSql.ToString())
                .Replace($"#{ExcelIEConsts.SelectSql}#", selectFields.ToString());
            ieDto.TemplateLog.ExportSql = Regex.Replace(exportSql, @"[\r\n\t]", "");
        }
        /// <summary>
        /// 格式化DataTable表头
        /// </summary>
        /// <param name="ieDto"></param>
        /// <param name="dt"></param>
        /// <param name="isEnToCh"></param>
        public static void FormatterHead(ExcelIEDto ieDto, DataTable dt, bool isEnToCh = false)
        {
            if (dt.Columns.Contains(ExcelIEConsts.PrimarkKey))
                dt.Columns.Remove(ExcelIEConsts.PrimarkKey);
            if (dt.Columns.Contains(ExcelIEConsts.RowNumber))
                dt.Columns.Remove(ExcelIEConsts.RowNumber);
            foreach (var item in ieDto.FieldList)
            {
                if (dt.Columns.Contains(item.FieldEnName))
                {
                    ieDto.ExportObj.Add(new JProperty(item.FieldEnName, item.FieldChName));
                    if (isEnToCh)
                        dt.Columns[item.FieldEnName].ColumnName = item.FieldChName;
                }
            }
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
