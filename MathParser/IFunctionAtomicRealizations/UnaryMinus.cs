using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class UnaryMinus : IFunction
{
    public object Act(params object[] args)
    {
        return -(double)args[0];
    }
}