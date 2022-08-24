using MathParser;

namespace Math_Parser_Console
{
    internal class Program
    {
        public static void Main()
        {
            var parser = new ExpressionParser(ParserBehave.ThrowException, new ConsoleLogger());
            while (true)
            {
                var inputString = Console.ReadLine();
                if(inputString != null) Console.WriteLine(parser.Solve(inputString));
            }
        }
    }
}

