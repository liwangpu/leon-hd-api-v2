using App.OSS.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.OSS.Domain.AggregateModels.FileAssetAggregate
{
    public interface IFileAssetRepository : IRepository<FileAsset>
    {
        void Add(FileAsset entity);
        void Update(FileAsset entity);
        void Delete(string id, string operatorId);
        Task<FileAsset> FindAsync(string id);
        IQueryable<FileAsset> Get(ISpecification<FileAsset> specification);
        IQueryable<FileAsset> Paging(IPagingSpecification<FileAsset> specification);
    }
}
