using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class UnaryPlus : IFunction
{
    public object Act(params object[] args)
    {
        return args[0];
    }
}