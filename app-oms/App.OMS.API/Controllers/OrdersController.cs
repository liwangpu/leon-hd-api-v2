using App.Base.API.Application.Queries;
using App.OMS.API.Application.Commands.Orders;
using App.OMS.API.Application.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.OMS.API.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    [Authorize]
    [Route("OMS/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取订单列表
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<OrderPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] OrderPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取订单信息
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new OrderIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建订单信息
        /// <summary>
        /// 新建订单信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] OrderCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Post UpdateCustomer 创建订单客户信息
        /// <summary>
        /// 创建订单客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/Customer")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateOrderCustomer(string id, [FromBody] OrderCustomerCreateCommand command)
        {
            command.OrderId = id;
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion
    }
}