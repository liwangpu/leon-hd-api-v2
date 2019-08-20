using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Consts;
using App.Basic.Export;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductSpecs
{
    public class ProductSpecPatchCommandHandler : IRequestHandler<ProductSpecPatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IProductSpecRepository productSpecRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly AccountService accountService;

        public ProductSpecPatchCommandHandler(IIdentityService identityService, IProductSpecRepository productSpecRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, AccountService accountService)
        {
            this.identityService = identityService;
            this.productSpecRepository = productSpecRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.accountService = accountService;
        }
        public async Task<Unit> Handle(ProductSpecPatchCommand request, CancellationToken cancellationToken)
        {
            var spec = await productSpecRepository.FindAsync(request.Id);
            if (spec == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductSpec", request.Id]);

            var pointKey = await accountService.GetAccessPoint();
            var canEditPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PriceEdit);
            var canEditPartnerPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PartnerPriceEdit);
            var canEditPurchasePrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PurchasePriceRetrieve);

            if (!canEditPrice)
                request.DisablePriceChange();
            if (!canEditPartnerPrice)
                request.DisablePartnerPriceChange();
            if (!canEditPurchasePrice)
                request.DisablePurchasePriceChange();

            mapper.Map(spec, request);
            request.ApplyPatch();


            var modifier = identityService.GetUserId();
            spec.UpdateBasicInfo(request.Name, request.Description, modifier);
            spec.UpdatePriceInfo(request.Price, request.PartnerPrice, request.PurchasePrice);
            spec.UpdateIcon(request.Icon);
            await productSpecRepository.UpdateAsync(spec);
            return Unit.Value;
        }
    }
}
