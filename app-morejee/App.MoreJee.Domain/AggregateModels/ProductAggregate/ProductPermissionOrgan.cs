using App.Base.Domain.Common;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductPermissionOrgan
    {
        public string Id { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string ProductPermissionGroupId { get; protected set; }
        public ProductPermissionGroup ProductPermissionGroup { get; protected set; }

        #region ctor
        protected ProductPermissionOrgan()
        {

        }

        public ProductPermissionOrgan(string organId, string groupId)
        {
            Id = GuidGen.NewGUID();
            OrganizationId = organId;
            ProductPermissionGroupId = groupId;
        } 
        #endregion
    }
}
