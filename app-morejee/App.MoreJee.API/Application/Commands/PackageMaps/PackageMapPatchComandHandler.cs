using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.PackageMaps
{
    public class PackageMapPatchComandHandler : IRequestHandler<PackageMapPatchComand>
    {
        private readonly IPackageMapRepository packageMapRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public PackageMapPatchComandHandler(IPackageMapRepository packageMapRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.packageMapRepository = packageMapRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(PackageMapPatchComand request, CancellationToken cancellationToken)
        {
            var data = await packageMapRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "PackageMap", request.Id]);

            mapper.Map(data, request);
            request.ApplyPatch();
            data.UpdateResource(request.ResourceId, request.ResourceType);

            await packageMapRepository.UpdateAsync(data);
            return Unit.Value;
        }
        #endregion
    }
}
