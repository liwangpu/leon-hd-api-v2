using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountCreateCommandHandler : IRequestHandler<AccountCreateCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IIdentityService _identityService;

        public AccountCreateCommandHandler(IAccountRepository accountRepository, IIdentityService identityService)
        {
            _accountRepository = accountRepository;
            _identityService = identityService;
        }

        public async Task<string> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.FirstName, request.LastName, request.Password, request.Mail, request.Phone, SystemRole.NormalUser.Id, _identityService.GetOrganizationId(), _identityService.GetUserId());
            await _accountRepository.AddAsync(account);
            return account.Id;
        }
    }
}
