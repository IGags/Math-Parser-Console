using System.Collections;
using MathParser.Interfaces;

namespace MathParser.IExpressionTreeNodeRealizations;

internal enum PreviousSegmentContent
{
    None,
    BothSidedOperator,
    Prefix,
    WeakPrefix,
    Postfix,
    Variable,
    Function,
    Value,
    Unparsed
}
internal class UnparsedTreeNode : IExpressionTreeNode
{
    private readonly ParserMethods _parserMethods;
    private string _fragment;
    private IExpressionTreeNode? _tree;

    public UnparsedTreeNode(string fragment, ParserBehave behave)
    {
        _fragment = fragment;
        Behave = behave;
        _parserMethods = new ParserMethods(behave);
    }

    public ParserBehave Behave { get; }

    public object Evaluate()
    {
        throw new InvalidOperationException("NodeIsUnparsed");
    }

    public int GetPriority()
    {
        return 100;
    }

    public IExpressionTreeNode BalanceTree(IExpressionTreeNode otherNode)
    {
        if (otherNode.GetPriority() >= GetPriority())
            throw new InvalidOperationException("Function and other node cannot stand nearby");
        return otherNode.BalanceTree(this);
    }

    public IExpressionTreeNode Parse()
    {
        if (string.IsNullOrEmpty(_fragment)) throw new InvalidOperationException("Unparsed node data is incorrect");
        var prevSegmentData = PreviousSegmentContent.None;
        while (_fragment.Length != 0)
        {
            if (_fragment.StartsWith("\""))
            {
                var valueNode = _parserMethods.ParseString(ref _fragment);
                prevSegmentData = PreviousSegmentContent.Value;
                UpdateTree(valueNode);
                continue;
            }

            if (_fragment.StartsWith("("))
            {
                var unparsedNode = _parserMethods.ResolveBrackets(ref _fragment);
                prevSegmentData = PreviousSegmentContent.Unparsed;
                UpdateTree(unparsedNode);
                continue;
            }

            var description = _parserMethods.ParseOperator(ref _fragment, ref prevSegmentData);
            if (description != null)
            {
                UpdateTree(description);
                continue;
            }

            var number = _parserMethods.ParseValue(ref _fragment);
            if (number != null)
            {
                prevSegmentData = PreviousSegmentContent.Value;
                UpdateTree(number);
                continue;
            }

            throw Behave switch
            {
                ParserBehave.ThrowException => new InvalidOperationException($"string part is unparsed:{_fragment}"),
                ParserBehave.UnknownSequenceAsVariable => new NotImplementedException(),
                _ => new ArgumentOutOfRangeException()
            };
        }

        if (_tree is UnparsedTreeNode tree) _tree = tree.Parse();
        if (_tree == null) throw new InvalidOperationException("Symbolic sequence wasn't parsed");
        foreach (var node in _tree) { }
        return _tree;
    }

    private void UpdateTree(IExpressionTreeNode additionalNode)
    {
        _tree = _tree == null ? additionalNode : _tree.BalanceTree(additionalNode);
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}