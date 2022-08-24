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
        if (_left == null) _left = otherNode;
        else if (_right == null) _right = otherNode;
        else _right = _right?.BalanceTree(otherNode);
        return this;
    }

    public IEnumerator GetEnumerator()
    {
        yield return this;
        if (_right is UnparsedTreeNode unparsedRight) _right = unparsedRight.Parse();
        else if (_right != null)
        {
            foreach (var val in _right)
            {
                yield return val;
            }
        }
        if (_left is UnparsedTreeNode unparsedLeft) _left = unparsedLeft.Parse();
        else if (_left != null)
        {
            foreach (var val in _left)
            {
                yield return val;
            }
        }
    }
}