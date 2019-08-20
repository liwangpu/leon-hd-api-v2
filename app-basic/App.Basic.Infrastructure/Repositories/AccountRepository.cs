using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Base.Infrastructure;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public BasicAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public AccountRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion

        protected EntityEntry<Account> Entry(Account entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadOrganizationAsync(Account entity)
        {
            await Entry(entity).Reference(x => x.Organization).LoadAsync();
        }

        public async Task LoadOwnRolesAsync(Account entity)
        {
            await Entry(entity).Collection(x => x.OwnRoles).LoadAsync();
        }

        public async Task<Account> FindAsync(string id)
        {
            return await _context.Set<Account>().FindAsync(id);
        }

        public IQueryable<Account> Get(ISpecification<Account> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Account>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Account> Paging(IPagingSpecification<Account> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<Account>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "modifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Account entity)
        {
            _context.Set<Account>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Account entity)
        {
            _context.Set<Account>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var account = await FindAsync(id);
            account.Delete(operatorId);
            _context.Set<Account>().Update(account);
            await _context.SaveEntitiesAsync();
        }

        public async Task<IQueryable<Account>> GetManagedAccount(string userId)
        {
            var userOrganId = await _context.Set<Account>().Where(x => x.Id == userId && x.SystemRoleId <= SystemRole.BrandOrganizationAdmin.Id).Select(x => x.OrganizationId).FirstOrDefaultAsync();
            if (userOrganId == null)
                return _context.Set<Account>().Take(0);


            var userSubOrganIds = await _context.Set<Organization>().Where(x => x.ParentId == userOrganId).Select(x => x.Id).ToListAsync();

            return _context.Set<Account>().Where(x => (x.OrganizationId == userOrganId) || (x.LegalPerson == EntityStateConst.Yes && userSubOrganIds.Contains(x.OrganizationId)));
        }
    }
}
