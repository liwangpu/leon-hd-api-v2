using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Linq;

namespace App.Base.API.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private IHttpContextAccessor _context;

        public UriService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUri(bool toLowerCase = true)
        {
            var uri = _context.HttpContext.Request.GetEncodedUrl();
            if (toLowerCase)
                return uri.ToLower();
            return uri;
        }

        public string GetUriWithoutQuery(bool toLowerCase = true)
        {
            var uriStr = _context.HttpContext.Request.GetEncodedUrl();
            var arr = uriStr.Split("?", StringSplitOptions.RemoveEmptyEntries);
            var uri = arr.First();
            if (toLowerCase)
                return uri.ToLower();
            return uri;
        }

    }
}
