using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathParser.Interfaces;

namespace MathParser.IFunctionCustomRealizations
{
    internal class Cos : IFunction
    {
        public object Act(params object[] args) => Math.Cos((double) args[0]);
    }
}
