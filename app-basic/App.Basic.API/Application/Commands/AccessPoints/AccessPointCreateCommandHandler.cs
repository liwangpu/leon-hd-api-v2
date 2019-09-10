using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.AccessPoints
{
    public class AccessPointCreateCommandHandler : IRequestHandler<AccessPointCreateCommand, string>
    {
        private readonly IAccessPointRepository accessPointRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public AccessPointCreateCommandHandler(IAccessPointRepository accessPointRepository, IIdentityService identityService)
        {
            this.accessPointRepository = accessPointRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(AccessPointCreateCommand request, CancellationToken cancellationToken)
        {
            var sysRoleId = identityService.GetUserRole();
            if (!(sysRoleId == SystemRole.ApplicationManager.Id.ToString() || sysRoleId == SystemRole.ApplicationService.Id.ToString()))
                throw new HttpForbiddenException();

            var accessPoint = new AccessPoint(request.Name, request.PointKey, request.Description);
            accessPointRepository.Add(accessPoint);
            await accessPointRepository.UnitOfWork.SaveEntitiesAsync();
            return accessPoint.Id;
        }
        #endregion
    }
}
