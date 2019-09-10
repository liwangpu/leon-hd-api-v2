using App.Basic.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public interface IAccessPointRepository : IRepository<AccessPoint>
    {
        Task<AccessPoint> FindByPointKeyAsync(string key);
        void Add(AccessPoint entity);
        void Update(AccessPoint entity);
        Task DeleteAsync(string id, string operatorId);
        Task<AccessPoint> FindAsync(string id);
        IQueryable<AccessPoint> Get(ISpecification<AccessPoint> specification);
        IQueryable<AccessPoint> Paging(IPagingSpecification<AccessPoint> specification);
    }
}
