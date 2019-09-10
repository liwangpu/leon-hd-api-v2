using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.CustomRoles
{
    public class CustomRoleCreateCommandHandler : IRequestHandler<CustomRoleCreateCommand, string>
    {
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public CustomRoleCreateCommandHandler(ICustomRoleRepository customRoleRepository, IIdentityService identityService)
        {
            this.customRoleRepository = customRoleRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(CustomRoleCreateCommand request, CancellationToken cancellationToken)
        {
            var role = new CustomRole(request.Name, request.Description, identityService.GetOrganizationId());
            customRoleRepository.Add(role);
            await customRoleRepository.UnitOfWork.SaveEntitiesAsync();
            return role.Id;
        }
        #endregion
    }
}
