using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class LogicalNegation : IFunction
{
    public object Act(params object[] args)
    {
        return !(bool)args[0];
    }
}