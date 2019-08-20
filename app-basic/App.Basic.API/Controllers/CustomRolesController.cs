using App.Base.API.Application.Queries;
using App.Basic.API.Application.Commands.CustomRoles;
using App.Basic.API.Application.Queries.CustomRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 用户自定义角色管理
    /// </summary>
    [Authorize]
    [Route("Basic/[controller]")]
    [ApiController]
    public class CustomRolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public CustomRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取角色列表
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<CustomRolePagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] CustomRolePagingQuery query)
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
        [ProducesResponseType(typeof(CustomRoleIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new CustomRoleIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建角色信息
        /// <summary>
        /// 新建角色信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomRoleIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] CustomRoleCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新角色信息
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<CustomRoleBatchCommand> patchDoc)
        {
            await _mediator.Send(new CustomRoleBatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

    }
}