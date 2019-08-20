using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetApplicationAdminManagedAccountSpecification : BaseSpecification<Account>
    {
        public GetApplicationAdminManagedAccountSpecification()
        {
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            Criteria = CriteriaPredicate;
        }
    }
}
