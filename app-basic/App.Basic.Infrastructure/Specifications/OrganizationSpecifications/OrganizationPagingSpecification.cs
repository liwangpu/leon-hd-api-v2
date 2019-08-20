using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class OrganizationPagingSpecification : PagingBaseSpecification<Organization>
    {

        public OrganizationPagingSpecification(string organizationId, int page, int pageSize, string orderBy, bool desc, string search)
        {

            AppendCriteriaAdd(m => m.ParentId == organizationId);

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
