using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;

using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using App.Basic.Infrastructure.Specifications.OrganizationSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationPagingQueryHandler : IRequestHandler<OrganizationPagingQuery, PagingQueryResult<OrganizationPagingQueryDTO>>
    {
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;
        private readonly IIdentityService identityService;
        private readonly IOrganizationRepository organizationRepository;

        #region ctor
        public OrganizationPagingQueryHandler(IStringLocalizer<AppBasicTranslation> appLocalizer, IIdentityService identityService, IOrganizationRepository organizationRepository)
        {
            this.appLocalizer = appLocalizer;
            this.identityService = identityService;
            this.organizationRepository = organizationRepository;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<OrganizationPagingQueryDTO>> Handle(OrganizationPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<OrganizationPagingQueryDTO>();
            request.CheckPagingParam();
            var specification = new OrganizationPagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            var dtos = await organizationRepository.Paging(specification).Select(x => OrganizationPagingQueryDTO.From(x)).ToListAsync();
            //翻译组织类型名称
            for (var idx = dtos.Count - 1; idx >= 0; idx--)
            {
                var it = dtos[idx];
                var organType = Enumeration.FromValue<OrganizationType>(it.OrganizationTypeId);
                it.OrganizationTypeName = appLocalizer[organType.Name];
            }

            result.Total = await organizationRepository.Get(specification).CountAsync();
            result.Data = dtos;
            return result;
        }
        #endregion
    }

}
