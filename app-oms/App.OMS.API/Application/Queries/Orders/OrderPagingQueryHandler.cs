using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using App.OMS.Infrastructure.Specifications.OrderSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Queries.Orders
{
    public class OrderPagingQueryHandler : IRequestHandler<OrderPagingQuery, PagingQueryResult<OrderPagingQueryDTO>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public OrderPagingQueryHandler(IOrderRepository orderRepository, IIdentityService identityService)
        {
            this.orderRepository = orderRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<OrderPagingQueryDTO>> Handle(OrderPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<OrderPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new OrderPagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            result.Total = await orderRepository.Get(specification).CountAsync();
            result.Data = await orderRepository.Paging(specification).Select(x => OrderPagingQueryDTO.From(x)).ToListAsync();
            return result;
        }
        #endregion
    }
}
