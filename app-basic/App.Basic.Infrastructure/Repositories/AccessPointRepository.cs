using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Infrastructure.Repositories
{
    public class AccessPointRepository : IAccessPointRepository
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
        public AccessPointRepository(BasicAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<AccessPoint> FindAsync(string id)
        {
            return await _context.Set<AccessPoint>().FindAsync(id);
        }

        public async Task<AccessPoint> FindByPointKeyAsync(string key)
        {
            var id = await _context.Set<AccessPoint>().Where(x => x.PointKey == key).Select(x => x.Id).FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(id)) return null;

            return await FindAsync(id);
        }

        public IQueryable<AccessPoint> Get(ISpecification<AccessPoint> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<AccessPoint>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<AccessPoint> Paging(IPagingSpecification<AccessPoint> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<AccessPoint>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "PointKey" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public void Add(AccessPoint entity)
        {
            entity._CustomizeId(GuidGenerator.NewGUID());
            _context.Set<AccessPoint>().Add(entity);
        }

        public void Update(AccessPoint entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var acc = await _context.Set<AccessPoint>().FirstOrDefaultAsync(x => x.Id == id);
            if (acc != null)
            {
                _context.Set<AccessPoint>().Remove(acc);
                await _context.SaveEntitiesAsync();
            }
        }
    }
}
