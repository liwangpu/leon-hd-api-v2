using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Domain.Events.ClientAssetEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.ClientAssets
{
    public class DeleteRelatedPackageMapHandler : INotificationHandler<ClientAssetDeleteEvent>
    {
        private readonly IPackageMapRepository packageMapRepository;

        public DeleteRelatedPackageMapHandler(IPackageMapRepository packageMapRepository)
        {
            this.packageMapRepository = packageMapRepository;
        }

        public async Task Handle(ClientAssetDeleteEvent notification, CancellationToken cancellationToken)
        {
            await packageMapRepository.DeleteAsync(notification.ResourceId);
        }
    }
}
