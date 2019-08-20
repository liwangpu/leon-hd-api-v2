using App.Base.API;
using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Infrastructure.Specifications.AccessPointSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class AccessPointPagingQueryHandler : IRequestHandler<AccessPointPagingQuery, PagingQueryResult<AccessPointPagingQueryDTO>>
    {
        private readonly IAccessPointRepository accessPointRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;
        private readonly IIdentityService identityService;

        #region ctor
        public AccessPointPagingQueryHandler(IAccessPointRepository accessPointRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<AppBasicTranslation> appLocalizer, IIdentityService identityService)
        {
            this.accessPointRepository = accessPointRepository;
            this.commonLocalizer = commonLocalizer;
            this.appLocalizer = appLocalizer;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<AccessPointPagingQueryDTO>> Handle(AccessPointPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<AccessPointPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new AccessPointPagingSpecification(request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search, identityService.GetOrganizationTypeId());
            var dtos = await accessPointRepository.Paging(specification).Select(x => AccessPointPagingQueryDTO.From(x)).ToListAsync();

            for (var idx = dtos.Count - 1; idx >= 0; idx--)
            {
                var it = dtos[idx];
                it.IsInnerName = it.IsInner == EntityStateConst.No ? "" : commonLocalizer["IsDefaultValue"];
                it.Name = appLocalizer[it.Name];

            }

            result.Total = await accessPointRepository.Get(specification).CountAsync();
            result.Data = dtos;
            return result;
        }
        #endregion
    }
}
