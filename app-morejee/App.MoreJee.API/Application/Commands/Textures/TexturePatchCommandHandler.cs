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

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TexturePatchCommandHandler : IRequestHandler<TexturePatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly ITextureRepository textureRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public TexturePatchCommandHandler(IIdentityService identityService, ITextureRepository textureRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.textureRepository = textureRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(TexturePatchCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var texture = await textureRepository.FindAsync(request.Id);
            if (texture == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Texture", request.Id]);

            mapper.Map(texture, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            texture.UpdateBasicInfo(request.Name, modifier);
            await textureRepository.UpdateAsync(texture);
            return Unit.Value;
        }
        #endregion
    }
}
