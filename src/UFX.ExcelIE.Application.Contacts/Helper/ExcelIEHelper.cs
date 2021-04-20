using System;
using System.Collections.Generic;
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
                            sb.AppendLine(string.Format("And {0} Not In ({1}) ", item.FieldName.First(), string.Join(',', multInValue.ToArray())));
                        }
                        else if (listFieldName == ExcelIEConsts.Like)
                            sb.AppendLine(string.Format("And {0} Like '%{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.NotLike)
                            sb.AppendLine(string.Format("And {0} Not Like '%{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.CommonLike)
                        {
                            var likeValue = tempValue;
                            sb.Append("And ( ");
                            foreach (var like in item.FieldName)
                            {
                                if (item.FieldName.IndexOf(like) == item.FieldName.Count - 1)
                                    sb.AppendLine(string.Format("{0} like '%{1}%' ", like.FieldName, likeValue));
                                else
                                {
                                    sb.AppendLine(string.Format("{0} like '%{1}%' Or ", like.FieldName, likeValue));
                                }
                            }
                            sb.Append(" ) ");
                        }
                        else if (listFieldName == ExcelIEConsts.StartWith)
                            sb.AppendLine(string.Format("And {0} Like '{1}%' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.EndWith)
                            sb.AppendLine(string.Format("And {0} Like '%{1}' ", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.FitNULL)
                            sb.AppendLine(string.Format("And {0} Is Null", tempName, tempValue));
                        else if (listFieldName == ExcelIEConsts.NotFitNULL)
                            sb.AppendLine(string.Format("And {0} Is Not Null ", tempName, tempValue));
                    }
                }
            }
            ieDto.TemplateLog.ExportSql = Regex.Replace(sb.ToString(), @"[\r\n\t]", "");
            ieDto.TemplateLog.ExportSql = ieDto.TemplateLog.ExportSql.Replace("#TopCount#", (Convert.ToInt32(ieDto.Template.ExecMaxCountPer) > 0 ? ieDto.Template.ExecMaxCountPer : 50000).ToString());
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
