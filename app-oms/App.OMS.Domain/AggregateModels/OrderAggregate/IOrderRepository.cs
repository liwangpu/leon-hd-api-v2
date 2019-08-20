using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.OMS.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task LoadOwnOrderItemsAsync(Order entity);
        Task LoadCustomerAsync(Order entity);
    }
}
