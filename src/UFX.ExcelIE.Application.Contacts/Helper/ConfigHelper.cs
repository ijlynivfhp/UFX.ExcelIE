using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.ExcelIE.Application.Contracts.Helper
{
    public static class ConfigHelper
    {
        static IConfiguration config = null;
        static ConfigHelper()
        {
            string currentClassDir = AppContext.BaseDirectory;
            if (config == null)
            {
                config = new ConfigurationBuilder()
                    .SetBasePath(currentClassDir)
                    .AddJsonFile("appsettings.json", false, true)
                    .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                    .Build();
            }
        }

        /// <summary>  
        /// 获取系统公共配置文件  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static T GetValue<T>(string configName = default) where T : class, new()
        {
            T sysConfig = new T();
            try
            {
                config.GetSection(string.IsNullOrEmpty(configName) ? sysConfig.GetType().Name : configName).Bind(sysConfig);
            }
            catch (Exception)
            {
                sysConfig = null;
            }
            return sysConfig;
        }

        /// <summary>  
        /// 获取单一节点配置文件  
        /// </summary>  
        /// <param name="key">节点，如果是多级节点需要按照:分隔的方式传递</param>  
        /// <returns></returns>  
        public static string GetValue(string key)
        {
            return config.GetSection(key).Value.ToString().Trim();
        }
    }
}
