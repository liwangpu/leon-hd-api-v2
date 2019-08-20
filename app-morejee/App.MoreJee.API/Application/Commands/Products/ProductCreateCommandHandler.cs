using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, string>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public ProductCreateCommandHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Brand, request.Unit, identityService.GetOrganizationId(), identityService.GetUserId());
            await productRepository.AddAsync(product);
            return product.Id;
        }
        #endregion

    }
}
