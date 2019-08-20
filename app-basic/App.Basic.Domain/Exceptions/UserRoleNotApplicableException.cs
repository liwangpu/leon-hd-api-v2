using System;

namespace App.Basic.Domain.Exceptions
{
    public class UserRoleNotApplicableException : Exception
    {
        public UserRoleNotApplicableException()
              : base()
        { }

        public UserRoleNotApplicableException(string message)
            : base(message)
        { }

        public UserRoleNotApplicableException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
