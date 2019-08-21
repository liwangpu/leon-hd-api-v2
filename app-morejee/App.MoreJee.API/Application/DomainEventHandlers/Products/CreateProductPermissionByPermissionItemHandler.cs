using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Domain.Events.ProductEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.DomainEventHandlers.Products
{
    public class CreateProductPermissionByPermissionItemHandler : INotificationHandler<ProductPermissionItemCreatedEvent>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IProductPermissionRepository productPermissionRepository;

        #region ctor
        public CreateProductPermissionByPermissionItemHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IProductPermissionRepository productPermissionRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.productPermissionRepository = productPermissionRepository;
        }
        #endregion

        #region Handle
        public async Task Handle(ProductPermissionItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            var group = await productPermissionGroupRepository.FindAsync(notification.ProductPermissionGroupId);
            await productPermissionGroupRepository.LoadOwnOrganItemsAsync(group);

            foreach (var item in group.OwnOrganItems)
            {
                var exist = await productPermissionRepository.ExistAsync(notification.ProductId, item.OrganizationId, notification.ProductPermissionGroupId);
                //    //if (!exist)
                //    //{
                //    var permission = new ProductPermission(notification.ProductId, item.OrganizationId, notification.ProductPermissionGroupId);
                //    await productPermissionRepository.AddAsync(permission);
                //    //}
            }


        }
        #endregion
    }
}
