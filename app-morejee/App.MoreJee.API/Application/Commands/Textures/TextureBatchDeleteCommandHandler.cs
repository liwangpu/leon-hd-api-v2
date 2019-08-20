using App.Base.API;
using App.Base.API.Infrastructure.ActionResults;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Extensions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TextureBatchDeleteCommandHandler : IRequestHandler<TextureBatchDeleteCommand, ObjectResult>
    {
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IUriService uriService;
        private readonly IIdentityService identityService;
        private readonly ITextureRepository textureRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        public TextureBatchDeleteCommandHandler(IStringLocalizer<CommonTranslation> localizer, IUriService uriService, IIdentityService identityService, ITextureRepository textureRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.localizer = localizer;
            this.uriService = uriService;
            this.identityService = identityService;
            this.textureRepository = textureRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }

        public async Task<ObjectResult> Handle(TextureBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var canOperate = await clientAssetPermissionControlService.CanEditClientAsset();
            if (!canOperate)
                throw new HttpForbiddenException();

            var result = new MultiStatusObjectResult();
            var operatorId = identityService.GetUserId();
            var resourcePartUri = uriService.GetUriWithoutQuery().URIUpperLevel();
            var idArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, len = idArr.Count(); i < len; i++)
            {
                var id = idArr[i];
                var uri = $"{resourcePartUri}/{id}";

                var data = await textureRepository.FindAsync(id);
                if (data == null)
                {
                    result.AddResult(uri, 404, "");
                    continue;
                }

                //var query = await userManagedAccountService.GetManagedAccounts(operatorId);
                //var canOperat = await query.AnyAsync(x => x.Id == accountId);
                //if (!canOperat)
                //{
                //    result.AddResult(uri, 403, localizer["OperateForbidden"]);
                //    continue;
                //}


                await textureRepository.DeleteAsync(data, operatorId);
                result.AddResult(uri, 200, "");
            }

            return result.Transfer();
        }
    }
}
