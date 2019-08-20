using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetAccountWithUserRoleSpecification : BaseSpecification<Account>
    {
        public GetAccountWithUserRoleSpecification(string accountId)
        {
            Criteria = acc => acc.Id == accountId;
            AddInclude(x => x.Organization);
            AddInclude(x => x.OwnRoles);
        }
    }
}
