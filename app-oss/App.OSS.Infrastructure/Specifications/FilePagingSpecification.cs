using App.Base.Domain.Common;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;

namespace App.OSS.Infrastructure.Specifications
{
    public class FilePagingSpecification : PagingBaseSpecification<FileAsset>
    {
        #region ctor
        public FilePagingSpecification(int page, int pageSize, string search, string orderBy, bool desc)
        {
            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(user => user.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
        #endregion
    }
}
