using App.Base.Domain.Common;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermission : Entity
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
            Id = GuidGen.NewGUID();
            ProductId = productId;
            OrganizationId = organizationId;
            ProductPermissionGroupId = productPermissionGroupId;
        }
        #endregion





    }
}
