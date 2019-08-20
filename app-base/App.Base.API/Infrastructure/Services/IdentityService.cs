using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace App.Base.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetOrganizationId()
        {
            var claim = _context.HttpContext.User.Claims.Where(x => x.Type == "OrganizationId").FirstOrDefault();
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }

        public string GetOrganizationTypeId()
        {
            var claim = _context.HttpContext.User.Claims.Where(x => x.Type == "OrganizationTypeId").FirstOrDefault();
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }

        public string GetToken()
        {
            var auth = _context.HttpContext.Request.Headers["Authorization"].ToString().Trim();
            if (!string.IsNullOrWhiteSpace(auth))
            {
                var arr = auth.Split("bearer", StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length >= 1)
                    return arr[0].Trim();

            }
            return string.Empty;
        }

        public string GetUserId()
        {
            var claim = _context.HttpContext.User.Claims.Where(x => x.Type == "AccountId").FirstOrDefault();
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }

        public string GetUserRole()
        {
            var claim = _context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }
    }
}
