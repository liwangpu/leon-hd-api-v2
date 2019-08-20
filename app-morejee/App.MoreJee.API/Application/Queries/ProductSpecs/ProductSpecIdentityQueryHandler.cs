using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.Domain.Consts;
using App.Basic.Export;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.ProductSpecs
{
    public class ProductSpecIdentityQueryHandler : IRequestHandler<ProductSpecIdentityQuery, ProductSpecIdentityQueryDTO>
    {
        private readonly IProductSpecRepository productSpecRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;
        private readonly AccountService accountService;

        public ProductSpecIdentityQueryHandler(IProductSpecRepository productSpecRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper, AccountService accountService)
        {
            this.productSpecRepository = productSpecRepository;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<ProductSpecIdentityQueryDTO> Handle(ProductSpecIdentityQuery request, CancellationToken cancellationToken)
        {
            var spec = await productSpecRepository.FindAsync(request.Id);
            if (spec == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductSpec", request.Id]);

            var dto = mapper.Map<ProductSpecIdentityQueryDTO>(spec);
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
            return dto;
        }
    }
}
