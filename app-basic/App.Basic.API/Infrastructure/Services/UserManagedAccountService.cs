using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.API.Infrastructure.Services
{
    public interface IUserManagedAccountService
    {
        Task<IQueryable<Account>> GetManagedAccounts(string accountId);

        Task<IQueryable<Account>> GetManagedAccountsByPaging(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone);
        Task<IQueryable<Account>> GetManagedPagingAccountsCount(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone);
    }


    public class UserManagedAccountService : IUserManagedAccountService
    {
        private readonly IAccountRepository accountRepository;

        public UserManagedAccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<IQueryable<Account>> GetManagedAccounts(string accountId)
        {
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOwnRolesAsync(account);

            //var bApplicationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.ApplicationManager.Id || x.RoleId == Role.ApplicationService.Id);
            //if (bApplicationAdmin)
            //    return accountRepository.Get(new GetApplicationAdminManagedAccountSpecification());

            //var bOrganizationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.OrganizationAdmin.Id);
            //if (bOrganizationAdmin)
            //    return accountRepository.Get(new GetOrganAdminManagedAccountSpecification(account.OrganizationId));

            return accountRepository.Get(new GetNormalUserManagedAccountSpecification(accountId));
        }

        public async Task<IQueryable<Account>> GetManagedAccountsByPaging(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOwnRolesAsync(account);

            //var bOrganizationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.OrganizationAdmin.Id);
            //if (bOrganizationAdmin)
            //    return accountRepository.Paging(new GetOrganAdminManagedAccountPagingSpecification(account.OrganizationId, page, pageSize, orderBy, desc, search, mail, phone));

            return accountRepository.Paging(new GetNormalUserManagedAccountPagingSpecification(accountId, page, pageSize, orderBy, desc, search, mail, phone));
        }

        public async Task<IQueryable<Account>> GetManagedPagingAccountsCount(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            var account = await accountRepository.FindAsync(accountId);
            await accountRepository.LoadOwnRolesAsync(account);

            //var bOrganizationAdmin = account.OwnRoles.Any(x => x.RoleId == Role.OrganizationAdmin.Id);
            //if (bOrganizationAdmin)
            //    return accountRepository.Get(new GetOrganAdminManagedAccountPagingSpecification(account.OrganizationId, page, pageSize, orderBy, desc, search, mail, phone));

            return accountRepository.Get(new GetNormalUserManagedAccountPagingSpecification(accountId, page, pageSize, orderBy, desc, search, mail, phone));
        }
    }
}
