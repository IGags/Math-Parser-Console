using MathParser.Interfaces;

namespace MathParser.IOperatorDescriptionRealizations;

public class FunctionOperatorDescription : IOperatorDescription
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