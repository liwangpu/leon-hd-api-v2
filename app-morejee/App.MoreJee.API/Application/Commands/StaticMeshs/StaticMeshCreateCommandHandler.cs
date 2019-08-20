using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Application.DomainEventHandlers.ClientAssets;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.StaticMeshs
{
    public class StaticMeshCreateCommandHandler : IRequestHandler<StaticMeshCreateCommand, string>
    {
        private readonly IStaticMeshRepository staticMeshRepository;
        private readonly IIdentityService identityService;
        private readonly IMediator mediator;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public StaticMeshCreateCommandHandler(IStaticMeshRepository staticMeshRepository, IIdentityService identityService, IMediator mediator, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.staticMeshRepository = staticMeshRepository;
            this.identityService = identityService;
            this.mediator = mediator;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(StaticMeshCreateCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var mesh = new StaticMesh(request.Name, request.Icon, identityService.GetOrganizationId(), identityService.GetUserId());

            await staticMeshRepository.AddAsync(mesh);


            if (request.CreateProduct)
                await mediator.Publish(new CreateStaticMeshRelatedProductEvent(mesh.Id, mesh.Name, mesh.OrganizationId, mesh.Creator));

            return mesh.Id;
        }
        #endregion
    }
}
