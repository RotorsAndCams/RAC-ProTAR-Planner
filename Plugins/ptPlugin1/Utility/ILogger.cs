using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptPlugin1.Utility
{
    internal interface ILogger
    {
        void Debug(string message, int sysid = 0);
        void Info(string message, int sysid = 0);
        void Warning(string message, int sysid = 0);
        void Error(string message, int sysid = 0);
    }
}
