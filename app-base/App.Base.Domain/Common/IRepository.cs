using System.Linq;
using System.Threading.Tasks;

namespace App.Base.Domain.Common
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id, string operatorId);
        Task<T> FindAsync(string id);
        IQueryable<T> Get(ISpecification<T> specification);
        IQueryable<T> Paging(IPagingSpecification<T> specification);
    }
}
