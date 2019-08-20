using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.UserRoleSpecifications
{
    public class GetUserRoleByAccountIdSpecification : BaseSpecification<UserRole>
    {
        public GetUserRoleByAccountIdSpecification(string accountId)
        {
            Criteria = userRole => userRole.AccountId == accountId;
        }
    }
}
