using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.PackageMapSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.PackageMaps
{
    public class PackageMapCreateCommandHandler : IRequestHandler<PackageMapCreateCommand, string>
    {
        private readonly IPackageMapRepository packageMapRepository;

        #region MyRegion
        public PackageMapCreateCommandHandler(IPackageMapRepository packageMapRepository)
        {
            this.packageMapRepository = packageMapRepository;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(PackageMapCreateCommand request, CancellationToken cancellationToken)
        {
            var newItem = false;
            var entity = await packageMapRepository.Get(new GetMapByPackageNameSpecification(request.Package)).FirstOrDefaultAsync();
            if (entity == null)
            {
                newItem = true;
                entity = new PackageMap(request.Package, request.ResourceId, request.ResourceType);
            }

            entity.UpdatePackage(request.Dependencies, request.SourceAssetUrl, request.UnCookedAssetUrl, request.Win64CookedAssetUrl, request.AndroidCookedAssetUrl, request.IOSCookedAssetUrl, request.DependencyAssetUrlsOfSource, request.DependencyAssetUrlsOfUnCooked, request.DependencyAssetUrlsOfWin64Cooked, request.DependencyAssetUrlsOfAndroidCooked, request.DependencyAssetUrlsOfIOSCooked, request.Property);

            if (newItem)
                await packageMapRepository.AddAsync(entity);
            else
                await packageMapRepository.UpdateAsync(entity);
            return entity.Id;
        }
        #endregion
    }
}
