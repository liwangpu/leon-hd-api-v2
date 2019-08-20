using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;

namespace App.MoreJee.Infrastructure.Specifications.CategorySpecifications
{
    public class GetRootCategoryByResourceSpecification : BaseSpecification<Category>
    {
        public GetRootCategoryByResourceSpecification(string resource)
        {
            AppendCriteriaAdd(x => x.Resource == resource && x.LValue == 1);

            Criteria = CriteriaPredicate;
        }
    }
}
