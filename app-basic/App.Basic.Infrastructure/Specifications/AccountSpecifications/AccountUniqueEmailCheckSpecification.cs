using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class AccountUniqueEmailCheckSpecification : Specification<Account>
    {
        #region ctor
        public AccountUniqueEmailCheckSpecification(string mail)
        {
            Criteria = account => account.Mail.ToLower().Trim() == mail.ToLower().Trim();
        }

        public AccountUniqueEmailCheckSpecification(string mail, string id)
        {
            Criteria = account => account.Id != id && account.Mail.ToLower().Trim() == mail.ToLower().Trim();
        }
        #endregion
    }
}
