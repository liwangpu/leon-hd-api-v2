using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using System;
using System.Linq;
namespace App.MoreJee.Infrastructure.Specifications.PackageMapSpecifications
{
    public class GetMapByPackageNameSpecification : BaseSpecification<PackageMap>
    {
        public GetMapByPackageNameSpecification(string packageNames)
        {
            var packages = packageNames.Split(",", StringSplitOptions.RemoveEmptyEntries);
            Criteria = x => packages.Distinct().Contains(x.Package);
        }
    }
}
