using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Misc
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    class LogUtils
    {
        //可以声明多个日志对象
        public static ILog log = LogManager.GetLogger(typeof(LogUtils));


        #region 01-DEBUG（调试信息）
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Debug(string msg)
        {
            log.Debug(msg);
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exception">错误信息</param>
        public static void Debug(string msg, Exception exception)
        {
            log.Debug(msg, exception);
        }
        #endregion


        #region 02-INFO（一般信息）
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Info(string msg)
        {
            log.Info(msg);
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exception">错误信息</param>
        public static void Info(string msg, Exception exception)
        {
            log.Info(msg, exception);
        }
        #endregion

        #region 03-WARN（警告）
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Warn(string msg)
        {
            log.Warn(msg);
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exception">错误信息</param>
        public static void Warn(string msg, Exception exception)
        {
            log.Warn(msg, exception);
        }
        #endregion

        #region 04-ERROR（一般错误）
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Error(string msg)
        {
            log.Error(msg);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exception">错误信息</param>
        public static void Error(string msg, Exception exception)
        {
            log.Error(msg, exception);
        }
        #endregion

        #region 05-FATAL(致命错误)
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Fatal(string msg)
        {
            log.Fatal(msg);
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exception">错误信息</param>
        public static void Fatal(string msg, Exception exception)
        {
            log.Fatal(msg, exception);
        }

        #endregion

    }
}
