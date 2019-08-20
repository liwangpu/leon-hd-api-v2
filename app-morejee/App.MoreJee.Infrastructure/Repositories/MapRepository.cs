using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class MapRepository : IMapRepository
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
        public MapRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Map> FindAsync(string id)
        {
            return await _context.Set<Map>().FindAsync(id);
        }

        public IQueryable<Map> Get(ISpecification<Map> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Map>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Map> Paging(IPagingSpecification<Map> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Map>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Map entity)
        {
            _context.Set<Map>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Map entity)
        {
            _context.Set<Map>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            data.DeleteClientAsset();
            _context.Set<Map>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(Map data, string operatorId)
        {
            data.DeleteClientAsset();
            _context.Set<Map>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
