﻿using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccountSpecifications
{
    public class GetNormalUserManagedAccountPagingSpecification : PagingBaseSpecification<Account>
    {
        public GetNormalUserManagedAccountPagingSpecification(string accountId, int page, int pageSize, string orderBy, bool desc, string search, string mail, string phone)
        {
            Page = 0;
            PageSize = 0;
            OrderBy = orderBy;
            Desc = desc;

            AppendCriteriaAdd(account => account.Id == accountId);
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
