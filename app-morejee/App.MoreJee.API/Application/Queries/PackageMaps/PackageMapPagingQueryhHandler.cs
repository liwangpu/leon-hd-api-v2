using App.Base.API.Application.Queries;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.PackageMapSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapPagingQueryhHandler : IRequestHandler<PackageMapPagingQuery, PagingQueryResult<PackageMapPagingQueryDTO>>
    {
        private readonly IPackageMapRepository packageMapRepository;

        #region ctor
        public PackageMapPagingQueryhHandler(IPackageMapRepository packageMapRepository)
        {
            this.packageMapRepository = packageMapRepository;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<PackageMapPagingQueryDTO>> Handle(PackageMapPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<PackageMapPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new PackageMapPagingSpecification(request.Page, request.PageSize, request.Search, request.OrderBy, request.Desc);
            var datas = await packageMapRepository.Paging(specification).Select(x => new { x.Id, x.Package, x.ResourceId, x.ResourceType }).ToListAsync();
            result.Total = await packageMapRepository.Get(specification).CountAsync();
            result.Data = datas.Select(x => PackageMapPagingQueryDTO.From(x.Id, x.Package, x.ResourceId, x.ResourceType)).ToList();
            return result;
        } 
        #endregion
    }
}
