namespace MathParser;

public static class StringExtensions
{
    public static bool CheckBracketExpression(this string expression)
    {
        var bracketStack = new Stack<bool>();
        foreach (var ch in expression)
        {
            if (ch == '(') bracketStack.Push(true);
            if (ch != ')') continue;
            if (bracketStack.Any()) bracketStack.Pop();
            else throw new InvalidOperationException("bracket expression is incorrect");
        }

        return !bracketStack.Any();
    }
}