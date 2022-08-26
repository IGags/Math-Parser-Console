using MathParser.IFunctionCustomRealizations;
using MathParser.Interfaces;
using MathParser.IOperatorDescriptionRealizations;

namespace MathParser;

internal class CustomFunctionList : IFunctionProvider
{
    public Dictionary<string, FunctionOperatorDescription> Functions { get; } =
        new()
        {
            { "sin", new FunctionOperatorDescription(new Sin(), "sin") },
            { "cos", new FunctionOperatorDescription(new Cos(), "cos") }
        };
}