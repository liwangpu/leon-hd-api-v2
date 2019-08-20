using App.Base.Domain.Common;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IQueryable<Account>> GetManagedAccount(string userId);
        Task LoadOrganizationAsync(Account entity);
        Task LoadOwnRolesAsync(Account entity);
    }
}
