using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace App.Basic.API.Application.Queries.Tokens
{
    public class TokenRequestQueryHandler : IRequestHandler<TokenRequestQuery, TokenRequestQueryDTO>
    {
        private readonly IAccountRepository accountRepository;
        private readonly AppConfig _appConfig;

        #region ctor
        public TokenRequestQueryHandler(IAccountRepository accountRepository, IOptions<AppConfig> settingsOptions)
        {
            this.accountRepository = accountRepository;
            _appConfig = settingsOptions.Value;
        }
        #endregion

        #region Handle
        public async Task<TokenRequestQueryDTO> Handle(TokenRequestQuery request, CancellationToken cancellationToken)
        {
            var accountId = await accountRepository.Get(new TokenRequestSpecification(request.Username, request.Password)).Select(x => x.Id).FirstAsync();
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOrganizationAsync(account);

            var dto = new TokenRequestQueryDTO();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Name)
                ,new Claim(ClaimTypes.Role, account.SystemRoleId.ToString())
                , new Claim("AccountId",account.Id)
                , new Claim("OrganizationTypeId",account.Organization.OrganizationTypeId.ToString())
                , new Claim("OrganizationId", account.OrganizationId)
            };

            var expires = DateTime.Now.AddDays(_appConfig.JwtSettings.ExpiresDay);
            dto.Expires = expires;
            dto.Token = new JwtSecurityToken(
             issuer: _appConfig.JwtSettings.Issuer,
             audience: _appConfig.JwtSettings.Audience,
                   claims: claims,
             notBefore: DateTime.Now,
             expires: expires,
             signingCredentials: creds);

            return dto;
        }
        #endregion
    }
}
