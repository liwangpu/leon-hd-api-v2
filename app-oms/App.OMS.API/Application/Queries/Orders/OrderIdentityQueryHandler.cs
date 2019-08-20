using App.OMS.Domain.AggregateModels.OrderAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Queries.Orders
{
    public class OrderIdentityQueryHandler : IRequestHandler<OrderIdentityQuery, OrderIdentityQueryDTO>
    {
        private readonly IOrderRepository orderRepository;
        #region ctor
        public OrderIdentityQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        #endregion

        #region Handle
        public async Task<OrderIdentityQueryDTO> Handle(OrderIdentityQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.FindAsync(request.Id);
            //await orderRepository.LoadCustomerAsync(order);
            await orderRepository.LoadOwnOrderItemsAsync(order);

            return OrderIdentityQueryDTO.From(order);
        }
        #endregion
    }
}
