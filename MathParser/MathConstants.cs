using MathParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser
{
    internal class MathConstants : ICosntantProvider
    {
        public Dictionary<string, object> ConstantDictioanry { get; } = new Dictionary<string, object>
        {
            {"pi", Math.PI},
            {"e", Math.E},
            {"tau", Math.Tau}
        };
    }
}
