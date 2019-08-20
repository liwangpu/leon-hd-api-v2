using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountProfileQueryHandler : IRequestHandler<AccountProfileQuery, AccountProfileQueryDTO>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public AccountProfileQueryHandler(IAccountRepository accountRepository, IIdentityService identityService)
        {
            this.accountRepository = accountRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<AccountProfileQueryDTO> Handle(AccountProfileQuery request, CancellationToken cancellationToken)
        {
            var accountId = identityService.GetUserId();
            var account = await accountRepository.FindAsync(identityService.GetUserId());
            if (account == null)
                throw new Exception($"找不到Id为{accountId}的用户");

            var dto = new AccountProfileQueryDTO();
            dto.Id = account.Id;
            dto.Name = account.Name;
            dto.OrganizationId = account.OrganizationId;
            return dto;
        }
        #endregion
    }
}
