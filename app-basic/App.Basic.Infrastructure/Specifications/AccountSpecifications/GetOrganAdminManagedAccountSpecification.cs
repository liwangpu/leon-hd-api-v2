using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetOrganAdminManagedAccountSpecification : BaseSpecification<Account>
    {
        public GetOrganAdminManagedAccountSpecification(string organId)
        {
            AppendCriteriaAdd(account => account.OrganizationId == organId);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            Criteria = CriteriaPredicate;
        }
    }
}
