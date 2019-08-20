using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.Domain.Consts;
using App.Basic.Export;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductIdentityQueryHandler : IRequestHandler<ProductIdentityQuery, ProductIdentityQueryDTO>
    {
        private readonly IProductRepository productRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly ICategoryRepository categoryRepository;
        private readonly AccountService accountService;

        #region ctor
        public ProductIdentityQueryHandler(IProductRepository productRepository, IStringLocalizer<CommonTranslation> commonLocalizer, ICategoryRepository categoryRepository, AccountService accountService)
        {
            this.productRepository = productRepository;
            this.commonLocalizer = commonLocalizer;
            this.categoryRepository = categoryRepository;
            this.accountService = accountService;
        }
        #endregion

        #region Handle
        public async Task<ProductIdentityQueryDTO> Handle(ProductIdentityQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindAsync(request.Id);
            await productRepository.LoadOwnProductSpecsAsync(product);
            if (product == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Product", request.Id]);
            var dto = ProductIdentityQueryDTO.From(product);
            var pointKey = await accountService.GetAccessPoint();
            var showPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PriceRetrieve);
            var showPartnerPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PartnerPriceRetrieve);
            var showPurchasePrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PurchasePriceRetrieve);

            if (!showPrice)
                dto.HidePrice();

            if (!showPartnerPrice)
                dto.HidePartnerPrice();

            if (!showPurchasePrice)
                dto.HidePurchasePrice();

            dto.CategoryName = await categoryRepository.GetCategoryName(product.CategoryId);
            return dto;
        }
        #endregion
    }
}
