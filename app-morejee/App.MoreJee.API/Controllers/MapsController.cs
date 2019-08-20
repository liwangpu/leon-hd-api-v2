using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Maps;
using App.MoreJee.API.Application.Queries.Maps;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 地图管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public MapsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取场景列表
        /// <summary>
        /// 获取场景列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<MapPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] MapPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取场景信息
        /// <summary>
        /// 获取场景信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MapIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new MapIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建场景信息
        /// <summary>
        /// 新建场景信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MapIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] MapCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新场景信息
        /// <summary>
        /// 更新场景信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<MapPatchCommand> patchDoc)
        {
            await _mediator.Send(new MapPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除场景
        /// <summary>
        /// 批量删除场景
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]MapBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion
    }
}