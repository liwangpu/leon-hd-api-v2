using System;

namespace App.OSS.Domain.Exceptions
{
    public class OSSDomailException : Exception
    {
        public OSSDomailException()
        { }

        public OSSDomailException(string message)
            : base(message)
        { }

        public OSSDomailException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
