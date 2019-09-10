
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    /// <summary>
    /// 校验组织的法定负责人是否是该用户
    /// </summary>
    public class OrganizationOwnerCheckSpecification : Specification<Organization>
    {
        public OrganizationOwnerCheckSpecification(string organizationId, string accountId)
        {
            Criteria = organ => organ.Id == organizationId && organ.OwnerId == accountId;
        }
    }
}
