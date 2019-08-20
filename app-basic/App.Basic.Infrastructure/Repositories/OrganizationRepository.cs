using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
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
        public OrganizationRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion

        protected EntityEntry<Organization> Entry(Organization entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadOwnAccountsAsync(Organization entity)
        {
            await Entry(entity).Collection(x => x.OwnAccounts).LoadAsync();
        }

        public async Task<Organization> FindAsync(string id)
        {
            return await _context.Set<Organization>().FindAsync(id);
        }

        public IQueryable<Organization> Get(ISpecification<Organization> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Organization>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Organization> Paging(IPagingSpecification<Organization> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<Organization>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "modifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Organization entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.ParentId) && entity.ParentId != DomainEntityDefaultIdConst.SoftwareProviderOrganizationId)
            {
                var parentCat = await FindAsync(entity.ParentId);
                entity.SetFingerprint(parentCat.Fingerprint);
                entity.SetLValue(parentCat.RValue);
                entity.SetRValue(entity.LValue + 1);

                #region 改动相关节点左右值信息
                {
                    var affectCats = await _context.Set<Organization>().Where(x => x.Fingerprint == parentCat.Fingerprint && x.RValue >= parentCat.RValue).ToListAsync();

                    for (var idx = affectCats.Count - 1; idx >= 0; idx--)
                    {
                        var cat = affectCats[idx];
                        cat.SetRValue(cat.RValue + 2);
                        if (cat.LValue > parentCat.LValue)
                            cat.SetLValue(cat.LValue + 2);
                        _context.Set<Organization>().Update(cat);
                    }
                }
                #endregion

            }
            else
            {
                //全新节点
                entity.SetLValue(1);
                entity.SetRValue(2);
                entity.SetFingerprint();
            }

            _context.Set<Organization>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Organization entity)
        {
            _context.Set<Organization>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var organ = await FindAsync(id);
            organ.Delete(operatorId);
            _context.Set<Organization>().Update(organ);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Organization> GetManagedOrganization(string userId)
        {
            var userOrganQuery = _context.Set<Account>().Where(x => x.Id == userId && x.SystemRoleId <= SystemRole.BrandOrganizationAdmin.Id).Select(x => x.OrganizationId);

            return from it in _context.Set<Organization>()
                   join oid in userOrganQuery on it.ParentId equals oid
                   select it;
        }

        public IQueryable<Organization> GetTopTreeOrganization(string organId)
        {
            var fingerPrintQ = from it in _context.Set<Organization>()
                               where it.Id == organId
                               select it.Fingerprint;
            return from it in _context.Set<Organization>()
                   where fingerPrintQ.Contains(it.Fingerprint) && it.LValue == 1
                   select it;
        }
    }
}
