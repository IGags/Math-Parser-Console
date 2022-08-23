using MathParser.IFunctionCustomRealizations;
using MathParser.IOperatorDescriptionRealizations;

namespace MathParser;

internal class CustomFunctionList : FunctionProvider
{
    public static Dictionary<string, FunctionOperatorDescription> Functions =
        new()
        {
            { "sin", new FunctionOperatorDescription(new Sin(), "sin") }
        };
}