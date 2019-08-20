using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetApplicationAdminManagedAccountPagingSpecification : PagingBaseSpecification<Account>
    {
        public GetApplicationAdminManagedAccountPagingSpecification(int page, int pageSize, string search)
        {
            Page = page;
            PageSize = pageSize;

            AppendCriteriaAdd(account => account.LegalPerson == EntityStateConst.Yes);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);
            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(account => account.Name.Contains(search));
            Criteria = CriteriaPredicate;
        }
    }
}
