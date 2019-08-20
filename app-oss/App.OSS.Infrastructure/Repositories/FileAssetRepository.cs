using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.OSS.Infrastructure.Repositories
{
    public class FileAssetRepository : IFileAssetRepository
    {

        public OSSAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public FileAssetRepository(OSSAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<FileAsset> FindAsync(string id)
        {
            return await _context.Set<FileAsset>().FindAsync(id);
        }

        public IQueryable<FileAsset> Get(ISpecification<FileAsset> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<FileAsset>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<FileAsset> Paging(IPagingSpecification<FileAsset> specification)
        {
            var noOrder = string.IsNullOrWhiteSpace(specification.OrderBy);
            var queryableResult = specification.Includes.Aggregate(_context.Set<FileAsset>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(noOrder ? "modifiedTime" : specification.OrderBy, noOrder ? true : specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(FileAsset entity)
        {
            _context.Set<FileAsset>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(FileAsset entity)
        {
            _context.Set<FileAsset>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public Task DeleteAsync(string id, string operatorId)
        {
            return Task.CompletedTask;
        }

    }
}
