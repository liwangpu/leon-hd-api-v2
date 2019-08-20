using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using System;
using System.Linq;
namespace App.Basic.Infrastructure.Specifications.CustomRoleSpecifications
{
    public class GetOrganizationCustomRoleSpecification : BaseSpecification<CustomRole>
    {
        public GetOrganizationCustomRoleSpecification(string organizationId, string ids = null)
        {
            AppendCriteriaAdd(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(ids))
            {
                var idArr = ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
                AppendCriteriaAdd(x => idArr.Contains(x.Id));
            }

            Criteria = CriteriaPredicate;
        }
    }
}
