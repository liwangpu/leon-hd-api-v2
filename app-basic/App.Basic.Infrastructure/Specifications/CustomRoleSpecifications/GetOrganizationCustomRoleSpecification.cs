
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.SeedWork;
using System;
using System.Linq;
namespace App.Basic.Infrastructure.Specifications.CustomRoleSpecifications
{
    public class GetOrganizationCustomRoleSpecification : Specification<CustomRole>
    {
        public GetOrganizationCustomRoleSpecification(string organizationId, string ids = null)
        {
            AppendCriteriaAdd(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(ids))
            {
                var idArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                AppendCriteriaAdd(x => idArr.Contains(x.Id));
            }

            Criteria = CriteriaPredicate;
        }
    }
}
