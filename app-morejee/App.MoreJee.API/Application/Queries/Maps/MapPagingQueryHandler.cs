using App.Base.API.Application.Queries;
using App.Basic.Export;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.MapSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Maps
{
    public class MapPagingQueryHandler : IRequestHandler<MapPagingQuery, PagingQueryResult<MapPagingQueryDTO>>
    {
        private readonly IMapRepository mapRepository;
        private readonly OrganizationService organizationService;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MapPagingQueryHandler(IMapRepository mapRepository, OrganizationService organizationService, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.mapRepository = mapRepository;
            this.organizationService = organizationService;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<MapPagingQueryDTO>> Handle(MapPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<MapPagingQueryDTO>();
            request.CheckPagingParam();

            var clientOrganId = await clientAssetPermissionControlService.ClientAssetOrganIdRedirection();

            var specification = new MapPagingSpecification(clientOrganId, request.Page, request.PageSize, request.Search);
            var datas = await mapRepository.Paging(specification).Select(x => new { x.Id, x.Name, x.CreatedTime, x.ModifiedTime }).ToListAsync();
            result.Total = await mapRepository.Get(specification).CountAsync();
            result.Data = datas.Select(x => MapPagingQueryDTO.From(x.Id, x.Name, x.CreatedTime, x.ModifiedTime)).ToList();
            return result;
        }
        #endregion
    }
}
