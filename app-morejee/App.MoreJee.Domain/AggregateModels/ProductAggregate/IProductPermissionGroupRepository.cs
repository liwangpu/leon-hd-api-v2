using App.Base.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public interface IProductPermissionGroupRepository : IRepository<ProductPermissionGroup>
    {
        Task LoadOwnOrganItemsAsync(ProductPermissionGroup entity);
        Task LoadOwnProductItemsAsync(ProductPermissionGroup entity);
        Task DeleteAsync(ProductPermissionGroup data, string operatorId);
        Task<List<ProductPermissionGroupOwnProductItemDTO>> QueryOwnProduct(string groupId, string search);

        Task<List<OrganizationAllPermissionProductDTO>> GetOrganizationAllPermissionProduct(string organizationId, string search);
    }


    public class ProductPermissionGroupOwnProductItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class OrganizationAllPermissionProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
    }
}
