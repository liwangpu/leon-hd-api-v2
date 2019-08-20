using MediatR;

namespace App.MoreJee.Domain.Events.CategoryEvents
{
    public class CategoryCreatedEvent : INotification
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Resource { get; protected set; }
        public string Description { get; protected set; }
        public string Icon { get; protected set; }
        public string Fingerprint { get; protected set; }
        public string OrganizationId { get; protected set; }

        public CategoryCreatedEvent(string id, string name, string resource, string description, string icon, string organizationId, string fingerPrint)
        {
            Id = id;
            Name = name;
            Resource = resource;
            Description = description;
            Icon = icon;
        }

    }
}
