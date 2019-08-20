using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.Products
{

    public class UpdateProductIconHandler : INotificationHandler<ProductSpecIconUpdatedEvent>
    {
        private readonly IProductRepository productRepository;

        #region ctor
        public UpdateProductIconHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        #endregion

        #region Handle
        public async Task Handle(ProductSpecIconUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindAsync(notification.ProductId);
            if (product.DefaultProductSpecId != notification.ProductSpecId) return;

            product.UpdateIcon(notification.Icon);
            await productRepository.UpdateAsync(product);
        }
        #endregion
    }
}
