using MathParser.Interfaces;

public enum AtomicOperatorAttachment
{
    Postfix,
    PostfixWeak,
    Prefix,
    PrefixWeak,
    BothSide
}

namespace MathParser.IOperatorDescriptionRealizations
{
    public class AtomicOperatorDescription : IOperatorDescription
    {
        public AtomicOperatorDescription(int priority, AtomicOperatorAttachment attachment, IFunction function)
        {
            Priority = priority;
            Attachment = attachment;
            Function = function;
        }

        public AtomicOperatorAttachment Attachment { get; }
        public int Priority { get; }
        public IFunction Function { get; }
    }
}