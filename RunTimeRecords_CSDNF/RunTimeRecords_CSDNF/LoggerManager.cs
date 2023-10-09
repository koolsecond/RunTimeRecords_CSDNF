using NLog;
using System;

namespace RunTimeRecords_CSDNF
{
    internal class LoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogTrace(string message)
        {
            logger.Trace(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarning(string message, Exception ex = null)
        {
            if (ex != null)
            {
                logger.Warn(ex, message);
            }
            else
            {
                logger.Warn(message);
            }
        }

        public void LogError(string message, Exception ex = null)
        {
            if (ex != null)
            {
                logger.Error(ex, message);
            }
            else
            {
                logger.Error(message);
            }
        }

        public void LogFatal(string message, Exception ex = null)
        {
            if (ex != null)
            {
                logger.Fatal(ex, message);
            }
            else
            {
                logger.Fatal(message);
            }
        }
    }
}
