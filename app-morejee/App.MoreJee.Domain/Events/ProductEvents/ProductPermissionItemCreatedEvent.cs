using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    public class ProductPermissionItemCreatedEvent : INotification
    {
        public string Id { get; protected set; }
        public string ProductId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }

        #region ctor
        public ProductPermissionItemCreatedEvent(string id, string productId, string productPermissionGroupId)
        {
            Id = id;
            ProductId = productId;
            ProductPermissionGroupId = productPermissionGroupId;
        }
        #endregion
    }
}
