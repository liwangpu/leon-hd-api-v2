﻿using System;

namespace App.Base.API.Infrastructure.Exceptions
{
    public class HttpForbiddenException : Exception
    {
        public HttpForbiddenException()
        { }

        public HttpForbiddenException(string message)
            : base(message)
        { }

        public HttpForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
