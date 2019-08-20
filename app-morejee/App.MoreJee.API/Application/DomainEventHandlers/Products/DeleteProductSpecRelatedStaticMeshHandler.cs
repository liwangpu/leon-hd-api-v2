using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.Products
{
    public class DeleteProductSpecRelatedStaticMeshHandler : INotificationHandler<DeleteProductSpecRelatedStaticMeshEvent>
    {
        private readonly IStaticMeshRepository staticMeshRepository;

        public DeleteProductSpecRelatedStaticMeshHandler(IStaticMeshRepository staticMeshRepository)
        {
            this.staticMeshRepository = staticMeshRepository;
        }

        public async Task Handle(DeleteProductSpecRelatedStaticMeshEvent notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.StaticMeshIds)) return;

            var idArr = notification.StaticMeshIds.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var id in idArr)
            {
                var data = await staticMeshRepository.FindAsync(id);
                await staticMeshRepository.DeleteAsync(data, string.Empty);
            }
        }
    }
}
