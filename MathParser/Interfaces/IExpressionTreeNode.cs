using System.Collections;

namespace MathParser.Interfaces;

internal interface IExpressionTreeNode : IEnumerable
{
    ParserBehave Behave { get; }
    object Evaluate();
    int GetPriority();
    IExpressionTreeNode BalanceTree(IExpressionTreeNode otherNode);
}