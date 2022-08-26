using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathParser.IOperatorDescriptionRealizations;

namespace MathParser.Interfaces
{
    public interface IFunctionProvider
    {
        public Dictionary<string, FunctionOperatorDescription> Functions { get; }
    }
}
