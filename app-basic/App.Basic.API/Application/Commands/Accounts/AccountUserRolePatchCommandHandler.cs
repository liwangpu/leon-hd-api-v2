using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountUserRolePatchCommandHandler : IRequestHandler<AccountUserRolePatchCommand>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        #region ctor
        public AccountUserRolePatchCommandHandler(IAccountRepository accountRepository, ICustomRoleRepository customRoleRepository, IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.accountRepository = accountRepository;
            this.customRoleRepository = customRoleRepository;
            this.identityService = identityService;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(AccountUserRolePatchCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.Id);
            if (account == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Account", request.Id]);

            await accountRepository.LoadOwnRolesAsync(account);

            request.ApplyPatch();

            account.UpdateCustomRoles(request.CustomRoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries));

            await accountRepository.UpdateAsync(account);

            return Unit.Value;
        }
        #endregion
    }
}
