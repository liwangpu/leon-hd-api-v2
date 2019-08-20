using App.OMS.Domain.AggregateModels.OrderAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Commands.Orders
{
    public class OrderCustomerCreateCommandHandler : IRequestHandler<OrderCustomerCreateCommand>
    {
        private readonly IOrderRepository orderRepository;

        #region ctor
        public OrderCustomerCreateCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(OrderCustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.FindAsync(request.OrderId);
            order.UpdateContactInfo(request.CustomerId, request.Name, request.Phone, request.Mail, request.Address);
            await orderRepository.UpdateAsync(order);
            return Unit.Value;
        } 
        #endregion
    }
}
