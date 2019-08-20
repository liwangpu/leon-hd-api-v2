using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRepository
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
        public MaterialRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Material> FindAsync(string id)
        {
            return await _context.Set<Material>().FindAsync(id);
        }

        public IQueryable<Material> Get(ISpecification<Material> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Material>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Material> Paging(IPagingSpecification<Material> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Material>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Material entity)
        {
            _context.Set<Material>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Material entity)
        {
            _context.Set<Material>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            data.DeleteClientAsset();
            _context.Set<Material>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(Material data, string operatorId)
        {
            data.DeleteClientAsset();
            _context.Set<Material>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }

}
