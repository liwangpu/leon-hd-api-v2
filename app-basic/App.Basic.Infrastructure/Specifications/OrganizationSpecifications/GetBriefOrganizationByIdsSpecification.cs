using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using System;
using System.Linq;

namespace App.Basic.Infrastructure.Specifications.OrganizationSpecifications
{
    public class GetBriefOrganizationByIdsSpecification : Specification<Organization>
    {
        public GetBriefOrganizationByIdsSpecification(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                AppendCriteriaAdd(m => false);


            var idArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            AppendCriteriaAdd(m => idArr.Contains(m.Id));

            Criteria = CriteriaPredicate;
        }
    }
}
