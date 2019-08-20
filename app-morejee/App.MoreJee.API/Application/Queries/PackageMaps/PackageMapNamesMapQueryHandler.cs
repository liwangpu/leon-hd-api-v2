using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.PackageMapSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapNamesMapQueryHandler : IRequestHandler<PackageMapNamesMapQuery, List<string>>
    {
        private readonly IPackageMapRepository packageMapRepository;
        #region ctor
        public PackageMapNamesMapQueryHandler(IPackageMapRepository packageMapRepository)
        {
            this.packageMapRepository = packageMapRepository;
        }
        #endregion

        #region Handle
        public async Task<List<string>> Handle(PackageMapNamesMapQuery request, CancellationToken cancellationToken)
        {
            return await packageMapRepository.Get(new GetMapByPackageNameSpecification(request.Packages)).Select(x => x.Id).ToListAsync();
        }
        #endregion
    }
}
