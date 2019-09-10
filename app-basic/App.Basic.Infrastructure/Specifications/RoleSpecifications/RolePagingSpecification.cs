
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.RoleSpecifications
{
    public class RolePagingSpecification: PagingSpecification<SystemRole>
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
