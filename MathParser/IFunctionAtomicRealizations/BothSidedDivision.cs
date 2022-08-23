using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class BothSidedDivision : IFunction
{
    public object Act(params object[] args)
    {
        return (double)args[0] / (double)args[1];
    }
}