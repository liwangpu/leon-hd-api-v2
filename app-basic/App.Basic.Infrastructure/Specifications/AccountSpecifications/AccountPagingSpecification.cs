using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Domain.SeedWork;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class AccountPagingSpecification : PagingSpecification<Account>
    {
        public AccountPagingSpecification(string organizationId, int page, int pageSize, string orderBy, bool desc, string search)
        {
            AppendCriteriaAdd(x => x.LegalPerson == EntityStateConst.No);
            AppendCriteriaAdd(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(m => m.Name.Contains(search));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
    }
}
