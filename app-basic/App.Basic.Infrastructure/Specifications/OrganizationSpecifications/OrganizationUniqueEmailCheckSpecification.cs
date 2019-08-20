using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class OrganizationUniqueEmailCheckSpecification : BaseSpecification<Organization>
    {
        #region ctor
        public OrganizationUniqueEmailCheckSpecification(string mail)
        {
            Criteria = organ => organ.Mail.ToLower().Trim() == mail.ToLower().Trim();
        }
        #endregion

    }
}
