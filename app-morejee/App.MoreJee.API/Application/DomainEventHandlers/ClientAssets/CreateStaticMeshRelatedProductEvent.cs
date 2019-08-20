using MediatR;

namespace App.MoreJee.API.Application.DomainEventHandlers.ClientAssets
{

    public class CreateStaticMeshRelatedProductEvent : INotification
    {
        public string StaticMeshId { get; protected set; }
        public string StaticMeshName { get; protected set; }
        public string Creator { get; protected set; }
        public string OrganizationId { get; protected set; }

        public CreateStaticMeshRelatedProductEvent(string staticMeshId, string staticMeshName, string organizationId, string creator)
        {
            StaticMeshId = staticMeshId;
            StaticMeshName = staticMeshName;
            OrganizationId = organizationId;
            Creator = creator;
        }
    }
}
