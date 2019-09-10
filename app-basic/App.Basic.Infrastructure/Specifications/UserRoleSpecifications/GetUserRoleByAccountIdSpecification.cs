
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.UserRoleSpecifications
{
    public class GetUserRoleByAccountIdSpecification : Specification<UserRole>
    {
        public GetUserRoleByAccountIdSpecification(string accountId)
        {
            Criteria = userRole => userRole.AccountId == accountId;
        }
    }
}
