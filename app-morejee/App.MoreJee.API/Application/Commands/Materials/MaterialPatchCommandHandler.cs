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

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialPatchCommandHandler : IRequestHandler<MaterialPatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IMaterialRepository materialRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MaterialPatchCommandHandler(IIdentityService identityService, IMaterialRepository materialRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.identityService = identityService;
            this.materialRepository = materialRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(MaterialPatchCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var data = await materialRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Material", request.Id]);

            mapper.Map(data, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            data.UpdateBasicInfo(request.Name, request.Description, request.Icon, modifier);
            data.UpdateCategory(request.CategoryId);
            await materialRepository.UpdateAsync(data);
            return Unit.Value;
        }
        #endregion
    }
}
