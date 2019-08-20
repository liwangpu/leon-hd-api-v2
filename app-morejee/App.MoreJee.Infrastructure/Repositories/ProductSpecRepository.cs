using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class ProductSpecRepository : IProductSpecRepository
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
        public ProductSpecRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        protected EntityEntry<ProductSpec> Entry(ProductSpec entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadProductAsync(ProductSpec entity)
        {
            await Entry(entity).Reference(x => x.Product).LoadAsync();
        }

        public async Task<ProductSpec> FindAsync(string id)
        {
            return await _context.Set<ProductSpec>().FindAsync(id);
        }

        public IQueryable<ProductSpec> Get(ISpecification<ProductSpec> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<ProductSpec>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<ProductSpec> Paging(IPagingSpecification<ProductSpec> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<ProductSpec>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(ProductSpec entity)
        {
            _context.Set<ProductSpec>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(ProductSpec entity)
        {
            _context.Set<ProductSpec>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public Task DeleteAsync(string id, string operatorId)
        {
            throw new System.NotImplementedException();
        }


    }
}
