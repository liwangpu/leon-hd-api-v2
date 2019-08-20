using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.Products
{
    public class UpdateProductPriceRateHandler : INotificationHandler<ProductSpecPriceUpdatedEvent>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductPriceRateHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Handle(ProductSpecPriceUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindAsync(notification.ProductId);
            await productRepository.LoadOwnProductSpecsAsync(product);

            var minPrice = product.OwnProductSpecs.Min(x => x.Price);
            var maxPrice = product.OwnProductSpecs.Max(x => x.Price);
            product.UpdatePriceRate(minPrice, maxPrice);

            var minPartnerPrice = product.OwnProductSpecs.Min(x => x.PartnerPrice);
            var maxPartnerPrice = product.OwnProductSpecs.Max(x => x.PartnerPrice);
            product.UpdatePartnerPriceRate(minPartnerPrice, maxPartnerPrice);

            var minPurchasePrice = product.OwnProductSpecs.Min(x => x.PurchasePrice);
            var maxPurchasePrice = product.OwnProductSpecs.Max(x => x.PurchasePrice);
            product.UpdatePurchasePriceRate(minPurchasePrice, maxPurchasePrice);

            await productRepository.UpdateAsync(product);
        }
    }
}
