using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    /// <summary>
    /// 获取组织管理员所管理的组织信息
    /// </summary>
    public class GetOrganAdminManagedOrganizationSpecification : BaseSpecification<Organization>
    {
        public GetOrganAdminManagedOrganizationSpecification(string organId)
        {
            AppendCriteriaAdd(organ => organ.Active == EntityStateConst.Active);
            AppendCriteriaAdd(organ => organ.Id == organId || organ.ParentId == organId);

            Criteria = CriteriaPredicate;
        }
    }
}
