using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathParser.Interfaces;

namespace MathParser
{
    internal class SilentLogger : ILogger
    {
        public void Log(string message)
        {
        }

        public void Log(IEnumerable enumerable)
        {
        }
    }
}
