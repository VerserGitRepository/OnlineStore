using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
namespace OnlineStore.WebUI.Infrastructure
{
    public class LogService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LogService(Logger logger)
        {
            _logger = logger;
        }
        public static void info(string Info)
        {
            _logger.Info(Info);
        }
        public static void Error(string _error)
        {
            _logger.Error(_error);
        }

    }
}