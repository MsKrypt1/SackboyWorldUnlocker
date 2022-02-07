using System;
using System.Runtime.Serialization;

namespace SackboySaveFix
{
    public class BytesPatternException : ProgramExecutionException
    {
        public BytesPatternException(BytesPattern pattern) : this($"Couldn't find pattern \"{pattern.Pattern}\" in the provided input")
        {
        }

        public BytesPatternException(string message) : base(message)
        {
        }

        public BytesPatternException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BytesPatternException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
