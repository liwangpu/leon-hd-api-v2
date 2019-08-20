using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    public class ProductCreatedEvent : INotification
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string SourcedStaticMeshId { get; protected set; }

        public ProductCreatedEvent(string id, string name, string sourcedStaticMeshId, string organizationId, string creator)
        {
            Id = id;
            Name = name;
            SourcedStaticMeshId = sourcedStaticMeshId;
            OrganizationId = organizationId;
            Creator = creator;
        }
    }
}
