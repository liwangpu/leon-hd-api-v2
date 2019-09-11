using App.MoreJee.Domain.SeedWork;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermission : Entity, IAggregateRoot
    {
        public string ProductId { get; set; }
        public string OrganizationId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }

        #region ctor
        protected ProductPermission()
        {

        }

        public ProductPermission(string productId, string organizationId, string productPermissionGroupId)
        {
            ProductId = productId;
            OrganizationId = organizationId;
            ProductPermissionGroupId = productPermissionGroupId;
        }
        #endregion





    }
}
