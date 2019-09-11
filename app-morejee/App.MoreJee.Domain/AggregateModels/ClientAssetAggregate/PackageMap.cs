using App.MoreJee.Domain.Events.PackageMapEvents;
using App.MoreJee.Domain.SeedWork;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public class PackageMap : Entity, IAggregateRoot
    {
        public string Package { get; protected set; }
        public string Dependencies { get; protected set; }
        public string Property { get; protected set; }
        public string SourceAssetUrl { get; set; }
        public string UnCookedAssetUrl { get; set; }
        public string Win64CookedAssetUrl { get; set; }
        public string AndroidCookedAssetUrl { get; set; }
        public string IOSCookedAssetUrl { get; set; }
        public string DependencyAssetUrlsOfSource { get; set; }
        public string DependencyAssetUrlsOfUnCooked { get; set; }
        public string DependencyAssetUrlsOfWin64Cooked { get; set; }
        public string DependencyAssetUrlsOfAndroidCooked { get; set; }
        public string DependencyAssetUrlsOfIOSCooked { get; set; }
        public string ResourceId { get; protected set; }
        public string ResourceType { get; protected set; }


        #region ctor
        protected PackageMap()
        {

        }

        public PackageMap(string package, string resourceId, string resourceType)
            : this()
        {
            Package = package;
            ResourceId = resourceId;
            ResourceType = resourceType;
        }
        #endregion

        public void UpdatePackage(string dependencies, string sourceAssetUrl, string unCookedAssetUrl, string win64CookedAssetUrl, string androidCookedAssetUrl, string iosCookedAssetUrl, string dependencyAssetUrlsOfSource, string dependencyAssetUrlsOfUnCooked, string dependencyAssetUrlsOfWin64Cooked, string dependencyAssetUrlsOfAndroidCooked, string dependencyAssetUrlsOfIOSCooked, string property)
        {
            Dependencies = dependencies;
            SourceAssetUrl = sourceAssetUrl;
            UnCookedAssetUrl = unCookedAssetUrl;
            Win64CookedAssetUrl = win64CookedAssetUrl;
            AndroidCookedAssetUrl = androidCookedAssetUrl;
            IOSCookedAssetUrl = iosCookedAssetUrl;
            DependencyAssetUrlsOfSource = dependencyAssetUrlsOfSource;
            DependencyAssetUrlsOfUnCooked = dependencyAssetUrlsOfUnCooked;
            DependencyAssetUrlsOfWin64Cooked = dependencyAssetUrlsOfWin64Cooked;
            DependencyAssetUrlsOfAndroidCooked = dependencyAssetUrlsOfAndroidCooked;
            DependencyAssetUrlsOfIOSCooked = dependencyAssetUrlsOfIOSCooked;
            Property = property;
        }

        public void UpdateResource(string resourceId, string resourceType)
        {
            ResourceId = resourceId;
            ResourceType = resourceType;
        }

        public void DeleteClientAsset()
        {
            AddDomainEvent(new PakcageMapDeleteEvent(Package));
        }
    }
}
