using App.Base.API.Application.Queries;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Roles
{
    public class RolePagingQueryHandler : IRequestHandler<RolePagingQuery, PagingQueryResult<RolePagingQueryDTO>>
    {
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;

        public RolePagingQueryHandler(IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.appLocalizer = appLocalizer;
        }

        public async Task<PagingQueryResult<RolePagingQueryDTO>> Handle(RolePagingQuery request, CancellationToken cancellationToken)
        {
            request.CheckPagingParam();
            var result = new PagingQueryResult<RolePagingQueryDTO>();
            var roles = Enumeration.GetAll<SystemRole>();
            result.Total = roles.Count();
            result.Data = roles.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).Select(x =>
            {
                var dto = new RolePagingQueryDTO();
                dto.Id = x.Id;
                dto.Name = appLocalizer[x.Name];
                dto.Description = appLocalizer[x.Description];
                dto.AccessPointKeys = x.AccessPointKeys;
                return dto;
            }).ToList();
            return await Task.FromResult(result);
        }
    }
}
