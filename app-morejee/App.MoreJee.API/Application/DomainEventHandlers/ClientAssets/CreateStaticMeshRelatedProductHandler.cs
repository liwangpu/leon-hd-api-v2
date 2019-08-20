using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.ClientAssets
{
    public class CreateStaticMeshRelatedProductHandler : INotificationHandler<CreateStaticMeshRelatedProductEvent>
    {
        private readonly IProductRepository productRepository;

        #region ctor
        public CreateStaticMeshRelatedProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        #endregion

        #region Handle
        public async Task Handle(CreateStaticMeshRelatedProductEvent notification, CancellationToken cancellationToken)
        {
            var product = new Product(notification.StaticMeshName, string.Empty, string.Empty, string.Empty, notification.OrganizationId, notification.Creator, notification.StaticMeshId);
            await productRepository.AddAsync(product);
        }
        #endregion
    }
}
