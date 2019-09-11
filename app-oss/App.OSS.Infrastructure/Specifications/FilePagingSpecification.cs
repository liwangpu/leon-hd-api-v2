using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using App.OSS.Domain.SeedWork;

namespace App.OSS.Infrastructure.Specifications
{
    public class FilePagingSpecification : PagingSpecification<FileAsset>
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
