using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Roles
{
    public class RoleIdentityQueryHandler : IRequestHandler<RoleIdentityQuery, RoleIdentityQueryDTO>
    {
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;

        public RoleIdentityQueryHandler(IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.commonLocalizer = commonLocalizer;
            this.appLocalizer = appLocalizer;
        }

        public async Task<RoleIdentityQueryDTO> Handle(RoleIdentityQuery request, CancellationToken cancellationToken)
        {
            var role = Enumeration.FromValue<SystemRole>(request.Id);
            if (role == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Role", request.Id]);
            var dto = new RoleIdentityQueryDTO();
            dto.Id = role.Id;
            dto.Name = appLocalizer[role.Name];
            dto.Description = appLocalizer[role.Description];
            dto.AccessPointKeys = role.AccessPointKeys;
            return await Task.FromResult(dto);
        }
    }
}
