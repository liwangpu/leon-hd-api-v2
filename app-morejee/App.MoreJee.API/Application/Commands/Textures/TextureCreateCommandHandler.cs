using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TextureCreateCommandHandler : IRequestHandler<TextureCreateCommand, string>
    {
        private readonly IIdentityService identityService;
        private readonly ITextureRepository textureRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public TextureCreateCommandHandler(IIdentityService identityService, ITextureRepository textureRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.textureRepository = textureRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(TextureCreateCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var texture = new Texture(request.Name, request.Icon, identityService.GetOrganizationId(), identityService.GetUserId());
            await textureRepository.AddAsync(texture);
            return texture.Id;
        }
        #endregion
    }
}
