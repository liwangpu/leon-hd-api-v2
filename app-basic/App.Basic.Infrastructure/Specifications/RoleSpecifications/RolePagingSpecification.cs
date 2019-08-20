using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;

namespace App.Basic.Infrastructure.Specifications.RoleSpecifications
{
    public class RolePagingSpecification: PagingBaseSpecification<SystemRole>
    {
        #region ctor
        public RolePagingSpecification(int page, int pageSize, string search)
        {
            Page = page;
            PageSize = pageSize;
            Criteria = user => string.IsNullOrWhiteSpace(search) ? true : user.Name.Contains(search);
        }
        #endregion
    }
}
