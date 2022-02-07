using System;
using System.Runtime.Serialization;

namespace SackboySaveFix
{
    public class ProgramExecutionException : Exception
    {
        public ProgramExecutionException()
        {
        }

        public ProgramExecutionException(string message) : base(message)
        {
        }

        public ProgramExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProgramExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
