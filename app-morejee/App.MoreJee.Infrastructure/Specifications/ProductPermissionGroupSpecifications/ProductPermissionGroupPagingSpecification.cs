using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;

namespace App.MoreJee.Infrastructure.Specifications.ProductPermissionGroupSpecifications
{
    public class ProductPermissionGroupPagingSpecification : PagingBaseSpecification<ProductPermissionGroup>
    {
        public ProductPermissionGroupPagingSpecification(string organizationId, int page, int pageSize, string orderBy, bool desc, string search)
        {
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
