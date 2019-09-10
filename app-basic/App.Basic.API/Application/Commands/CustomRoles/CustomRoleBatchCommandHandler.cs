using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.CustomRoles
{
    public class CustomRoleBatchCommandHandler : IRequestHandler<CustomRoleBatchCommand>
    {
        private readonly ICustomRoleRepository customRoleRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;
        #region ctor
        public CustomRoleBatchCommandHandler(ICustomRoleRepository customRoleRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper, IIdentityService identityService)
        {
            this.customRoleRepository = customRoleRepository;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(CustomRoleBatchCommand request, CancellationToken cancellationToken)
        {
            var data = await customRoleRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "CustomRole", request.Id]);

            mapper.Map(data, request);
            request.ApplyPatch();
            data.UpdateBasicInfo(request.Name, request.Description);
            data.UpdateAccessPoint(request.AccessPointKeys);
            customRoleRepository.Update(data);
            await customRoleRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }
        #endregion
    }
}
