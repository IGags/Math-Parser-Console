using MathParser.Interfaces;

namespace MathParser.IFunctionCustomRealizations;

internal class Sin : IFunction
{
    public object Act(params object[] args)
    {
        return Math.Sin((double)args[0]);
    }
}