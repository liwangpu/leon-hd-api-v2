using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapIdentityQueryHandler : IRequestHandler<PackageMapIdentityQuery, PackageMapIdentityQueryDTO>
    {
        private readonly IPackageMapRepository packageMapRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public PackageMapIdentityQueryHandler(IPackageMapRepository packageMapRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.packageMapRepository = packageMapRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<PackageMapIdentityQueryDTO> Handle(PackageMapIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await packageMapRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "PackageMap", request.Id]);

            return PackageMapIdentityQueryDTO.From(data, request.MapType);
        }
        #endregion
    }
}
