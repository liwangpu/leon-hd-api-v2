using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetApplicationAdminManagedAccountPagingSpecification : PagingSpecification<Account>
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
