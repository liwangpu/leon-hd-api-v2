using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class ProductPermissionRepository : IProductPermissionRepository
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
        public ProductPermissionRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task AddAsync(ProductPermission entity)
        {
            _context.Set<ProductPermission>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(string productId, string oranizationId, string productPermissionGroupId)
        {
            return await _context.Set<ProductPermission>().AnyAsync(x => x.ProductId == productId && x.OrganizationId == oranizationId && x.ProductPermissionGroupId == productPermissionGroupId);
        }
    }
}
