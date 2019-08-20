using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;

namespace App.MoreJee.Infrastructure.Specifications.StaticMeshSpecifications
{
    public class StaticMeshPagingSpecification : PagingBaseSpecification<StaticMesh>
    {
        #region ctor
        public StaticMeshPagingSpecification(string clientOrganId, int page, int pageSize, string search)
        {
            AppendCriteriaAdd(txt => txt.OrganizationId == clientOrganId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(mesh => mesh.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            Criteria = CriteriaPredicate;
        }
        #endregion
    }
}
