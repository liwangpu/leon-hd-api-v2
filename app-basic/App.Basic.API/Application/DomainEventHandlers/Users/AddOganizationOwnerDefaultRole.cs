using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Events.UserEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.DomainEventHandlers.Users
{
    /// <summary>
    /// 为组织法人创建默认用户角色
    /// </summary>
    public class AddOganizationOwnerDefaultRole : INotificationHandler<OrganizationOwnerSettingEvent>
    {
        private readonly IAccountRepository accountRepository;

        public AddOganizationOwnerDefaultRole(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task Handle(OrganizationOwnerSettingEvent notification, CancellationToken cancellationToken)
        {
            var account = await accountRepository.FindAsync(notification.AccountId);
            await accountRepository.LoadOrganizationAsync(account);

            if (notification.OrganizationTypeId == OrganizationType.ServiceProvider.Id)
            {
                account.AddRole(Role.ApplicationManager.Id, true);
                account.AddRole(Role.OrganizationAdmin.Id, true);
            }
            else
            {
                account.AddRole(Role.OrganizationAdmin.Id, true);
            }

            await accountRepository.UpdateAsync(account);
        }
    }
}
