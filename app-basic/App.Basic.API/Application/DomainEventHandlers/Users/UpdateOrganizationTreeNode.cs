using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Events.UserEvents;
using App.Basic.Infrastructure.Specifications.OrganizationTreeSpecifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.DomainEventHandlers.Users
{
    public class UpdateOrganizationTreeNode : INotificationHandler<OrganizationUpdatedEvent>
    {
        private readonly IOrganizationTreeRepository organTreeRepositoy;

        #region ctor
        public UpdateOrganizationTreeNode(IOrganizationTreeRepository organTreeRepositoy)
        {
            this.organTreeRepositoy = organTreeRepositoy;
        }
        #endregion

        #region Handle
        public async Task Handle(OrganizationUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var treeNode = organTreeRepositoy.Get(new GetOrganizationTreeNodeByObjIdSpecification(notification.Organization.Id)).First();
            treeNode.UpdateNodeName(notification.Organization.Name);
            await organTreeRepositoy.UpdateAsync(treeNode);
        } 
        #endregion
    }
}
