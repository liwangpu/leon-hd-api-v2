using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class AccessPointIdentityQueryHandler : IRequestHandler<AccessPointIdentityQuery, AccessPointIdentityQueryDTO>
    {
        private readonly IAccessPointRepository accessPointRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;

        #region ctor
        public AccessPointIdentityQueryHandler(IAccessPointRepository accessPointRepository, IStringLocalizer<CommonTranslation> localizer, IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.accessPointRepository = accessPointRepository;
            this.localizer = localizer;
            this.appLocalizer = appLocalizer;
        }
        #endregion

        #region Handle
        public async Task<AccessPointIdentityQueryDTO> Handle(AccessPointIdentityQuery request, CancellationToken cancellationToken)
        {
            var acc = await accessPointRepository.FindAsync(request.Id);
            if (acc == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "AccessPoint", request.Id]);
            var dto = AccessPointIdentityQueryDTO.From(acc);
            dto.Name = appLocalizer[dto.Name];
            return dto;
        } 
        #endregion
    }
}
