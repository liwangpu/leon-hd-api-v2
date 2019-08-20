using System;

namespace App.Base.Domain.Exceptions
{
    public class DomailException : Exception
    {
        public DomailException()
        { }

        public DomailException(string message)
            : base(message)
        { }

        public DomailException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
