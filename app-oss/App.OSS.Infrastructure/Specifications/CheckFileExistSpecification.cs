using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using App.OSS.Domain.SeedWork;

namespace App.OSS.Infrastructure.Specifications
{
    public class CheckFileExistSpecification : Specification<FileAsset>
    {
        public CheckFileExistSpecification(string md5)
        {
            Criteria = file => file.Id == md5;
        }
    }
}
