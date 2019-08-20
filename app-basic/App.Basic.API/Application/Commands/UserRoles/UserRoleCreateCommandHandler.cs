using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Exceptions;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.UserRoles
{
    public class UserRoleCreateCommandHandler : IRequestHandler<UserRoleCreateCommand, UserRoleCreateCommandDTO>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;

        public UserRoleCreateCommandHandler(IAccountRepository accountRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.accountRepository = accountRepository;
            this.commonLocalizer = commonLocalizer;
            this.appLocalizer = appLocalizer;
        }

        public async Task<UserRoleCreateCommandDTO> Handle(UserRoleCreateCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.Get(new GetAccountWithUserRoleSpecification(request.AccountId)).FirstOrDefaultAsync();

            if (account == null)
                throw new HttpBadRequestException(commonLocalizer["HttpRespond.BadRequest.1", "Accounts", request.AccountId]);

            var roleType = Enumeration.FromValue<Role>(request.RoleId);
            if (roleType == null)
                throw new HttpBadRequestException(commonLocalizer["HttpRespond.BadRequest.1", "Roles", request.RoleId]);

            try
            {
                var role = account.AddRole(request.RoleId);
                await accountRepository.UpdateAsync(account);
                var dto = UserRoleCreateCommandDTO.From(role);
                dto.SetDetail(commonLocalizer[roleType.Name], commonLocalizer[roleType.Description]);
                return dto;
            }
            catch (Exception ex)
            {
                var exType = ex.GetType();
                if (exType == typeof(UserRoleDuplicationException))
                {
                    throw new HttpBadRequestException(appLocalizer["UserRole.Dupplication"]);
                }
                else if (exType == typeof(UserRoleNotApplicableException))
                {
                    throw new HttpBadRequestException(appLocalizer["UserRole.NotApplicable"]);
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
