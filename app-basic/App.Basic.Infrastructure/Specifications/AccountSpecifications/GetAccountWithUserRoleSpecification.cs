using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetAccountWithUserRoleSpecification : Specification<Account>
    {
        public GetAccountWithUserRoleSpecification(string accountId)
        {
            Criteria = acc => acc.Id == accountId;
            AddInclude(x => x.Organization);
            AddInclude(x => x.OwnRoles);
        }
    }
}
