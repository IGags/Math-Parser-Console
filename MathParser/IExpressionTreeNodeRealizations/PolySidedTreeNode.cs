using System.Collections;
using MathParser.Interfaces;

namespace MathParser.IExpressionTreeNodeRealizations;

internal class PolySidedTreeNode : IExpressionTreeNode
{
    private List<IExpressionTreeNode> _nodes;
    private readonly IOperatorDescription _treeNode;

    public PolySidedTreeNode(IOperatorDescription treeNode, IEnumerable<IExpressionTreeNode> nodes, ParserBehave behave)
    {
        _nodes = nodes.ToList();
        _treeNode = treeNode;
        Behave = behave;
    }

    public ParserBehave Behave { get; }

    public int GetPriority()
    {
        return _treeNode.Priority;
    }

    public IExpressionTreeNode BalanceTree(IExpressionTreeNode otherNode)
    {
        if (otherNode.GetPriority() >= GetPriority())
            throw new InvalidOperationException("Function and other node cannot stand nearby");
        return otherNode.BalanceTree(this);
    }

    public object Evaluate()
    {
        return _treeNode.Function.Act(_nodes.Select(n => n.Evaluate()));
    }

    public IEnumerator GetEnumerator()
    {
        _nodes = _nodes.Select(value => ((UnparsedTreeNode)value).Parse()).ToList();
        yield return this;
    }
}