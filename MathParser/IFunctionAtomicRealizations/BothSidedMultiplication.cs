using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class BothSidedMultiplication : IFunction
{
    public Dictionary<Type, List<Type>>? ContextDescription { get; }

    public object Act(params object[] args)
    {
        return (double)args[0] * (double)args[1];
    }
}