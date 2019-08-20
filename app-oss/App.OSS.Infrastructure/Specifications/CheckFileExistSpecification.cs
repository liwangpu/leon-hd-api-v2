using App.Base.Domain.Common;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;

namespace App.OSS.Infrastructure.Specifications
{
    public class CheckFileExistSpecification : BaseSpecification<FileAsset>
    {
        public CheckFileExistSpecification(string md5)
        {
            Criteria = file => file.Id == md5;
        }
    }
}
