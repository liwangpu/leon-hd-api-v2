using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class OrganizationAllPermissionProductQueryHandler : IRequestHandler<OrganizationAllPermissionProductQuery, List<OrganizationAllPermissionProductDTO>>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        #region ctor
        public OrganizationAllPermissionProductQueryHandler(IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
        }
        #endregion

        #region Handle
        public async Task<List<OrganizationAllPermissionProductDTO>> Handle(OrganizationAllPermissionProductQuery request, CancellationToken cancellationToken)
        {
            var list = await productPermissionGroupRepository.GetOrganizationAllPermissionProduct(request.OrganizationId, request.Search);
            return list;
        }
        #endregion
    }
}
