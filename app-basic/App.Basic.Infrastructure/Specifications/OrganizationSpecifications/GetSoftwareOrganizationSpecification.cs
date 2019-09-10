using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class GetSoftwareOrganizationSpecification : Specification<Organization>
    {
        public GetSoftwareOrganizationSpecification()
        {
            AppendCriteriaAdd(organ => organ.OrganizationTypeId == OrganizationType.ServiceProvider.Id);
            Criteria = CriteriaPredicate;
        }
    }
}
