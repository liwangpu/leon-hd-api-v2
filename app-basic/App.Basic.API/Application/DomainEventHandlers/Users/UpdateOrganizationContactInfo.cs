using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Events.UserEvents;
using App.Basic.Infrastructure.Specifications.OrganizationSpecifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.DomainEventHandlers.Users
{
    public class UpdateOrganizationContactInfo : INotificationHandler<AccountUpdateContactInfoEvent>
    {
        private readonly IOrganizationRepository organizationRepository;

        public UpdateOrganizationContactInfo(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task Handle(AccountUpdateContactInfoEvent notification, CancellationToken cancellationToken)
        {
            var userIsOrganizationOwner = organizationRepository.Get(new OrganizationOwnerCheckSpecification(notification.OranizationId, notification.AccountId)).Count() > 0;
            if (!userIsOrganizationOwner)
                return;

            var organization = await organizationRepository.FindAsync(notification.OranizationId);
            if (organization != null)
            {
                organization.UpdateContactInfo(notification.Mail, notification.Phone);
                await organizationRepository.UpdateAsync(organization);
            }
        }
    }
}
