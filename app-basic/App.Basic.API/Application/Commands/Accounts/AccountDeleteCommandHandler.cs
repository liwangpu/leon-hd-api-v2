using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountDeleteCommandHandler : IRequestHandler<AccountDeleteCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IIdentityService _identityService;

        #region ctor
        public AccountDeleteCommandHandler(IAccountRepository accountRepository, IIdentityService identityService)
        {
            _accountRepository = accountRepository;
            _identityService = identityService;
        }
        #endregion

        public async Task<Unit> Handle(AccountDeleteCommand request, CancellationToken cancellationToken)
        {
            await _accountRepository.DeleteAsync(request.Id, _identityService.GetUserId());
            return Unit.Value;
        }
    }
}
