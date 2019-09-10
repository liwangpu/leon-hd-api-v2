using App.Basic.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public interface ICustomRoleRepository : IRepository<CustomRole>
    {
        void Add(CustomRole entity);
        void Update(CustomRole entity);
        Task DeleteAsync(string id, string operatorId);
        Task<CustomRole> FindAsync(string id);
        IQueryable<CustomRole> Get(ISpecification<CustomRole> specification);
        IQueryable<CustomRole> Paging(IPagingSpecification<CustomRole> specification);
    }
}
