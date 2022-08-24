using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Interfaces
{
    public interface ILogger
    {
        public void Log(string message);
        public void Log(IEnumerable enumerable);
    }
}
