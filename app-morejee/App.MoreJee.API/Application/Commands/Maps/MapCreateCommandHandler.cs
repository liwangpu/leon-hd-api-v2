using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Maps
{
    public class MapCreateCommandHandler : IRequestHandler<MapCreateCommand, string>
    {
        private readonly IIdentityService identityService;
        private readonly IMapRepository mapRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MapCreateCommandHandler(IIdentityService identityService, IMapRepository mapRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.mapRepository = mapRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(MapCreateCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var map = new Map(request.Name, request.Icon, identityService.GetOrganizationId(), identityService.GetUserId());
            await mapRepository.AddAsync(map);
            return map.Id;
        }
        #endregion
    }
}
