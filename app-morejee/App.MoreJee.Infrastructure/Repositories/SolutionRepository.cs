using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class SolutionRepository: ISolutionRepository
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
        public SolutionRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Solution> FindAsync(string id)
        {
            return await _context.Set<Solution>().FindAsync(id);
        }

        public IQueryable<Solution> Get(ISpecification<Solution> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Solution>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Solution> Paging(IPagingSpecification<Solution> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Solution>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Solution entity)
        {
            _context.Set<Solution>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Solution entity)
        {
            _context.Set<Solution>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            _context.Set<Solution>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
