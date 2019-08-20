using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Infrastructure.Specifications.ProductPermissionGroupSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupPagingQueryHandler : IRequestHandler<ProductPermissionGroupPagingQuery, PagingQueryResult<ProductPermissionGroupPagingQueryDTO>>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public ProductPermissionGroupPagingQueryHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IIdentityService identityService)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<ProductPermissionGroupPagingQueryDTO>> Handle(ProductPermissionGroupPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<ProductPermissionGroupPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new ProductPermissionGroupPagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            result.Total = await productPermissionGroupRepository.Get(specification).CountAsync();
            result.Data = await productPermissionGroupRepository.Paging(specification).Select(x => ProductPermissionGroupPagingQueryDTO.From(x)).ToListAsync();
            return result;
        }
        #endregion
    }
}
