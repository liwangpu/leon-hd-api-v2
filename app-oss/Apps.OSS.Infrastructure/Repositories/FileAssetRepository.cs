using Apps.OSS.Domain.AggregateModels.FileAssetAggregate;
using Apps.OSS.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Apps.OSS.Infrastructure.Repositories
{
    public class FileAssetRepository : IFileAssetRepository
    {

        private readonly OSSAppContext context;

        public IUnitOfWork UnitOfWork => context;

        #region ctor
        public FileAssetRepository(OSSAppContext context)
        {
            this.context = context;
        }
        #endregion


        public async Task<FileAsset> FindAsync(string id)
        {
            return await context.Set<FileAsset>().FindAsync(id);
        }

        public void Add(FileAsset entity)
        {
            context.Set<FileAsset>().Add(entity);
        }

        public void Update(FileAsset entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
