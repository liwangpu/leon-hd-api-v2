using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using System;
using System.Linq;

namespace App.MoreJee.Infrastructure.Specifications.ProductSpecifications
{
    public class ProductPagingSpecification : PagingBaseSpecification<Product>
    {
        public ProductPagingSpecification(string organizationId, int page, int pageSize, string search, string orderBy, bool desc, string categoryIds, bool unClassified)
        {
            //AppendCriteriaAdd(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(mesh => mesh.Name.Contains(search));

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

            Ext1 = organizationId;
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }

        public void SignPermissionPaging()
        {
            Ext2 = "true";
        }
    }
}
