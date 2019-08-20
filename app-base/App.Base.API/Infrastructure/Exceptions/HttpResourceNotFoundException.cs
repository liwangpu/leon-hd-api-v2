using System;

namespace App.Base.API.Infrastructure.Exceptions
{
    /// <summary>
    /// 资源没有找到异常,返回http 404
    /// </summary>
    public class HttpResourceNotFoundException : Exception
    {
        public HttpResourceNotFoundException()
        {
        }

        public HttpResourceNotFoundException(string message)
            : base(message)
        { }

        public HttpResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
