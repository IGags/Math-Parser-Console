using MathParser.Interfaces;

namespace MathParser.IFunctionAtomicRealizations;

internal class BoolEquals : IFunction
{
    public object Act(params object[] args)
    {
        return args[0] switch
        {
            string => (string)args[0] == (string)args[1],
            double => Math.Abs((double)args[0] - (double)args[1]) < 1e-10,
            bool => (bool)args[0] == (bool)args[1],
            _ => throw new InvalidOperationException("Unknown operand type")
        };
    }
}