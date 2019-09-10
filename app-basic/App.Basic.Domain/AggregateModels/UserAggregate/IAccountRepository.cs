using App.Basic.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IQueryable<Account>> GetManagedAccount(string userId);
        Task LoadOrganizationAsync(Account entity);
        Task LoadOwnRolesAsync(Account entity);
        void Add(Account entity);
        void Update(Account entity);
        Task DeleteAsync(string id, string operatorId);
        Task<Account> FindAsync(string id);
        IQueryable<Account> Get(ISpecification<Account> specification);
        IQueryable<Account> Paging(IPagingSpecification<Account> specification);
    }
}
