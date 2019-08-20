using App.Base.Domain.Common;
using App.OMS.Domain.AggregateModels.CustomerAggregate;

namespace App.OMS.Infrastructure.Specifications.CustomerSpecifications
{
    public class CustomerPagingSpecification : PagingBaseSpecification<Customer>
    {
        public CustomerPagingSpecification(string organId, int page, int pageSize, string orderBy, bool desc, string search)
        {
            AppendCriteriaAdd(map => map.OrganizationId == organId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(m => m.Name.Contains(search));


            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
    }
}
