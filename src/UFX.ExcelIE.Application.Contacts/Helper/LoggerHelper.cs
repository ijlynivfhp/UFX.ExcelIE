//using Microsoft.Extensions.Logging;
//using NLog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UFX.ExcelIE.Application.Contracts.Helper
//{
//    public static class LoggerHelper
//    {
//        private static readonly Logger logger;
//        static LoggerHelper()
//        {
//            logger = LogManager.GetCurrentClassLogger();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="o"></param>
//        public static void Info(Object o, bool isConsole = false)
//        {
//            logger.Info(o.ToString());
//            Console.WriteLine(o.ToString());
//        }

//        public static void Error(string message, Exception ex, bool isConsole = false)
//        {
//            logger.Error(ex, message);
//            Console.WriteLine($"message:{message},ex.Message:{ex.Message}");
//        }

//        public static void Error(Exception ex, bool isConsole = false)
//        {
//            logger.Error(ex, ex.Message);
//            Console.WriteLine($"ex.Message:{ex.Message}");
//        }

//        public static void Error(string message, bool isConsole = false)
//        {
//            logger.Error(message);
//            Console.WriteLine(message);
//        }

//        public static void Debug(string message, bool isConsole = false)
//        {
//            logger.Debug(message);
//            Console.WriteLine(message);
//        }
//    }
//}
