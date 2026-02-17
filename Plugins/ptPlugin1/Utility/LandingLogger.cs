using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptPlugin1.Utility
{
    internal class LandingLogger : ILogger
    {
        // singleton pattern.
        private static LandingLogger instance;
        private static Logger logger; // hold a single instance of the nLog logger

        private LandingLogger()
        {
        
        }
        public static LandingLogger GetInstance()
        {
            if (instance == null)
                instance = new LandingLogger();
            return instance;
        }

        private Logger GetLogger(string theLogger)
        { 
            if (LandingLogger.logger == null)
                LandingLogger.logger = LogManager.GetLogger(theLogger);
            return LandingLogger.logger;
        }


        public void Debug(string message, int sysid = 0)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, int sysid = 0)
        {
            GetLogger("landingLoggerRule").Error($"sysid={sysid}: {message}");
        }

        public void Info(string message, int sysid = 0)
        {
            GetLogger("landingLoggerRule").Info($"sysid={sysid}: {message}");
        }

        public void Warning(string message, int sysid = 0)
        {
            throw new NotImplementedException();
        }
    }
}
