using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;

namespace App.MoreJee.Infrastructure.Specifications.PackageMapSpecifications
{
    public class PackageMapPagingSpecification : PagingBaseSpecification<PackageMap>
    {
        public PackageMapPagingSpecification(int page, int pageSize, string search, string orderBy, bool desc)
        {

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(x => x.Package.Contains(search));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
    }
}
