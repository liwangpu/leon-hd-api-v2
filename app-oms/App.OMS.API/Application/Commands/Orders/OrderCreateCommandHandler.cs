using App.Base.API.Infrastructure.Services;
using App.MoreJee.Export;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Commands.Orders
{
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, string>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IIdentityService identityService;
        private readonly ProductSpecService productSpecService;
        private readonly ProductService productService;

        #region ctor
        public OrderCreateCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, ProductSpecService productSpecService, ProductService productService)
        {
            this.orderRepository = orderRepository;
            this.identityService = identityService;
            this.productSpecService = productSpecService;
            this.productService = productService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.Name, request.Description, identityService.GetOrganizationId(), identityService.GetUserId());

            var productSpecs = await productSpecService.GetBriefByIds(request.OrderItems != null ? request.OrderItems.Select(x => x.ProductSpecId) : null);
            var products = await productService.GetBriefByIds(productSpecs != null ? productSpecs.Select(x => x.ProductId) : null);

            request.OrderItems.ForEach(it =>
            {
                var spec = productSpecs.First(x => x.Id == it.ProductSpecId);
                var refProduct = products.First(x => x.Id == spec.ProductId);
                order.AddItem(refProduct.Id, refProduct.Name, refProduct.Description, refProduct.Icon, refProduct.Brand, refProduct.Unit, spec.Id, spec.Name, spec.Description, spec.Icon, it.Num, it.UnitPrice, it.Remark);
            });

            order.OrderItemSummary();
            await orderRepository.AddAsync(order);
            return order.Id;
        }
        #endregion
    }
}
