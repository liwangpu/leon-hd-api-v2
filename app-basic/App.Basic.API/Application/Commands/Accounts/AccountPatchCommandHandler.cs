using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountPatchCommandHandler : IRequestHandler<AccountPatchCommand>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public AccountPatchCommandHandler(IAccountRepository accountRepository, IIdentityService identityService, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.accountRepository = accountRepository;
            this.identityService = identityService;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        public async Task<Unit> Handle(AccountPatchCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(request.Id);
            if (account == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Account", request.Id]);

            var canOperatedQ = await accountRepository.GetManagedAccount(identityService.GetUserId());
            var canOperated = await canOperatedQ.AnyAsync(x => x.Id == request.Id);
            if (!canOperated)
                throw new HttpForbiddenException(commonLocalizer["OperateForbidden"]);

            mapper.Map(account, request);
            request.ApplyPatch();
            account.UpdateBasicInfo(request.FirstName, request.LastName, request.Description, request.Mail, request.Phone, identityService.GetUserId());
            accountRepository.Update(account);
            await accountRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }
    }
}
