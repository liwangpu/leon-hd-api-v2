using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    public class OrganizationUpdatedEvent : INotification
    {
        public Organization Organization { get; protected set; }

        public OrganizationUpdatedEvent(Organization organ)
        {
            Organization = organ;
        }
    }
}
