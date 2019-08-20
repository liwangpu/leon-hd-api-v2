using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class TokenRequestSpecification : BaseSpecification<Account>
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
