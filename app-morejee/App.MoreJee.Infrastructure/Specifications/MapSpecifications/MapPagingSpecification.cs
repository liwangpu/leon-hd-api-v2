using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;

namespace App.MoreJee.Infrastructure.Specifications.MapSpecifications
{
    public class MapPagingSpecification : PagingBaseSpecification<Map>
    {
        #region ctor
        public MapPagingSpecification(string clientOrganId, int page, int pageSize, string search)
        {
            AppendCriteriaAdd(map => map.OrganizationId == clientOrganId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(map => map.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            Criteria = CriteriaPredicate;
        }
        #endregion
    }
}
