

using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetOrganAdminManagedAccountSpecification : Specification<Account>
    {
        public GetOrganAdminManagedAccountSpecification(string organId)
        {
            AppendCriteriaAdd(account => account.OrganizationId == organId);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            Criteria = CriteriaPredicate;
        }
    }
}
