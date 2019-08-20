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

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialBatchDeleteCommandHandler : IRequestHandler<MaterialBatchDeleteCommand, ObjectResult>
    {
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IUriService uriService;
        private readonly IIdentityService identityService;
        private readonly IMaterialRepository materialRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MaterialBatchDeleteCommandHandler(IStringLocalizer<CommonTranslation> localizer, IUriService uriService, IIdentityService identityService, IMaterialRepository materialRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.localizer = localizer;
            this.uriService = uriService;
            this.identityService = identityService;
            this.materialRepository = materialRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion


        #region Handle
        public async Task<ObjectResult> Handle(MaterialBatchDeleteCommand request, CancellationToken cancellationToken)
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

                var data = await materialRepository.FindAsync(id);
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


                await materialRepository.DeleteAsync(data, operatorId);
                result.AddResult(uri, 200, "");
            }

            return result.Transfer();
        } 
        #endregion
    }
}
