using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetNormalUserManagedAccountSpecification : BaseSpecification<Account>
    {
        public GetNormalUserManagedAccountSpecification(string accountId)
        {
            AppendCriteriaAdd(account => account.Id == accountId);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            Criteria = CriteriaPredicate;
        }
    }
}
