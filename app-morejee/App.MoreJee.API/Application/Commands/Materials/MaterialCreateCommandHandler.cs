using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialCreateCommandHandler : IRequestHandler<MaterialCreateCommand, string>
    {
        private readonly IIdentityService identityService;
        private readonly IMaterialRepository materialRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;
        #region ctor
        public MaterialCreateCommandHandler(IIdentityService identityService, IMaterialRepository materialRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.materialRepository = materialRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(MaterialCreateCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var data = new Material(request.Name, request.Icon, request.CategoryId, identityService.GetOrganizationId(), identityService.GetUserId());
            await materialRepository.AddAsync(data);

            return data.Id;
        }
        #endregion
    }
}
