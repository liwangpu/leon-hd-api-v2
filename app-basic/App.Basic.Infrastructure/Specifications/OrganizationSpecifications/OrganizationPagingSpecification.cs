
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class OrganizationPagingSpecification : PagingSpecification<Organization>
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
