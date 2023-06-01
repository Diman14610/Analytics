using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Handlers.Exceptions
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException()
        {
        }

        public HandlerNotFoundException(string? message) : base(message)
        {
        }

        public HandlerNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
