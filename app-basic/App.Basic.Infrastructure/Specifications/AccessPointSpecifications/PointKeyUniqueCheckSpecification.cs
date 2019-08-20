using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;

namespace App.Basic.Infrastructure.Specifications.AccessPointSpecifications
{
    public class PointKeyUniqueCheckSpecification : BaseSpecification<AccessPoint>
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
