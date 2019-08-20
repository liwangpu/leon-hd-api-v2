using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;

namespace App.Basic.Infrastructure.Specifications.OrganizationTreeSpecifications
{
    public class GetOrganizationTreeNodeByObjIdSpecification : BaseSpecification<OrganizationTree>
    {
        #region ctor
        public GetOrganizationTreeNodeByObjIdSpecification(string objId)
        {
            Criteria = tree => tree.ObjId == objId;

        }
        #endregion

    }
}
