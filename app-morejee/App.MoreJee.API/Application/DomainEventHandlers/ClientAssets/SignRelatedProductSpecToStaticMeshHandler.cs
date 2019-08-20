using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.ClientAssets
{
    public class SignRelatedProductSpecToStaticMeshHandler : INotificationHandler<ProductSpecCreatedEvent>
    {
        private readonly IStaticMeshRepository staticMeshRepository;

        #region ctor
        public SignRelatedProductSpecToStaticMeshHandler(IStaticMeshRepository staticMeshRepository)
        {
            this.staticMeshRepository = staticMeshRepository;
        }
        #endregion

        #region Handle
        public async Task Handle(ProductSpecCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.SourcedStaticMeshId))
                return;

            var mesh = await staticMeshRepository.FindAsync(notification.SourcedStaticMeshId);
            mesh.SignRelatedProductSpec(notification.Id);
            await staticMeshRepository.UpdateAsync(mesh);
        } 
        #endregion
    }
}
