using MathParser.IFunctionAtomicRealizations;
using MathParser.IOperatorDescriptionRealizations;

namespace MathParser;

internal static class AtomicOperatorConstants
{
    public static readonly Dictionary<string, List<AtomicOperatorDescription>> AtomicOperators =
        new()
        {
            { "**", new List<AtomicOperatorDescription> { new(0, AtomicOperatorAttachment.BothSide, new Power()) } },
            {
                "++", new List<AtomicOperatorDescription>
                {
                    new(0, AtomicOperatorAttachment.Postfix, new Increment()),
                    new(-1, AtomicOperatorAttachment.Prefix, new Increment())
                }
            },
            {
                "--", new List<AtomicOperatorDescription>
                {
                    new(0, AtomicOperatorAttachment.Postfix, new Decrement()),
                    new(-1, AtomicOperatorAttachment.Prefix, new Decrement())
                }
            },
            {
                "!",
                new List<AtomicOperatorDescription>
                    { new(-1, AtomicOperatorAttachment.Prefix, new LogicalNegation()) }
            },
            {
                "-", new List<AtomicOperatorDescription>
                {
                    new(-1, AtomicOperatorAttachment.PrefixWeak, new UnaryMinus()),
                    new(-3, AtomicOperatorAttachment.BothSide, new BothSidedMinus())
                }
            },
            {
                "+", new List<AtomicOperatorDescription>
                {
                    new(-1, AtomicOperatorAttachment.PrefixWeak, new UnaryPlus()),
                    new(-3, AtomicOperatorAttachment.BothSide, new BothSidedPlus())
                }
            },
            {
                "*",
                new List<AtomicOperatorDescription>
                    { new(-2, AtomicOperatorAttachment.BothSide, new BothSidedMultiplication()) }
            },
            {
                "/",
                new List<AtomicOperatorDescription>
                    { new(-2, AtomicOperatorAttachment.BothSide, new BothSidedDivision()) }
            },
            {
                "%",
                new List<AtomicOperatorDescription>
                    { new(-2, AtomicOperatorAttachment.BothSide, new EuclideanDivision()) }
            },
            { "<", new List<AtomicOperatorDescription> { new(-4, AtomicOperatorAttachment.BothSide, new Lesser()) } },
            {
                "<=",
                new List<AtomicOperatorDescription> { new(-4, AtomicOperatorAttachment.BothSide, new LesserOrEquals()) }
            },
            { ">", new List<AtomicOperatorDescription> { new(-4, AtomicOperatorAttachment.BothSide, new Higher()) } },
            {
                ">=",
                new List<AtomicOperatorDescription> { new(-4, AtomicOperatorAttachment.BothSide, new HigherOrEquals()) }
            },
            {
                "==",
                new List<AtomicOperatorDescription> { new(-5, AtomicOperatorAttachment.BothSide, new BoolEquals()) }
            },
            {
                "!=",
                new List<AtomicOperatorDescription>
                    { new(-5, AtomicOperatorAttachment.BothSide, new BoolNotEquals()) }
            },
            { "^", new List<AtomicOperatorDescription> { new(-6, AtomicOperatorAttachment.BothSide, new NOR()) } },
            { "&&", new List<AtomicOperatorDescription> { new(-7, AtomicOperatorAttachment.BothSide, new LazyAnd()) } },
            { "||", new List<AtomicOperatorDescription> { new(-8, AtomicOperatorAttachment.BothSide, new LazyOr()) } }
        };
}