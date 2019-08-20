using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupIdentityQueryHandler : IRequestHandler<ProductPermissionGroupIdentityQuery, ProductPermissionGroupIdentityQueryDTO>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        #region ctor
        public ProductPermissionGroupIdentityQueryHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<ProductPermissionGroupIdentityQueryDTO> Handle(ProductPermissionGroupIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductPermissionGroup", request.Id]);

            return ProductPermissionGroupIdentityQueryDTO.From(data);
        }
        #endregion
    }
}
