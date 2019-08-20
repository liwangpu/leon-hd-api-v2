using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountPagingQueryHandler : IRequestHandler<AccountPagingQuery, PagingQueryResult<AccountPagingQueryDTO>>
    {
        private readonly IIdentityService identityService;
        private readonly IAccountRepository accountRepository;

        #region ctor
        public AccountPagingQueryHandler(IIdentityService identityService, IAccountRepository accountRepository)
        {
            this.identityService = identityService;
            this.accountRepository = accountRepository;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<AccountPagingQueryDTO>> Handle(AccountPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<AccountPagingQueryDTO>();
            request.CheckPagingParam();
            var currentUserId = identityService.GetUserId();

            var specification = new AccountPagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            result.Total = await accountRepository.Get(specification).CountAsync();
            result.Data = await accountRepository.Paging(specification).Select(x => AccountPagingQueryDTO.From(x)).ToListAsync();
            return result;
        } 
        #endregion
    }
}
