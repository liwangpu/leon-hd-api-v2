using App.Basic.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        void Add(UserRole entity);
        Task DeleteAsync(string id, string operatorId);
        Task<UserRole> FindAsync(string id);
        IQueryable<UserRole> Get(ISpecification<UserRole> specification);
        IQueryable<UserRole> Paging(IPagingSpecification<UserRole> specification);
    }
}
