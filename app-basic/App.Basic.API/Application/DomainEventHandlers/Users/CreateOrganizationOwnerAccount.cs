using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Domain.Events.UserEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.DomainEventHandlers.Users
{
    /// <summary>
    /// 新建组织后,自动创建组织法定负责人
    /// </summary>
    public class CreateOrganizationOwnerAccount : INotificationHandler<OrganizationCreatedEvent>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IOrganizationRepository organizationRepository;

        public CreateOrganizationOwnerAccount(IAccountRepository accountRepository, IOrganizationRepository organizationRepository)
        {
            this.accountRepository = accountRepository;
            this.organizationRepository = organizationRepository;
        }

        public async Task Handle(OrganizationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var systemRoleId = SystemRole.NormalUser.Id;

            if (notification.Organization.OrganizationTypeId == OrganizationType.ServiceProvider.Id)
                systemRoleId = SystemRole.ApplicationManager.Id;
            else if (notification.Organization.OrganizationTypeId == OrganizationType.Brand.Id)
                systemRoleId = SystemRole.BrandOrganizationAdmin.Id;
            else if (notification.Organization.OrganizationTypeId == OrganizationType.Partner.Id)
                systemRoleId = SystemRole.PartnerOrganizationAdmin.Id;
            else if (notification.Organization.OrganizationTypeId == OrganizationType.Supplier.Id)
                systemRoleId = SystemRole.SupplierOrganizationAdmin.Id;
            else { }

            var owner = new Account("Admin", "", MD5Gen.CalcString(DomainPasswordConst.NormalPassword), notification.Organization.Mail, notification.Organization.Phone, systemRoleId, notification.Organization.Id, DomainEntityDefaultIdConst.SoftwareProviderAdminId);
            owner.SignLegalPerson();

            //如果是软件供应商,设置一下管理员的id
            if (notification.Organization.Id == DomainEntityDefaultIdConst.SoftwareProviderOrganizationId)
                owner.CustomizeId(DomainEntityDefaultIdConst.SoftwareProviderAdminId);
            await accountRepository.AddAsync(owner);

            var organ = await organizationRepository.FindAsync(owner.OrganizationId);
            organ.SetOwner(owner.Id);
            await organizationRepository.UpdateAsync(organ);

            //var systemRoleId = SystemRole.BrandOrganizationAdmin.Id;
            ////如果是软件供应商,设置一下管理员的默认角色Id
            //if (notification.Organization.Id == DomainEntityDefaultIdConst.SoftwareProviderOrganizationId)
            //    systemRoleId = SystemRole.ApplicationManager.Id;

            //var owner = new Account("Admin", MD5Gen.CalcString(DomainPasswordConst.NormalPassword), notification.Organization.Mail, notification.Organization.Phone, systemRoleId, notification.Organization.Id, DomainEntityDefaultIdConst.SoftwareProviderAdminId);
            //owner.SignLegalPerson();
            ////如果是软件供应商,设置一下管理员的id
            //if (notification.Organization.Id == DomainEntityDefaultIdConst.SoftwareProviderOrganizationId)
            //    owner.CustomizeId(DomainEntityDefaultIdConst.SoftwareProviderAdminId);
            //await accountRepository.AddAsync(owner);

            //var organ = await organizationRepository.FindAsync(owner.OrganizationId);
            //organ.SetOwner(owner.Id);
            //await organizationRepository.UpdateAsync(organ);
        }
    }
}
