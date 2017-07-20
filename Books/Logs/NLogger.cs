using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Books.Logs
{
    public class NLogger:ILogger
    {
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public void Trace(string message)
        {
            logger.Trace(message);
        }
        public void Debug(string message)
        {
            logger.Debug(message);
        }
        public void Info(string message)
        {
            logger.Info(message);
        }
        public void Warn(string message)
        {
            logger.Warn(message);
        }
        public void Error(string message)
        {
            logger.Info("Unhandled exception:");
            logger.Error(message);
        }
        public void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}
