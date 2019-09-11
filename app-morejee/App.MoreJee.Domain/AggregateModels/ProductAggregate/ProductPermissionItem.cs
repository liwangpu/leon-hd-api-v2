using App.MoreJee.Domain.Events.ProductEvents;
using App.MoreJee.Domain.SeedWork;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermissionItem : Entity
    {
        public string ProductId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }
        public ProductPermissionGroup ProductPermissionGroup { get; protected set; }

        #region ctor
        protected ProductPermissionItem()
        {

        }

        public ProductPermissionItem(string productId, string groupId)
        {
            ProductId = productId;
            ProductPermissionGroupId = groupId;
            AddDomainEvent(new ProductPermissionItemCreatedEvent(Id, ProductId, ProductPermissionGroupId));
        }
        #endregion
    }
}
