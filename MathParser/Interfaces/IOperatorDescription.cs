namespace MathParser.Interfaces;

internal interface IOperatorDescription
{
    int Priority { get; }
    IFunction Function { get; }
}