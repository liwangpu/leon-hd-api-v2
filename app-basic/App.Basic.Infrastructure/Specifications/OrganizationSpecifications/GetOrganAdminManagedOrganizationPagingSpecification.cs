using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class GetOrganAdminManagedOrganizationPagingSpecification : PagingBaseSpecification<Organization>
    {

        public GetOrganAdminManagedOrganizationPagingSpecification(string organId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;

            AppendCriteriaAdd(organ => organ.Active == EntityStateConst.Active);
            AppendCriteriaAdd(organ => organ.ParentId == organId);

            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(organ => organ.Name.Contains(search));
            if (!string.IsNullOrWhiteSpace(mail))
                AppendCriteriaAdd(organ => organ.Mail.Contains(mail));
            if (!string.IsNullOrWhiteSpace(phone))
                AppendCriteriaAdd(organ => organ.Phone.Contains(phone));

            Criteria = CriteriaPredicate;
        }
    }
}
