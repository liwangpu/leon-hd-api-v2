using App.MoreJee.Domain.SeedWork;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public interface IProductPermissionRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(ProductPermission entity);
        Task DeleteAsync(string id, string operatorId);
        Task<bool> ExistAsync(string productId, string oranizationId, string productPermissionGroupId);
    }
}
