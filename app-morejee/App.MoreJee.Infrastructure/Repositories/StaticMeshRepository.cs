using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class StaticMeshRepository : IStaticMeshRepository
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
        public StaticMeshRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<StaticMesh> FindAsync(string id)
        {
            return await _context.Set<StaticMesh>().FindAsync(id);
        }

        public IQueryable<StaticMesh> Get(ISpecification<StaticMesh> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<StaticMesh>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<StaticMesh> Paging(IPagingSpecification<StaticMesh> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<StaticMesh>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(StaticMesh entity)
        {
            _context.Set<StaticMesh>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(StaticMesh entity)
        {
            _context.Set<StaticMesh>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            data.DeleteClientAsset();
            _context.Set<StaticMesh>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(StaticMesh data, string operatorId)
        {
            data.DeleteClientAsset();
            _context.Set<StaticMesh>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
