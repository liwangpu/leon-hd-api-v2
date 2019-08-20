using App.Base.API.Application.Queries;
using App.OMS.API.Application.Commands.Customers;
using App.OMS.API.Application.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.OMS.API.Controllers
{
    /// <summary>
    /// 客户管理
    /// </summary>
    [Authorize]
    [Route("OMS/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取客户列表
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<CustomerPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] CustomerPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取客户信息
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new CustomerIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建客户信息
        /// <summary>
        /// 新建客户信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] CustomerCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion
    }
}