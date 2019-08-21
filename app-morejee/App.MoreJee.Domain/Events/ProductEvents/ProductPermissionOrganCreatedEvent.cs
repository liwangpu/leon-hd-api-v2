using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    public class ProductPermissionOrganCreatedEvent : INotification
    {
        public string Id { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }

        #region ctor
        public ProductPermissionOrganCreatedEvent(string id, string organizationId, string productPermissionGroupId)
        {
            Id = id;
            OrganizationId = organizationId;
            ProductPermissionGroupId = productPermissionGroupId;
        }
        #endregion
    }
}
