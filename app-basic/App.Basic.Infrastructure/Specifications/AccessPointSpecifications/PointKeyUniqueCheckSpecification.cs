using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccessPointSpecifications
{
    public class PointKeyUniqueCheckSpecification : Specification<AccessPoint>
    {
        public PointKeyUniqueCheckSpecification(string key, string id = null)
        {
            if (!string.IsNullOrWhiteSpace(id))
                AppendCriteriaAdd(acp => acp.Id != id);

            AppendCriteriaAdd(acp => acp.PointKey.ToLower() == key.Trim().ToLower());

            Criteria = CriteriaPredicate;
        }
    }
}
