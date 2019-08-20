using App.Base.API;
using App.Base.API.Infrastructure.ActionResults;
using App.Base.API.Infrastructure.Extensions;
using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.UserRoles
{
    public class UserRoleBatchDeleteCommandHandler : IRequestHandler<UserRoleBatchDeleteCommand, ObjectResult>
    {
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IUriService uriService;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        public UserRoleBatchDeleteCommandHandler(IUserRoleRepository userRoleRepository, IUriService uriService, IStringLocalizer<CommonTranslation> localizer)
        {
            this.userRoleRepository = userRoleRepository;
            this.uriService = uriService;
            this.localizer = localizer;
        }

        public async Task<ObjectResult> Handle(UserRoleBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var resourcePartUri = uriService.GetUriWithoutQuery().URIUpperLevel();
            var result = new MultiStatusObjectResult();

            var userRoleIds = request.UserRoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < userRoleIds.Length; i++)
            {
                var userRoleId = userRoleIds[i];
                var role = await userRoleRepository.FindAsync(userRoleId);
                if (role == null)
                {
                    result.AddResult($"{resourcePartUri}/{userRoleId}", 404, "");
                    continue;
                }

                if (role.IsDefault == EntityStateConst.IsDefault)
                {
                    var roleType = Enumeration.FromValue<Role>(role.RoleId);
                    result.AddResult($"{resourcePartUri}/{userRoleId}", 403, localizer["UserRole.CannotDeleteDefaultRole", localizer[roleType.Name]]);
                    continue;
                }

                await userRoleRepository.DeleteAsync(userRoleId, null);
                result.AddResult($"{resourcePartUri}/{userRoleId}", 200, "");
            }

            return result.Transfer();
        }

    }
}
