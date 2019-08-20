using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.StaticMeshs;
using App.MoreJee.API.Application.Queries.StaticMeshs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 模型信息管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class StaticMeshsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public StaticMeshsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取模型列表
        /// <summary>
        /// 获取模型列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<StaticMeshPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] StaticMeshPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取模型信息
        /// <summary>
        /// 获取模型信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StaticMeshIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new StaticMeshIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建模型信息
        /// <summary>
        /// 新建模型信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(StaticMeshIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] StaticMeshCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新模型信息
        /// <summary>
        /// 更新模型信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<StaticMeshPatchCommand> patchDoc)
        {
            await _mediator.Send(new StaticMeshPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除模型
        /// <summary>
        /// 批量删除模型
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]StaticMeshBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion
    }
}