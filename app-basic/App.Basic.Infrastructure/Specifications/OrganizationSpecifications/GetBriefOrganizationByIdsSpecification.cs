using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;
using System;
using System.Linq;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class GetBriefOrganizationByIdsSpecification : BaseSpecification<Organization>
    {
        public GetBriefOrganizationByIdsSpecification(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                AppendCriteriaAdd(m => false);


            var idArr = ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            AppendCriteriaAdd(m => idArr.Contains(m.Id));

            Criteria = CriteriaPredicate;
        }
    }
}
