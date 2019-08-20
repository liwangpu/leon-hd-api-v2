using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace App.MoreJee.Infrastructure.Repositories
{
    public class ProductPermissionGroupRepository : IProductPermissionGroupRepository
    {
        private readonly ICategoryRepository categoryRepository;

        public MoreJeeAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public ProductPermissionGroupRepository(MoreJeeAppContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            this.categoryRepository = categoryRepository;
        }
        #endregion

        protected EntityEntry<ProductPermissionGroup> Entry(ProductPermissionGroup entity)
        {
            return _context.Entry(entity);
        }

        public async Task LoadOwnOrganItemsAsync(ProductPermissionGroup entity)
        {
            if (entity == null) return;
            await Entry(entity).Collection(x => x.OwnOrganItems).LoadAsync();
        }

        public async Task LoadOwnProductItemsAsync(ProductPermissionGroup entity)
        {
            if (entity == null) return;
            await Entry(entity).Collection(x => x.OwnProductItems).LoadAsync();
        }

        public async Task<ProductPermissionGroup> FindAsync(string id)
        {
            return await _context.Set<ProductPermissionGroup>().FindAsync(id);
        }

        public IQueryable<ProductPermissionGroup> Get(ISpecification<ProductPermissionGroup> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<ProductPermissionGroup>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<ProductPermissionGroup> Paging(IPagingSpecification<ProductPermissionGroup> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<ProductPermissionGroup>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "Name" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(ProductPermissionGroup entity)
        {
            _context.Set<ProductPermissionGroup>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(ProductPermissionGroup entity)
        {
            _context.Set<ProductPermissionGroup>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            _context.Set<ProductPermissionGroup>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(ProductPermissionGroup data, string operatorId)
        {
            if (data == null) return;
            _context.Set<ProductPermissionGroup>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public Task<List<ProductPermissionGroupOwnProductItemDTO>> QueryOwnProduct(string groupId, string search)
        {
            var q = from it in _context.Set<ProductPermissionItem>()
                    join pt in _context.Set<Product>() on it.ProductId equals pt.Id
                    where (string.IsNullOrWhiteSpace(search) ? true : pt.Name.Contains(search)) && it.ProductPermissionGroupId == groupId
                    select new ProductPermissionGroupOwnProductItemDTO
                    {
                        Id = it.Id,
                        Name = pt.Name,
                        Description = pt.Description,
                        CategoryId = pt.CategoryId
                    };

            return q.ToListAsync();
        }

        public async Task<List<OrganizationAllPermissionProductDTO>> GetOrganizationAllPermissionProduct(string organizationId, string search)
        {
            var organQ = from it in _context.Set<ProductPermissionOrgan>()
                         where it.OrganizationId == organizationId
                         select it;
            var groupQ = from it in _context.Set<ProductPermissionGroup>()
                         join ot in organQ on it.Id equals ot.ProductPermissionGroupId
                         select it;
            var q = from it in _context.Set<ProductPermissionItem>()
                    join g in groupQ on it.ProductPermissionGroupId equals g.Id
                    join pt in _context.Set<Product>() on it.ProductId equals pt.Id
                    where string.IsNullOrWhiteSpace(search) ? true : pt.Name.Contains(search)
                    select new OrganizationAllPermissionProductDTO
                    {
                        Id = pt.Id,
                        Name = pt.Name,
                        Icon = pt.Icon,
                        Description = pt.Description,
                        CategoryId = pt.CategoryId,
                        GroupName = g.Name
                    };

            var list = await q.ToListAsync();

            for (var idx = list.Count - 1; idx >= 0; idx--)
            {
                var it = list[idx];
                it.CategoryName = await categoryRepository.GetCategoryName(it.CategoryId);
            }

            return list;
        }
    }
}
