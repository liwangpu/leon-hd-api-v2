using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;

namespace App.MoreJee.Infrastructure.Specifications.TextureSpecifications
{
    public class TexturePagingSpecification : PagingBaseSpecification<Texture>
    {
        #region ctor
        public TexturePagingSpecification(string clientOrganId, int page, int pageSize, string search, string orderBy, bool desc)
        {
            AppendCriteriaAdd(txt => txt.OrganizationId == clientOrganId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(txt => txt.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
        #endregion
    }
}
