using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetOrganAdminManagedAccountPagingSpecification : PagingBaseSpecification<Account>
    {
        public GetOrganAdminManagedAccountPagingSpecification(string organId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;

            AppendCriteriaAdd(account => account.LegalPerson == EntityStateConst.No);
            AppendCriteriaAdd(account => account.OrganizationId == organId);
            AppendCriteriaAdd(account => account.Active == EntityStateConst.Active);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(account => account.Name.Contains(search));
            if (!string.IsNullOrWhiteSpace(mail))
                AppendCriteriaAdd(account => account.Mail.Contains(mail));
            if (!string.IsNullOrWhiteSpace(phone))
                AppendCriteriaAdd(account => account.Phone.Contains(phone));

            Criteria = CriteriaPredicate;
        }
    }
}
