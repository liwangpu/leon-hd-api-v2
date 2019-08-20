using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;

namespace App.MoreJee.Infrastructure.Specifications.SolutionSpecifications
{
    public class SolutionPagingSpecification : PagingBaseSpecification<Solution>
    {
        public SolutionPagingSpecification(string creator, int page, int pageSize, string search, string orderBy, bool desc)
        {
            AppendCriteriaAdd(x => x.Creator == creator);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(map => map.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
    }
}
