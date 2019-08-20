using App.Base.Domain.Common;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task LoadOwnAccountsAsync(Organization entity);
        IQueryable<Organization> GetManagedOrganization(string userId);
        IQueryable<Organization> GetTopTreeOrganization(string organId);
    }
}
