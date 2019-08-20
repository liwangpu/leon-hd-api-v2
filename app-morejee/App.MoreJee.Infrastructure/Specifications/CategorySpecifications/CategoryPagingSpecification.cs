using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;

namespace App.MoreJee.Infrastructure.Specifications.CategorySpecifications
{
    public class CategoryPagingSpecification : PagingBaseSpecification<Category>
    {
        public CategoryPagingSpecification(int page, int pageSize, string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(x => x.Name.Contains(search));


            AppendCriteriaAdd(x => x.LValue == 1);
            Page = page;
            PageSize = pageSize;
            Criteria = CriteriaPredicate;
        }
    }
}
