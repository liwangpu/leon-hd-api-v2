using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;

namespace App.Basic.Infrastructure.Specifications.CustomRoleSpecifications
{
    public class CustomRolePagingSpecification : PagingBaseSpecification<CustomRole>
    {
        public CustomRolePagingSpecification(string organizationId, int page, int pageSize, string orderBy, bool desc, string search)
        {
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;

            AppendCriteriaAdd(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(x => x.Name.Contains(search));

            Criteria = CriteriaPredicate;
        }
    }
}
