using System;
using System.Runtime.Serialization;

namespace Contracts.Exceptions
{
    public class AppConcurencyException : Exception
    {
        public AppConcurencyException()
        {
        }

        public AppConcurencyException(string message) : base(message)
        {
        }

        public AppConcurencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppConcurencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
