using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    public class OrganizationCreatedEvent : INotification
    {
        public Organization Organization { get; protected set; }
        public OrganizationCreatedEvent(Organization organ)
        {
            Organization = organ;
        }
    }
}
