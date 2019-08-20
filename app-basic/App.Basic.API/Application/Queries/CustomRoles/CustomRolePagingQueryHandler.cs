using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Infrastructure.Specifications.CustomRoleSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.CustomRoles
{
    public class CustomRolePagingQueryHandler : IRequestHandler<CustomRolePagingQuery, PagingQueryResult<CustomRolePagingQueryDTO>>
    {
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public CustomRolePagingQueryHandler(ICustomRoleRepository customRoleRepository, IIdentityService identityService)
        {
            this.customRoleRepository = customRoleRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<CustomRolePagingQueryDTO>> Handle(CustomRolePagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<CustomRolePagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new CustomRolePagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            result.Total = await customRoleRepository.Get(specification).CountAsync();
            result.Data = await customRoleRepository.Paging(specification).Select(x => CustomRolePagingQueryDTO.From(x)).ToListAsync(); ;
            return result;
        }
        #endregion
    }
}
