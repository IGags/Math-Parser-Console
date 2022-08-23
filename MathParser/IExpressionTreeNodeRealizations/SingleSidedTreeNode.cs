using MathParser.Interfaces;

namespace MathParser.IExpressionTreeNodeRealizations;

internal class SingleSidedTreeNode : IExpressionTreeNode
{
    private readonly IOperatorDescription _treeNode;
    private IExpressionTreeNode? _leaf;

    public SingleSidedTreeNode(IOperatorDescription node, ParserBehave behave)
    {
        _treeNode = node;
        Behave = behave;
    }

    public ParserBehave Behave { get; }

    public object Evaluate()
    {
        return _treeNode.Function.Act(_leaf.Evaluate());
    }

    public int GetPriority()
    {
        return _treeNode.Priority;
    }

    public IExpressionTreeNode BalanceTree(IExpressionTreeNode otherNode)
    {
        if (otherNode.GetPriority() <= GetPriority()) return otherNode.BalanceTree(this);
        if(_leaf == null) return AddUnparsedNode(otherNode);
        _leaf = _leaf.BalanceTree(otherNode);
        return this;
    }

    private IExpressionTreeNode AddUnparsedNode(IExpressionTreeNode node)
    {
        if (node is UnparsedTreeNode unparsed) _leaf = unparsed.Parse();
        else _leaf = node;
        return this;
    }
}