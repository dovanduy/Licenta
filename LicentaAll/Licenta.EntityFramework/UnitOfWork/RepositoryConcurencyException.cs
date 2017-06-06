using System;
using System.Runtime.Serialization;

namespace Licenta.EntityFramework.UnitOfWork
{
    public class RepositoryConcurencyException : Exception
    {
        public RepositoryConcurencyException()
        {
        }

        public RepositoryConcurencyException(string message) : base(message)
        {
        }

        public RepositoryConcurencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryConcurencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
