using System;

namespace App.Basic.Domain.Exceptions
{
    public class UserRoleDuplicationException : Exception
    {
        public UserRoleDuplicationException()
              : base()
        { }

        public UserRoleDuplicationException(string message)
            : base(message)
        { }

        public UserRoleDuplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
