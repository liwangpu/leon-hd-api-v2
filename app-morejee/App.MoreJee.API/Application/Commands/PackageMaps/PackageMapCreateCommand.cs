using MediatR;

namespace App.MoreJee.API.Application.Commands.PackageMaps
{
    public class PackageMapCreateCommand : IRequest<string>
    {
        public string Package { get; set; }
        public string Dependencies { get; set; }
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
        public string ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string Property { get; set; }
    }
}
