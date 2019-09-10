using App.Basic.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task LoadOwnAccountsAsync(Organization entity);
        IQueryable<Organization> GetManagedOrganization(string userId);
        IQueryable<Organization> GetTopTreeOrganization(string organId);
        void Add(Organization entity);
        void Update(Organization entity);
        Task DeleteAsync(string id, string operatorId);
        Task<Organization> FindAsync(string id);
        IQueryable<Organization> Get(ISpecification<Organization> specification);
        IQueryable<Organization> Paging(IPagingSpecification<Organization> specification);
    }
}
