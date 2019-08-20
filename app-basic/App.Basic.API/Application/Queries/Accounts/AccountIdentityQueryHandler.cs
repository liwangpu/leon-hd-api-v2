using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountIdentityQueryHandler : IRequestHandler<AccountIdentityQuery, AccountIdentityQueryDTO>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        public AccountIdentityQueryHandler(IAccountRepository accountRepository, IStringLocalizer<CommonTranslation> localizer)
        {
            this.accountRepository = accountRepository;
            this.localizer = localizer;
        }

        public async Task<AccountIdentityQueryDTO> Handle(AccountIdentityQuery request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.Id);
            if (account == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "Account", request.Id]);

            return AccountIdentityQueryDTO.From(account);
        }
    }
}
