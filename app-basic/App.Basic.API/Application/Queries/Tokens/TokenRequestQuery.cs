using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace App.Basic.API.Application.Queries.Tokens
{
    public class TokenRequestQuery : IRequest<TokenRequestQueryDTO>
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    public class TokenRequestQueryDTO
    {
        public DateTime Expires { get; set; }
        public JwtSecurityToken Token { get; set; }
    }

}
