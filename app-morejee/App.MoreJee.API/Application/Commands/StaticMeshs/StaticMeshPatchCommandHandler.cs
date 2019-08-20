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

namespace App.MoreJee.API.Application.Commands.StaticMeshs
{
    public class StaticMeshPatchCommandHandler : IRequestHandler<StaticMeshPatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IStaticMeshRepository staticMeshRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public StaticMeshPatchCommandHandler(IIdentityService identityService, IStaticMeshRepository staticMeshRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.staticMeshRepository = staticMeshRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(StaticMeshPatchCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var staticMesh = await staticMeshRepository.FindAsync(request.Id);
            if (staticMesh == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "StaticMesh", request.Id]);

            mapper.Map(staticMesh, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            staticMesh.UpdateBasicInfo(request.Name, modifier);
            await staticMeshRepository.UpdateAsync(staticMesh);
            return Unit.Value;
        }
        #endregion
    }
}
