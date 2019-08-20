using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;


namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountAccessPointQueryHandler : IRequestHandler<AccountAccessPointQuery, AccountAccessPointQueryDTO>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IIdentityService identityService;
        #region ctor
        public AccountAccessPointQueryHandler(IAccountRepository accountRepository, ICustomRoleRepository customRoleRepository, IIdentityService identityService)
        {
            this.accountRepository = accountRepository;
            this.customRoleRepository = customRoleRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<AccountAccessPointQueryDTO> Handle(AccountAccessPointQuery request, CancellationToken cancellationToken)
        {
            var dto = new AccountAccessPointQueryDTO();
            var account = await accountRepository.FindAsync(identityService.GetUserId());
            await accountRepository.LoadOwnRolesAsync(account);

            var systemRole = Enumeration.FromValue<SystemRole>(account.SystemRoleId);
            if (!string.IsNullOrWhiteSpace(systemRole.AccessPointKeys))
                dto.Keys.AddRange(systemRole.AccessPointKeys.Split(",", StringSplitOptions.RemoveEmptyEntries));

            foreach (var cus in account.OwnRoles)
            {
                var role = await customRoleRepository.FindAsync(cus.CustomRoleId);
                if (!string.IsNullOrWhiteSpace(role.AccessPointKeys))
                {
                    var ks = role.AccessPointKeys.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    dto.Keys.AddRange(ks);
                }
            }
            dto.DistinctKey();
            return dto;
        }
        #endregion
    }
}
