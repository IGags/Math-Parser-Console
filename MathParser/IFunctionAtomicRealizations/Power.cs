using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class Power : IFunction
{
    public object Act(params object[] args)
    {
        return Math.Pow((double)args[0], (double)args[1]);
    }
}