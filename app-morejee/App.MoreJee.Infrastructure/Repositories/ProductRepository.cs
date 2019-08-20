using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
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
        public ProductRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        protected EntityEntry<Product> Entry(Product entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadOwnProductSpecsAsync(Product entity)
        {
            if (entity == null) return;
            await Entry(entity).Collection(x => x.OwnProductSpecs).LoadAsync();
        }

        public async Task<Product> FindAsync(string id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public IQueryable<Product> Get(ISpecification<Product> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Product>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Product> Paging(IPagingSpecification<Product> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);

            /*
             * 这里的分页查询用到两个扩展参数
             * Ext1用来存储组织Id信息
             * Ext2存储任意字符,如果有数据信息,则改查询为权限查询(既该查询为代理商或者供应商产品分页查询)
             */


            if (!string.IsNullOrWhiteSpace(specification.Ext2))
            {
                var permissionQ = from it in _context.Set<ProductPermissionOrgan>()
                                  join ip in _context.Set<ProductPermissionItem>() on it.ProductPermissionGroupId equals ip.ProductPermissionGroupId
                                  where it.OrganizationId == specification.Ext1
                                  select ip.ProductId;

                var q = specification.Includes.Aggregate(_context.Set<Product>().AsQueryable(), (current, include) => current.Include(include));
                return q.Where(specification.Criteria).Where(x => permissionQ.Contains(x.Id)).OrderBy(noOrder ? "ModifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
            }



            var queryableResult = specification.Includes.Aggregate(_context.Set<Product>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "ModifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Product entity)
        {
            _context.Set<Product>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Set<Product>().Update(entity);
            await _context.SaveEntitiesAsync();
        }
        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            await LoadOwnProductSpecsAsync(data);
            foreach (var spec in data.OwnProductSpecs)
                spec.DeleteRelatedStaticMesh();
            _context.Set<Product>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(Product data, string operatorId)
        {
            await LoadOwnProductSpecsAsync(data);
            foreach (var spec in data.OwnProductSpecs)
                spec.DeleteRelatedStaticMesh();
            _context.Set<Product>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
