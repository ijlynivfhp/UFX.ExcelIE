using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Helper
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToJsonString(this object obj)
        {
            string str = "";
            var settings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",            //时间格式化
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore //忽略循环引用
            };
            str = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, settings);
            return str;
        }

        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToJson<T>(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }
    }
}
