using System.Globalization;
using NUnit.Framework;
using MathParser;

namespace MathParserTests
{
    [TestFixture]
    public class Tests
    {
        private readonly ExpressionParser _parser = new(ParserBehave.ThrowException);

        #region ValueTests

        [TestCase("1", "1.0")]
        [TestCase("1", "1,0")]
        [TestCase("0.1", ".1")]
        [TestCase("0.1", ",1")]
        [TestCase("True", "true")]
        [TestCase("False", "false")]
        [TestCase("1","1")]
        public void ValueCases(string should, string input)
        {
            var actual = _parser.Solve(input);
            Assert.AreEqual(should, actual);
        }
        [Test]
        public void SimpleNumberParsingRandomTest()
        {
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var rnd = random.NextDouble() * 10000;
                Assert.AreEqual(rnd.ToString(CultureInfo.InvariantCulture), _parser.Solve(rnd.ToString(CultureInfo.InvariantCulture)));
            }
        }

        #endregion

        #region AtomicOperatorsSimpleTests

        [TestCase("2", "1+1")]
        [TestCase("2", "1++")]
        [TestCase("2", "++1")]
        [TestCase("0", "1--")]
        [TestCase("0", "--1")]
        [TestCase("1", "+1")]
        [TestCase("-1", "-1")]
        [TestCase("6", "2*3")]
        [TestCase("3", "6/2")]
        [TestCase("36", "6**2")]
        [TestCase("0", "1-1")]
        [TestCase("1", "4%3")]
        [TestCase("True", "2==2.0")]
        [TestCase("True", "1!=3")]
        [TestCase("False", "1==2")]
        [TestCase("False", "1!=1")]
        [TestCase("Infinity", "1/0")]
        [TestCase("NaN", "0/0")]
        public void AtomicSimpleCases(string should, string input)
        {
            var actual = _parser.Solve(input);
            Assert.AreEqual(should, actual);
        }

        #endregion

        #region AtomicPriorityTests
        [TestCase("6", "2+2*2")]
        [TestCase("4", "1+++++1")]
        [TestCase("4", "++1+++1")]
        [TestCase("8", "(2+2)*2")]
        [TestCase("16", "(2+2)*2**3/(3-1)")]
        [TestCase("2", "((((1+1))))")]
        public void AtomicPriorityCases(string should, string input)
        {
            var actual = _parser.Solve(input);
            Assert.AreEqual(should, actual);
        }


        #endregion

        #region AtomicBoolOperationTableTests

        private Dictionary<string, Func<bool, bool, bool>> CreateOperatorDictionary()
        {
            return new Dictionary<string, Func<bool, bool, bool>>()
            {
                { "||", (arg1, arg2) => arg1 || arg2 },
                { "&&", (arg1, arg2) => arg1 && arg2 },
                { "^", (arg1, arg2) => arg1 ^ arg2 },
                { "==", (arg1, arg2) => arg1 == arg2 },
                { "!=", (arg1, arg2) => arg1 != arg2 },
            };
        }

        [Test]
        public void TestBoolAtomicOperator()
        {
            foreach (var entry in CreateOperatorDictionary())
            {
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        var should = entry.Value(i.ToBool(), j.ToBool());
                        Assert.AreEqual(should.ToString(), _parser.Solve(i.ToBool() + entry.Key + j.ToBool()));
                    }
                }
            }
        }


        #endregion

        #region CustomFunctionTests

        [TestCase("0.9999996829318346", "sin(3.14/2)")]
        public void CustomFunctionTests(string should, string unparsed)
        {
            Assert.AreEqual(should, _parser.Solve(unparsed));
        }

        #endregion
    }

#region ExtensionClass
    internal static class IntExtensions
    {
        public static bool ToBool(this int number) => number != 0;
    }


#endregion
}