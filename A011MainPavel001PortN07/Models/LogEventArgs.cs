using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task003AsyncEnd003.Models
{
    public class LogEventArgs : EventArgs
    {
        public readonly string msg;

        public LogEventArgs(string message)
        {
            msg = message;
        }
    }
}
