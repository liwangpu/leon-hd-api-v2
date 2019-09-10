
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class TokenRequestSpecification : Specification<Account>
    {
        #region ctor
        public TokenRequestSpecification(string username, string password)
        {
            Criteria = user => user.Mail.ToLower() == username.ToLower() && user.Password == password;
            AddInclude(x=>x.Organization);
        }
        #endregion

    }
}
