using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class LazyAnd : IFunction
{
    public object Act(params object[] args)
    {
        return (bool)args[0] && (bool)args[1];
    }
}