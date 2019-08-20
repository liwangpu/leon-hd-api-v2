using App.Base.API.Application.Queries;
using App.Basic.API.Application.Commands.AccessPoints;
using App.Basic.API.Application.Queries.AccessPoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 权限点管理
    /// </summary>
    [Authorize]
    [Route("Basic/[controller]")]
    [ApiController]
    public class AccessPointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public AccessPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取权限点信息列表
        /// <summary>
        /// 获取权限点信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<AccessPointPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] AccessPointPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取权限点信息
        /// <summary>
        /// 获取权限点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccessPointIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new AccessPointIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建权限点信息
        /// <summary>
        /// 新建权限点信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccessPointIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] AccessPointCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新权限点信息
        /// <summary>
        /// 更新权限点信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<AccessPointPatchCommand> patchDoc)
        {
            var cmd = new AccessPointPatchCommand(id, patchDoc);
            cmd.ApplyPatch();
            await _mediator.Send(cmd);
            return NoContent();
        }
        #endregion

        #region Get CheckExistPointKey 校验用户是否有某个权限点
        /// <summary>
        /// 校验用户是否有某个权限点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("Check")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> CheckExistPointKey([FromQuery] CheckExistAssetPointQuery query)
        {
            var dto = await _mediator.Send(query);
            return Ok(dto);
        } 
        #endregion
    }
}