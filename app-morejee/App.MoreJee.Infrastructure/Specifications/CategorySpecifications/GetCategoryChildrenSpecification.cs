using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;

namespace App.MoreJee.Infrastructure.Specifications.CategorySpecifications
{
    public class GetCategoryChildrenSpecification : BaseSpecification<Category>
    {
        public GetCategoryChildrenSpecification(string categoryId)
        {
            AppendCriteriaAdd(x => x.ParentId == categoryId);

            Criteria = CriteriaPredicate;
        }
    }
}
