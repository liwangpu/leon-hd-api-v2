using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.CustomRoles
{
    public class CustomRoleIdentityQueryHandler : IRequestHandler<CustomRoleIdentityQuery, CustomRoleIdentityQueryDTO>
    {
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        #region ctor
        public CustomRoleIdentityQueryHandler(ICustomRoleRepository customRoleRepository, IStringLocalizer<CommonTranslation> localizer)
        {
            this.customRoleRepository = customRoleRepository;
            this.localizer = localizer;
        }
        #endregion

        #region Handle
        public async Task<CustomRoleIdentityQueryDTO> Handle(CustomRoleIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await customRoleRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "CustomRole", request.Id]);

            return CustomRoleIdentityQueryDTO.From(data);
        }
        #endregion
    }
}
