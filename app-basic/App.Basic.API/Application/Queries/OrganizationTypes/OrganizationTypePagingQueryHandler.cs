using App.Base.API.Application.Queries;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.OrganizationTypes
{
    public class OrganizationTypePagingQueryHandler : IRequestHandler<OrganizationTypePagingQuery, PagingQueryResult<OrganizationTypePagingQueryDTO>>
    {
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;

        #region ctor
        public OrganizationTypePagingQueryHandler(IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.appLocalizer = appLocalizer;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<OrganizationTypePagingQueryDTO>> Handle(OrganizationTypePagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<OrganizationTypePagingQueryDTO>();
            var list = Enumeration.GetAll<OrganizationType>().ToList();
            result.Total = list.Count;
            result.Data = list.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).Select(x =>
            {
                var dto = new OrganizationTypePagingQueryDTO();
                dto.Id = x.Id;
                dto.Name = appLocalizer[x.Name];
                dto.Description = appLocalizer[x.Description];
                return dto;
            }).ToList();
            return await Task.FromResult(result);
        }
        #endregion
    }
}
