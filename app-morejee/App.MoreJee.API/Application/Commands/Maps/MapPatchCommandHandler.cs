using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Maps
{
    public class MapPatchCommandHandler : IRequestHandler<MapPatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IMapRepository mapRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MapPatchCommandHandler(IIdentityService identityService, IMapRepository mapRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.mapRepository = mapRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(MapPatchCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var map = await mapRepository.FindAsync(request.Id);
            if (map == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Map", request.Id]);

            mapper.Map(map, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            map.UpdateBasicInfo(request.Name, modifier);
            await mapRepository.UpdateAsync(map);
            return Unit.Value;
        } 
        #endregion
    }
}
