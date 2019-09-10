using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetNormalUserManagedAccountSpecification : Specification<Account>
    {
        public GetNormalUserManagedAccountSpecification(string accountId)
        {
            AppendCriteriaAdd(account => account.Id == accountId);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            Criteria = CriteriaPredicate;
        }
    }
}
