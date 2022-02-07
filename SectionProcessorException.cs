using System;
using System.Runtime.Serialization;

namespace SackboySaveFix
{
    public class SectionProcessorException : ProgramExecutionException
    {
        public SectionProcessorException(World world) : this($"World {world} is already unlocked!")
        {
        }

        public SectionProcessorException(string message) : base(message)
        {
        }

        public SectionProcessorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SectionProcessorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
