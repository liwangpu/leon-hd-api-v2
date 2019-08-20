using App.Base.API;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupOwnProductQueryHandler : IRequestHandler<ProductPermissionGroupOwnProductQuery, List<ProductPermissionGroupOwnProductItemDTO>>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        #region ctor
        public ProductPermissionGroupOwnProductQueryHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<List<ProductPermissionGroupOwnProductItemDTO>> Handle(ProductPermissionGroupOwnProductQuery request, CancellationToken cancellationToken)
        {
            var list = await productPermissionGroupRepository.QueryOwnProduct(request.ProductPermissionGroupId, request.Search);

            return list;
        }
        #endregion
    }
}
