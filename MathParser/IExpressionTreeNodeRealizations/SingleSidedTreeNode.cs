using System.Collections;
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
        if(_leaf == null) return AddNode(otherNode);
        _leaf = _leaf.BalanceTree(otherNode);
        return this;
    }

    private IExpressionTreeNode AddNode(IExpressionTreeNode node)
    {
        _leaf = node;
        return this;
    }

    public IEnumerator GetEnumerator()
    {
        yield return this;
        if (_leaf is UnparsedTreeNode unparsed) _leaf = unparsed.Parse();
        else if (_leaf != null)
        {
            foreach (var val in _leaf)
            {
                yield return val;
            }
        }
    }
}