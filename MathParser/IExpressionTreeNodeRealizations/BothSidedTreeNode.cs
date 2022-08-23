using System.Collections;
using MathParser.Interfaces;

namespace MathParser.IExpressionTreeNodeRealizations;

internal class BothSidedTreeNode : IExpressionTreeNode
{
    private readonly IOperatorDescription _treeNode;
    private IExpressionTreeNode? _left;
    private IExpressionTreeNode? _right;

    public BothSidedTreeNode(IOperatorDescription node, ParserBehave behave)
    {
        _treeNode = node;
        Behave = behave;
    }

    public ParserBehave Behave { get; }

    public object Evaluate()
    {
        return _treeNode.Function.Act(_left.Evaluate(), _right.Evaluate());
    }

    public int GetPriority()
    {
        return _treeNode.Priority;
    }

    public IExpressionTreeNode BalanceTree(IExpressionTreeNode otherNode)
    {
        if (GetPriority() > otherNode.GetPriority()) return otherNode.BalanceTree(this);
        if (_left == null)
        {
            if (otherNode is UnparsedTreeNode node) otherNode = node.Parse();
            _left = otherNode;
        }
        else if (_right == null)
        {
            if (otherNode is UnparsedTreeNode node) otherNode = node.Parse();
            _right = otherNode;
        }
        else
        {
            _right = _right?.BalanceTree(otherNode);
        }

        return this;
    }

    public IEnumerator GetEnumerator()
    {
        yield return this;
        if (_left != null)
        {
            foreach (var value in _left)
            {
                yield return value;
            }
        }
        if (_right == null) yield break;
        foreach (var value in _right)
        {
            yield return value;
        }
    }
}