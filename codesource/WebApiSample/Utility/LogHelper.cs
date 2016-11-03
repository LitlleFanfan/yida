using System;
using System.Reflection;
using log4net;

namespace WebApiSample.Utility
{
    public class LogHelper
    {
        private static ILog Log;

        static LogHelper()
        {
            if (Log == null)
            {
                Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        private LogHelper()
        {
            if (Log == null)
            {
                Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
        }


        public string HostName
        {
            get
            {
                return Environment.UserName;
            }
        }

        public void WriteInfo(string msg)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(HostName + ":" + msg);
            }
        }

        public void WriteFatal(string msg, Exception ex)
        {
            if (Log.IsFatalEnabled)
            {
                Log.Fatal(HostName + ":" + msg, ex);
            }
        }

        public void WriteDebug(string msg, Exception ex)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(HostName + ":" + msg, ex);
            }
        }

        public void WriteWarning(string msg, Exception ex)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(HostName + ":" + msg, ex);
            }
        }

        public void WriteError(string msg, Exception ex)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(HostName + ":" + msg, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static LogHelper GetInstance()
        {
            return new LogHelper();
        }
    }
}