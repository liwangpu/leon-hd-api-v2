using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.OSS.API.Application.Commands.Files
{
    public class FilePatchCommandHandler : IRequestHandler<FilePatchCommand>
    {
        private readonly IFileAssetRepository fileAssetRepository;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        public FilePatchCommandHandler(IFileAssetRepository fileAssetRepository, IIdentityService identityService, IMapper mapper, IStringLocalizer<CommonTranslation> localizer)
        {
            this.fileAssetRepository = fileAssetRepository;
            this.identityService = identityService;
            this.mapper = mapper;
            this.localizer = localizer;
        }


        public async Task<Unit> Handle(FilePatchCommand request, CancellationToken cancellationToken)
        {
            var asset = await fileAssetRepository.FindAsync(request.Id);
            if (asset == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "File", request.Id]);
            var operatorId = identityService.GetUserId();
            mapper.Map(asset, request);
            request.ApplyPatch();
            asset.UpdateBasicInfo(request.Name, request.Description, request.FileState, operatorId);
            await fileAssetRepository.UpdateAsync(asset);
            return Unit.Value;
        }
    }
}
