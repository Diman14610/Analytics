using System.Runtime.Serialization;

namespace Analytics.Core.Exceptions
{
    public class MethodNotFoundException : Exception
    {
        public MethodNotFoundException()
        {
        }

        public MethodNotFoundException(string? message) : base(message)
        {
        }

        public MethodNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MethodNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
