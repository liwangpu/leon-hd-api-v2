using App.Base.Domain.Common;

namespace App.MoreJee.Domain.AggregateModels.CategoryAggregate
{
    public class CategoryRegistry : Entity
    {
        public string Name { get; protected set; }
        public string Resource { get; protected set; }
        public string Description { get; protected set; }
        public string Icon { get; protected set; }
        public string Fingerprint { get; protected set; }
        public string OrganizationId { get; protected set; }

        #region ctor
        protected CategoryRegistry()
        {

        }

        public CategoryRegistry(string categoryName, string categoryDescription, string fingerPrint, string categoryResource, string categoryIcon, string organizationId)
        {
            Name = categoryName;
            Resource = categoryResource;
            Description = categoryDescription;
            Fingerprint = fingerPrint;
            Icon = categoryIcon;
            OrganizationId = organizationId;
        }
        #endregion

        public void UpdateBasicInfo(string categoryName, string categoryDescription, string categoryResource, string categoryIcon)
        {
            Name = categoryName;
            Resource = categoryResource;
            Description = categoryDescription;
            Icon = categoryIcon;
        }
    }
}
