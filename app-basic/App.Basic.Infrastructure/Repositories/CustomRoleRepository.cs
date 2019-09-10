using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace App.Basic.Infrastructure.Repositories
{
    public class CustomRoleRepository : ICustomRoleRepository
    {
        public BasicAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        IUnitOfWork IRepository<CustomRole>.UnitOfWork => throw new System.NotImplementedException();

        #region ctor
        public CustomRoleRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<CustomRole> FindAsync(string id)
        {
            return await _context.Set<CustomRole>().FindAsync(id);
        }

        public IQueryable<CustomRole> Get(ISpecification<CustomRole> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<
                CustomRole>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<CustomRole> Paging(IPagingSpecification<CustomRole> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<CustomRole>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "name" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public void Add(CustomRole entity)
        {
            entity._CustomizeId(GuidGenerator.NewGUID());
            _context.Set<CustomRole>().Add(entity);
        }

        public void Update(CustomRole entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var CustomRole = await FindAsync(id);
            _context.Set<CustomRole>().Update(CustomRole);
            await _context.SaveEntitiesAsync();
        }
    }
}
