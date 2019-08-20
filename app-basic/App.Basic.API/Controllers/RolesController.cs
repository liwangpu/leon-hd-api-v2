using App.Basic.API.Application.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Authorize]
    [Route("Basic/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取角色信息列表
        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RolePagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取角色信息
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _mediator.Send(new RoleIdentityQuery() { Id = id });
            return Ok(dto);
        }
        #endregion

    }
}