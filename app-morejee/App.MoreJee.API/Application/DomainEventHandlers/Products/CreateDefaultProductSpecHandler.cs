using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.Products
{
    public class CreateDefaultProductSpecHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly IProductSpecRepository productSpecRepository;
        private readonly IProductRepository productRepository;

        #region ctor
        public CreateDefaultProductSpecHandler(IProductSpecRepository productSpecRepository, IProductRepository productRepository)
        {
            this.productSpecRepository = productSpecRepository;
            this.productRepository = productRepository;
        }
        #endregion

        #region Handler
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var spec = new ProductSpec(notification.Name, string.Empty, notification.Id, notification.OrganizationId, notification.Creator, notification.SourcedStaticMeshId);
            await productSpecRepository.AddAsync(spec);
            var product = await productRepository.FindAsync(notification.Id);
            product.SetDefaultProductSpec(spec);
            await productRepository.UpdateAsync(product);
        }
        #endregion
    }
}
