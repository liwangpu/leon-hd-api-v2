using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class PackageMapRepository : IPackageMapRepository
    {
        public MoreJeeAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public PackageMapRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<PackageMap> FindAsync(string id)
        {
            return await _context.Set<PackageMap>().FindAsync(id);
        }

        public IQueryable<PackageMap> Get(ISpecification<PackageMap> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<PackageMap>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<PackageMap> Paging(IPagingSpecification<PackageMap> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<PackageMap>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "ResourceId" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(PackageMap entity)
        {
            _context.Set<PackageMap>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(PackageMap entity)
        {
            _context.Set<PackageMap>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var entity = await FindAsync(id);
            if (entity == null) return;
            entity.DeleteClientAsset();
            _context.Set<PackageMap>().Remove(entity);
            await _context.SaveEntitiesAsync(false);
        }


        public async Task DeleteAsync(string resourceId)
        {
            var entity = await _context.Set<PackageMap>().FirstOrDefaultAsync(x => x.ResourceId == resourceId);
            if (entity == null) return;
            entity.DeleteClientAsset();
            _context.Set<PackageMap>().Remove(entity);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
