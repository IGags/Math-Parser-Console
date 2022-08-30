using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Interfaces
{
    public interface ICosntantProvider
    {
        Dictionary<string, object> ConstantDictioanry { get; }
    }
}
