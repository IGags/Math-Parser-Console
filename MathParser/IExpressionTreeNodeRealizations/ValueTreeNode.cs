using System.Collections;
using MathParser.Interfaces;

namespace MathParser.IExpressionTreeNodeRealizations;

internal class ValueTreeNode : IExpressionTreeNode
{
    private readonly object _value;

    public ValueTreeNode(object value, ParserBehave behave)
    {
        _value = value;
        Behave = behave;
    }

    public ParserBehave Behave { get; }

    public object Evaluate()
    {
        return _value;
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

    public IEnumerator GetEnumerator()
    {
        yield return this;
    }
}