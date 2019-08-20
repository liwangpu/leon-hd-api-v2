using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Events.UserEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.DomainEventHandlers.Users
{
    public class CreateOrganizationTreeNode : INotificationHandler<OrganizationCreatedEvent>
    {
        private readonly IOrganizationTreeRepository organTreeRepositoy;

        #region ctor
        public CreateOrganizationTreeNode(IOrganizationTreeRepository organTreeRepositoy)
        {
            this.organTreeRepositoy = organTreeRepositoy;
        }
        #endregion

        public async Task Handle(OrganizationCreatedEvent notification, CancellationToken cancellationToken)
        {

            var treeNode = new OrganizationTree(notification.Organization.Id, notification.Organization.Name, notification.Organization.OrganizationTypeId.ToString(), notification.Organization.ParentId);
            await organTreeRepositoy.AddAsync(treeNode);
        }
    }
}
