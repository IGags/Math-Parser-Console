using System.Globalization;
using System.Text.RegularExpressions;
using MathParser.IExpressionTreeNodeRealizations;
using MathParser.Interfaces;
using MathParser.IOperatorDescriptionRealizations;

namespace MathParser;

internal class ParserMethods
{
    private readonly ParserBehave _behave;
    private readonly Regex _regex = new(@"^(?<Value>\d*((\.|\,){1}\d+)?)(?<Left>.*)", RegexOptions.Compiled);

    public ParserMethods(ParserBehave behave)
    {
        _behave = behave;
    }

    public IExpressionTreeNode? ParseOperator(ref string fragment, ref PreviousSegmentContent content)
    {
        var op = TryParseAtomicOperator(fragment, content);
        if (op.Item1 != null)
        {
            content = op.Item1.Attachment switch
            {
                AtomicOperatorAttachment.Postfix => PreviousSegmentContent.Postfix,
                AtomicOperatorAttachment.PostfixWeak => PreviousSegmentContent.Postfix,
                AtomicOperatorAttachment.Prefix => PreviousSegmentContent.Prefix,
                AtomicOperatorAttachment.PrefixWeak => PreviousSegmentContent.WeakPrefix,
                AtomicOperatorAttachment.BothSide => PreviousSegmentContent.BothSidedOperator,
                _ => throw new ArgumentOutOfRangeException()
            };

            fragment = UpdateFragment(fragment, op.Item2);

            return op.Item1.Attachment switch
            {
                AtomicOperatorAttachment.Postfix => new SingleSidedTreeNode(op.Item1, _behave),
                AtomicOperatorAttachment.PostfixWeak => new SingleSidedTreeNode(op.Item1, _behave),
                AtomicOperatorAttachment.Prefix => new SingleSidedTreeNode(op.Item1, _behave),
                AtomicOperatorAttachment.PrefixWeak => new SingleSidedTreeNode(op.Item1, _behave),
                AtomicOperatorAttachment.BothSide => new BothSidedTreeNode(op.Item1, _behave),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        var func = TryParseFunction(ref fragment, content);
        if (func != null) content = PreviousSegmentContent.Function;
        return func;
    }

    public ValueTreeNode? ParseValue(ref string fragment)
    {
        
        if (fragment.StartsWith("true", StringComparison.InvariantCultureIgnoreCase)) return AssembleBoolNode(ref fragment, "true");
        if (fragment.StartsWith("false", StringComparison.InvariantCultureIgnoreCase)) return AssembleBoolNode(ref fragment, "false");
        var groups = _regex.Matches(fragment);
        if (groups.Count == 0 || groups[0].Groups["Value"].Length == 0) return null;
        fragment = groups[0].Groups["Left"].Value;
        var value = groups[0].Groups["Value"].Value.Replace('.', ',');
        return new ValueTreeNode(double.Parse(value, NumberStyles.AllowDecimalPoint & NumberStyles.Any), _behave);
    }

    public UnparsedTreeNode ResolveBrackets(ref string fragment)
    {
        return new UnparsedTreeNode(EvaluateBracketExpression(ref fragment), _behave);
    }

    private static (AtomicOperatorDescription?, int) TryParseAtomicOperator(string fragment,
        PreviousSegmentContent previousSegmentContext)
    {
        var sameOperators = AtomicOperatorConstants.AtomicOperators
            .Where(value => fragment.StartsWith(value.Key))
            .SelectMany(value => value.Value.Select(val => (value.Key, val)))
            .OrderByDescending(item => item.val.Priority);

        foreach (var operatorDescription in sameOperators)
            switch (operatorDescription.val.Attachment)
            {
                case AtomicOperatorAttachment.BothSide:
                    if (previousSegmentContext is PreviousSegmentContent.None
                        or PreviousSegmentContent.Prefix
                        or PreviousSegmentContent.WeakPrefix) continue;
                    return (operatorDescription.val, operatorDescription.Key.Length);
                case AtomicOperatorAttachment.PrefixWeak:
                    if (previousSegmentContext is not PreviousSegmentContent.None) continue;
                    return (operatorDescription.val, operatorDescription.Key.Length);
                case AtomicOperatorAttachment.Prefix:
                    if (previousSegmentContext is not (PreviousSegmentContent.None
                        or PreviousSegmentContent.BothSidedOperator)) continue;
                    return (operatorDescription.val, operatorDescription.Key.Length);
                case AtomicOperatorAttachment.Postfix:
                    if (previousSegmentContext is PreviousSegmentContent.Postfix
                        or PreviousSegmentContent.BothSidedOperator
                        or PreviousSegmentContent.Prefix
                        or PreviousSegmentContent.WeakPrefix
                        or PreviousSegmentContent.None) continue;
                    return (operatorDescription.val, operatorDescription.Key.Length);
                case AtomicOperatorAttachment.PostfixWeak:
                default: return (null, 0);
            }

        return (null, 0);
    }

    private IExpressionTreeNode? TryParseFunction(ref string fragment,
        PreviousSegmentContent previousSegmentContext,
        params FunctionProvider[] providers)
    {
        var bracketIndex = fragment.IndexOf('(');
        if (bracketIndex < 0) return null;
        var functionName = fragment[..(bracketIndex - 1)];
        if (!CustomFunctionList.Functions.ContainsKey(functionName)) return null;
        fragment = UpdateFragment(fragment, functionName.Length);
        var arguments = EvaluateBracketExpression(ref fragment).Split(',', StringSplitOptions.RemoveEmptyEntries);
        return new PolySidedTreeNode(CustomFunctionList.Functions[functionName],
            arguments.Select(value => (IExpressionTreeNode)new UnparsedTreeNode(value, _behave)).ToList(), _behave);
    }

    private ValueTreeNode AssembleBoolNode(ref string fragment, string trimValue)
    {
        fragment = UpdateFragment(fragment, trimValue.Length);
        return new ValueTreeNode(bool.Parse(trimValue), _behave);
    }

    private static string UpdateFragment(string fragment, int length)
    {
        return fragment.Length > length ? fragment[length..] : string.Empty;
    }

    private string EvaluateBracketExpression(ref string fragment)
    {
        var bracketStack = new Stack<bool>();
        for (var i = 0; i < fragment.Length; i++)
        {
            if (fragment[i] == '(') bracketStack.Push(true);
            if (fragment[i] != ')' && bracketStack.Any()) continue;
            if (bracketStack.Any()) {bracketStack.Pop();}
            if (bracketStack.Any()) continue;
            var bracketSequence = fragment.Substring(1, i - 1);
            fragment = UpdateFragment(fragment, i + 1);
            return bracketSequence;
        }
        throw new ArgumentException("there is no correct bracket expression");
    }
}