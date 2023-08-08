using System.Runtime.Serialization;

namespace Analytics.Methods.Exceptions
{
    public class FunctionNotImplementedException : Exception
    {
        public FunctionNotImplementedException()
        {
        }

        public FunctionNotImplementedException(string? message) : base(message)
        {
        }

        public FunctionNotImplementedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FunctionNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
