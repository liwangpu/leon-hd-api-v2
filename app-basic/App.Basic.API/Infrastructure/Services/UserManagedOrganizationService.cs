using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.OrganizationSpecifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.API.Infrastructure.Services
{
    public interface IUserManagedOrganizationService
    {
        Task<IQueryable<Organization>> GetManagedOrganizations(string accountId);

        Task<IQueryable<Organization>> GetManagedOrganizationsByPaging(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone);
    }

    /// <summary>
    /// 用户所管理的组织信息帮助类
    /// </summary>
    public class UserManagedOrganizationService : IUserManagedOrganizationService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IOrganizationRepository organizationRepository;

        #region ctor
        public UserManagedOrganizationService(IAccountRepository accountRepository, IOrganizationRepository organizationRepository)
        {
            this.accountRepository = accountRepository;
            this.organizationRepository = organizationRepository;
        }
        #endregion


        public async Task<IQueryable<Organization>> GetManagedOrganizations(string accountId)
        {
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOwnRolesAsync(account);


            //var bOrganizationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.OrganizationAdmin.Id);
            //if (bOrganizationAdmin)
            //    return organizationRepository.Get(new GetOrganAdminManagedOrganizationSpecification(account.OrganizationId));

            return new List<Organization>().AsQueryable();
        }

        public async Task<IQueryable<Organization>> GetManagedOrganizationsByPaging(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOwnRolesAsync(account);


            //var bOrganizationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.OrganizationAdmin.Id);
            //if (bOrganizationAdmin)
            //    return organizationRepository.Paging(new GetOrganAdminManagedOrganizationPagingSpecification(account.OrganizationId, page, pageSize, orderBy, desc, search, mail, phone));

            return new List<Organization>().AsQueryable();
        }
    }
}
