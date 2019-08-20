using App.Base.Domain.Common;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermissionItem
    {
        public string Id { get; protected set; }
        public string ProductId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }
        public ProductPermissionGroup ProductPermissionGroup { get; protected set; }

        #region ctor
        protected ProductPermissionItem()
        {

        }

        public ProductPermissionItem(string productId, string groupId)
        {
            Id = GuidGen.NewGUID();
            ProductId = productId;
            ProductPermissionGroupId = groupId;
        } 
        #endregion
    }
}
