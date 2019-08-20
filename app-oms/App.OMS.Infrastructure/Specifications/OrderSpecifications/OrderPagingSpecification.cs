using App.Base.Domain.Common;
using App.OMS.Domain.AggregateModels.OrderAggregate;

namespace App.OMS.Infrastructure.Specifications.OrderSpecifications
{
    public class OrderPagingSpecification : PagingBaseSpecification<Order>
    {
        public OrderPagingSpecification(string organId, int page, int pageSize, string orderBy, bool desc, string search)
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
