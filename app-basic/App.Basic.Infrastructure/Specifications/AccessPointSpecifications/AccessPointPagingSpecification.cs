﻿using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.AccessPointSpecifications
{
    public class AccessPointPagingSpecification : PagingBaseSpecification<AccessPoint>
    {
        #region ctor
        public AccessPointPagingSpecification(int page, int pageSize, string orderBy, bool desc, string search, string applyOrganTypeId)
        {
            if (!string.IsNullOrWhiteSpace(search))
                AppendCriteriaAdd(map => map.Name.Contains(search));

            if(applyOrganTypeId != OrganizationType.ServiceProvider.Id.ToString())
                AppendCriteriaAdd(map => map.ApplyOranizationTypeIds.Contains(applyOrganTypeId)||string.IsNullOrWhiteSpace(map.ApplyOranizationTypeIds));

            Page = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            Desc = desc;
            Criteria = CriteriaPredicate;
        }
        #endregion
    }
}
