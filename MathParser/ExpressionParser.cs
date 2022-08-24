using System.Globalization;
using System.Text;
using MathParser.IExpressionTreeNodeRealizations;
using MathParser.Interfaces;

namespace MathParser; //:TODO затарможенный парсер IEnumerable без детей

public enum ParserBehave
{
    UnknownSequenceAsVariable,
    ThrowException
}

public class ExpressionParser
{
    private readonly ParserBehave _behave;
    private readonly ILogger _logger;

    public ExpressionParser(ParserBehave behave, ILogger? logger = null)
    {
        _logger = logger ?? new SilentLogger();
        _behave = behave;
    }

    public string Solve(string expression)
    {
        expression = PreProcessString(expression);
        if (!expression.CheckBracketExpression()) throw new Exception("bracket expression is incorrect");
        var node = new UnparsedTreeNode(expression, _behave).Parse();
        var value = node.Evaluate();
        //_logger.Log(node);
        return value switch
        {
            bool val => val.ToString(),
            double val => val.ToString(CultureInfo.InvariantCulture),
            string val => val,
            _ => value.ToString()
        };
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