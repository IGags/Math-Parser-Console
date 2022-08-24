using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathParser.Interfaces;

namespace Math_Parser_Console
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(IEnumerable enumerable)
        {
            foreach (var element in enumerable)
            {
                element.ToString();
            }
        }
    }
}
