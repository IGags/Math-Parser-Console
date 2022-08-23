using System.Globalization;
using System.Text;
using MathParser.IExpressionTreeNodeRealizations;

namespace MathParser;

public enum ParserBehave
{
    UnknownSequenceAsVariable,
    ThrowException
}

public class ExpressionParser
{
    private readonly ParserBehave _behave;

    public ExpressionParser(ParserBehave behave)
    {
        _behave = behave;
    }

    public string Solve(string expression)
    {
        expression = PreProcessString(expression);
        if (!expression.CheckBracketExpression()) throw new Exception("bracket expression is incorrect");
        var node = new UnparsedTreeNode(expression, _behave).Parse();
        var value = node.Evaluate();
        switch (value)
        {
            case bool val:
                return val.ToString();
            case double val:
                return val.ToString(CultureInfo.InvariantCulture);
            case string val:
                return val;
            default:
                return value.ToString();
        }
    }

    private string PreProcessString(string expression)
    {
        var sb = new StringBuilder();
        foreach (var ch in expression)
            if (!SymbolConstants.EmptySymbols.Contains(ch))
                sb.Append(ch);
        return sb.Length == 0
            ? throw new InvalidOperationException("input string contains only splitters or empty chars")
            : sb.ToString();
    }
}