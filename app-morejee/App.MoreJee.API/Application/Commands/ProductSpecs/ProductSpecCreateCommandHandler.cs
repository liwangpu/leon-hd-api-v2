using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductSpecs
{
    public class ProductSpecCreateCommandHandler : IRequestHandler<ProductSpecCreateCommand, string>
    {
        private readonly IProductSpecRepository productSpecRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public ProductSpecCreateCommandHandler(IProductSpecRepository productSpecRepository, IIdentityService identityService)
        {
            this.productSpecRepository = productSpecRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(ProductSpecCreateCommand request, CancellationToken cancellationToken)
        {
            var spec = new ProductSpec(request.Name, request.Description, request.ProductId, identityService.GetOrganizationId(), identityService.GetUserId());
            spec.UpdatePriceInfo(request.Price, request.PartnerPrice, request.PurchasePrice);
            await productSpecRepository.AddAsync(spec);
            return spec.Id;
        }
        #endregion
    }
}
