using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.AccessPoints
{
    public class AccessPointPatchCommandHandler : IRequestHandler<AccessPointPatchCommand>
    {
        private readonly IAccessPointRepository accessPointRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IIdentityService identityService;

        #region ctor
        public AccessPointPatchCommandHandler(IAccessPointRepository accessPointRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer, IIdentityService identityService)
        {
            this.accessPointRepository = accessPointRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(AccessPointPatchCommand request, CancellationToken cancellationToken)
        {
            var sysRoleId = identityService.GetUserRole();
            if (!(sysRoleId == SystemRole.ApplicationManager.Id.ToString() || sysRoleId == SystemRole.ApplicationService.Id.ToString()))
                throw new HttpForbiddenException();

            var accessPoint = await accessPointRepository.FindAsync(request.Id);
            mapper.Map(accessPoint, request);
            request.ApplyPatch();

            //var existKey = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(request.PointKey, request.Id)).AnyAsync();
            //if (existKey)
            //    throw new HttpBadRequestException(commonLocalizer["FieldValueIsDuplicate", request.PointKey, "PointKey"]);

            accessPoint.UpdateBasicInfo(request.Name, request.PointKey, request.Description);
            await accessPointRepository.UpdateAsync(accessPoint);
            return Unit.Value;
        }
        #endregion
    }
}
