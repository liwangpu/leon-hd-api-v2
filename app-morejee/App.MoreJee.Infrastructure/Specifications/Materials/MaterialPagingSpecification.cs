using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using System;
using System.Linq;

namespace App.MoreJee.Infrastructure.Specifications.Materials
{
    public class MaterialPagingSpecification : PagingBaseSpecification<Material>
    {
        public MaterialPagingSpecification(string clientOrganId, int page, int pageSize, string orderBy, bool desc, string search, string categoryIds, bool unClassified)
        {
            AppendCriteriaAdd(map => map.OrganizationId == clientOrganId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(m => m.Name.Contains(search));

            if (unClassified)
            {
                AppendCriteriaAdd(m => m.CategoryId == null);
                categoryIds = null;
            }
            if (!string.IsNullOrWhiteSpace(categoryIds))
            {
                var catIdArr = categoryIds.Split(",", StringSplitOptions.RemoveEmptyEntries);
                AppendCriteriaAdd(m => catIdArr.Contains(m.CategoryId));
            }

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
    }
}
