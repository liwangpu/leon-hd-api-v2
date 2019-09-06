using Apps.OSS.Domain.SeedWork;
using System.Threading.Tasks;

namespace Apps.OSS.Domain.AggregateModels.FileAssetAggregate
{
    public interface IFileAssetRepository: IRepository<FileAsset>
    {
        Task<FileAsset> FindAsync(string id);
        void Add(FileAsset entity);
        void Update(FileAsset entity);
    }
}
