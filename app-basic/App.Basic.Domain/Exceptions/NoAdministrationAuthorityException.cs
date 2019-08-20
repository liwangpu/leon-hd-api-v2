using System;

namespace App.Basic.Domain.Exceptions
{
    public class NoAdministrationAuthorityException : Exception
    {
        public NoAdministrationAuthorityException()
            : base()
        { }

        public NoAdministrationAuthorityException(string message)
            : base(message)
        { }

        public NoAdministrationAuthorityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
