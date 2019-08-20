using App.Base.Domain.Common;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;

namespace App.MoreJee.Infrastructure.Specifications.MapSpecifications
{
    public class GetMapByFileAssetIdSpecification : BaseSpecification<Map>
    {
        public GetMapByFileAssetIdSpecification(string fileAssetId)
        {
            Criteria = map => map.CookedAssetId == fileAssetId;
        }
    }
}
