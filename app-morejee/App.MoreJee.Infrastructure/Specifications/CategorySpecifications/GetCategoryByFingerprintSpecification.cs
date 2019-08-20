using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;

namespace App.MoreJee.Infrastructure.Specifications.CategorySpecifications
{
    public class GetCategoryByFingerprintSpecification : BaseSpecification<Category>
    {
        public GetCategoryByFingerprintSpecification(string fingerprint)
        {
            AppendCriteriaAdd(x => x.Fingerprint == fingerprint);

            Criteria = CriteriaPredicate;
        }
    }
}
