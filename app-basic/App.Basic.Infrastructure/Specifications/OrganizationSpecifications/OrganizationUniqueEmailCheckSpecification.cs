
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class OrganizationUniqueEmailCheckSpecification : Specification<Organization>
    {
        #region ctor
        public OrganizationUniqueEmailCheckSpecification(string mail)
        {
            Criteria = organ => organ.Mail.ToLower().Trim() == mail.ToLower().Trim();
        }
        #endregion

    }
}
