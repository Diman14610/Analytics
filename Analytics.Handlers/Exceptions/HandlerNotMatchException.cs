using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Handlers.Exceptions
{
    public class HandlerNotMatchException : Exception
    {
        public HandlerNotMatchException()
        {
        }

        public HandlerNotMatchException(string? message) : base(message)
        {
        }

        public HandlerNotMatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HandlerNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
