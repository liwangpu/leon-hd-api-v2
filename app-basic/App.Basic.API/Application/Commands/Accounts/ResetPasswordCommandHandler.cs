using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        public ResetPasswordCommandHandler(IAccountRepository accountRepository,  IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.accountRepository = accountRepository;
            this.identityService = identityService;
            this.commonLocalizer = commonLocalizer;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var currrentUserId = identityService.GetUserId();
            var canOperatedQ = await accountRepository.GetManagedAccount(currrentUserId);
            var canOperated = await canOperatedQ.AnyAsync(x => x.Id == request.AccountId);
            if (!canOperated)
                throw new HttpForbiddenException(commonLocalizer["OperateForbidden"]);

            var account = await accountRepository.FindAsync(request.AccountId);
            account.ChangePassword(request.Password, currrentUserId);
            await accountRepository.UpdateAsync(account);
            return Unit.Value;
        }
    }
}
