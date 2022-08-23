using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class Increment : IFunction
{
    public object Act(params object[] args)
    {
        return (double)args[0] + 1;
    }
}