using App.Base.API;
using App.Base.API.Infrastructure.ActionResults;
using App.Base.API.Infrastructure.Extensions;
using App.Base.API.Infrastructure.Services;
using App.Basic.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountBatchDeleteCommandHandler : IRequestHandler<AccountBatchDeleteCommand, ObjectResult>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUriService uriService;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IUserManagedAccountService userManagedAccountService;

        public AccountBatchDeleteCommandHandler(IAccountRepository accountRepository, IUriService uriService, IIdentityService identityService, IStringLocalizer<CommonTranslation> localizer, IUserManagedAccountService userManagedAccountService)
        {
            this.accountRepository = accountRepository;
            this.uriService = uriService;
            this.identityService = identityService;
            this.localizer = localizer;
            this.userManagedAccountService = userManagedAccountService;
        }

        public async Task<ObjectResult> Handle(AccountBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new MultiStatusObjectResult();
            var operatorId = identityService.GetUserId();
            var resourcePartUri = uriService.GetUriWithoutQuery().URIUpperLevel();
            var accountIdArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0, len = accountIdArr.Count(); i < len; i++)
            {
                var accountId = accountIdArr[i];
                var uri = $"{resourcePartUri}/{accountId}";

                var account = await accountRepository.FindAsync(accountId);
                if (account == null)
                {
                    result.AddResult(uri, 404, "");
                    continue;
                }

                var query = await userManagedAccountService.GetManagedAccounts(operatorId);
                var canOperat = await query.AnyAsync(x => x.Id == accountId);
                if (!canOperat)
                {
                    result.AddResult(uri, 403, localizer["OperateForbidden"]);
                    continue;
                }


                await accountRepository.DeleteAsync(accountId, operatorId);
                result.AddResult(uri, 200, "");
            }

            return result.Transfer();
        }
    }
}
