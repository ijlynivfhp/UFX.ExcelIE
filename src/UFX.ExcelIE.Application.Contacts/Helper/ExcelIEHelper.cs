using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain.Shared.Const;
using UFX.ExcelIE.Domain.Shared.Const.RabbitMq;

namespace UFX.ExcelIE.Application.Contracts.Helper
{
    public static class ExcelIEHelper
    {
        /// <summary>
        /// 获取导出查询sql
        /// </summary>
        /// <param name="templateSql"></param>
        /// <param name="dictParams"></param>
        /// <returns></returns>
        public static string GetSql(string templateSql, Dictionary<string, List<string>> dictParams)
        {


            var query = dictParams.ContainsKey(MqParams.Query) ? dictParams[MqParams.Query].First() : "";
            var queryFields = dictParams.ContainsKey(MqParams.Fields) ? dictParams[MqParams.Fields] : new List<string>();

            //删除无用参数
            RemoveDictParamsByKeys(dictParams);

            StringBuilder sb = new StringBuilder(templateSql);
            sb.Append(" where 1=1 ");
            foreach (var item in dictParams)
            {
                sb.AppendFormat("And {0}='{1}' ", item.Key, item.Value.First());
            }
            if (!string.IsNullOrEmpty(query) && queryFields.Count > 0)
            {
                sb.Append("And ( ");
                foreach (var field in queryFields)
                {
                    if (queryFields.IndexOf(field) == queryFields.Count - 1)
                        sb.AppendFormat(" {0} like '%{1}%' ", field, query);
                    else
                    {
                        sb.AppendFormat(" {0} like '%{1}%' Or ", field, query);
                    }
                }
                sb.Append(" ) ");
            }
            return Regex.Replace(sb.ToString(), @"[\r\n\t]", "");
        }

        /// <summary>
        /// 构造sql时删除无用的参数
        /// </summary>
        /// <param name="dictParams"></param>
        public static void RemoveDictParamsByKeys(Dictionary<string, List<string>> dictParams, List<string> removeKeys = default)
        {
            removeKeys = removeKeys ?? new List<string>() { ExcelIEConsts.TemplateCode, ExcelIEConsts.TntId, ExcelIEConsts.UserId, ExcelIEConsts.UserName, MqParams.Query, MqParams.Fields };
            foreach (var item in removeKeys)
            {
                if (dictParams.ContainsKey(item))
                    dictParams.Remove(item);
            }
        }
    }
}
