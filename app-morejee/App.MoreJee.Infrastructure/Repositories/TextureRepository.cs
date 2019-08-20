using App.Base.Domain.Common;
using App.Base.Infrastructure;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Infrastructure.Repositories
{
    public class TextureRepository : ITextureRepository
    {

        public MoreJeeAppContext _context { get; }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        #region ctor
        public TextureRepository(MoreJeeAppContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<Texture> FindAsync(string id)
        {
            return await _context.Set<Texture>().FindAsync(id);
        }

        public IQueryable<Texture> Get(ISpecification<Texture> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Texture>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).AsNoTracking();
        }

        public IQueryable<Texture> Paging(IPagingSpecification<Texture> specification)
        {
            var queryableResult = specification.Includes.Aggregate(_context.Set<Texture>().AsQueryable(), (current, include) => current.Include(include));
            return queryableResult.Where(specification.Criteria).OrderBy(specification.OrderBy, specification.Desc).Skip((specification.Page - 1) * specification.PageSize).Take(specification.PageSize).AsNoTracking();
        }

        public async Task AddAsync(Texture entity)
        {
            _context.Set<Texture>().Add(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task UpdateAsync(Texture entity)
        {
            _context.Set<Texture>().Update(entity);
            await _context.SaveEntitiesAsync();
        }

        public async Task DeleteAsync(string id, string operatorId)
        {
            var data = await FindAsync(id);
            if (data == null) return;
            data.DeleteClientAsset();
            _context.Set<Texture>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }

        public async Task DeleteAsync(Texture data, string operatorId)
        {
            data.DeleteClientAsset();
            _context.Set<Texture>().Remove(data);
            await _context.SaveEntitiesAsync(false);
        }
    }
}
