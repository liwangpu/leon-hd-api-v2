using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public interface IAccessPointRepository : IRepository<AccessPoint>
    {
        Task<AccessPoint> FindByPointKeyAsync(string key);
    }
}
