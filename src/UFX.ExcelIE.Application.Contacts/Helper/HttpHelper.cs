using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Helper
{
    public class HttpHelper
    {
        #region Dictionary 和参数字符串互转
        #region Dictionary Parse To String
        /// <summary>
        /// Dictionary Parse To String
        /// </summary>
        /// <param name="parameters">Dictionary</param>
        /// <returns>String</returns>
        public static string ParseToString(IDictionary<string, List<string>> parameters)
        {
            IDictionary<string, List<string>> sortedParams = new SortedDictionary<string, List<string>>(parameters);
            IEnumerator<KeyValuePair<string, List<string>>> dem = sortedParams.GetEnumerator();

            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                var key = dem.Current.Key;
                var value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && value.Count>0)
                {
                    foreach (var item in value)
                    {
                        query.Append(key).Append("=").Append(item).Append("&");
                    }
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);

            return content;
        }
        #endregion

        #region String Parse To Dictionary
        /// <summary>
        /// String Parse To Dictionary
        /// </summary>
        /// <param name="parameter">String</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, List<string>> ParseToDictionary(string parameter = "")
        {

            var dict = new Dictionary<string, List<string>>();
            try
            {
                String[] dataArry = parameter.Trim('?').Split('&');

                for (int i = 0; i <= dataArry.Length - 1; i++)
                {
                    String dataParm = dataArry[i];
                    int dIndex = dataParm.IndexOf("=");
                    if (dIndex != -1)
                    {
                        String key = dataParm.Substring(0, dIndex);
                        String value = dataParm.Substring(dIndex + 1, dataParm.Length - dIndex - 1);
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                            continue;
                        if (dict.ContainsKey(key))
                            dict[key].Add(value);
                        else
                        {
                            dict.Add(key, new List<string>() { value });
                        }
                    }
                }
                return dict;
            }
            catch
            {
                return dict;
            }
        }
        #endregion
        #endregion
    }
}
