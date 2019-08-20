using App.Base.API;
using App.Base.API.Application.Queries;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.StaticMeshSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.StaticMeshs
{
    public class StaticMeshPagingQueryHandler : IRequestHandler<StaticMeshPagingQuery, PagingQueryResult<StaticMeshPagingQueryDTO>>
    {
        private readonly IStaticMeshRepository staticMeshRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public StaticMeshPagingQueryHandler(IStaticMeshRepository staticMeshRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.staticMeshRepository = staticMeshRepository;
            this.commonLocalizer = commonLocalizer;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<StaticMeshPagingQueryDTO>> Handle(StaticMeshPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<StaticMeshPagingQueryDTO>();
            request.CheckPagingParam();

            var clientOrganId = await clientAssetPermissionControlService.ClientAssetOrganIdRedirection();
            var yesTranslated = commonLocalizer["IsDefaultValue"];
            var specification = new StaticMeshPagingSpecification(clientOrganId,request.Page, request.PageSize, request.Search);
            var datas = await staticMeshRepository.Paging(specification).Select(x => new { x.Id, x.Name, x.RelatedProductSpecIds, x.CreatedTime, x.ModifiedTime }).ToListAsync();
            result.Total = await staticMeshRepository.Get(specification).CountAsync();
            result.Data = datas.Select(x => StaticMeshPagingQueryDTO.From(x.Id, x.Name, string.IsNullOrWhiteSpace(x.RelatedProductSpecIds) ? "" : yesTranslated, x.CreatedTime, x.ModifiedTime)).ToList();
            return result;
        }
        #endregion
    }
}
