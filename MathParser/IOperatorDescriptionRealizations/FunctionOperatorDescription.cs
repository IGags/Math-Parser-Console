using MathParser.Interfaces;

namespace MathParser.IOperatorDescriptionRealizations;

internal class FunctionOperatorDescription : IOperatorDescription
{
    public FunctionOperatorDescription(IFunction function, string name)
    {
        Function = function;
        Name = name;
    }

    public string Name { get; }
    public int Priority => 100;
    public IFunction Function { get; }
}