using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Solutions;
using App.MoreJee.API.Application.Queries.Solutions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 方案管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class SolutionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public SolutionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取方案列表
        /// <summary>
        /// 获取方案列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<SolutionPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] SolutionPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取方案信息
        /// <summary>
        /// 获取方案信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SolutionIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new SolutionIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建方案信息
        /// <summary>
        /// 新建方案信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SolutionIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] SolutionCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新方案信息
        /// <summary>
        /// 更新方案信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<SolutionPatchCommand> patchDoc)
        {
            await _mediator.Send(new SolutionPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除方案
        /// <summary>
        /// 批量删除方案
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> BatchDelete([FromBody]SolutionBatchDeleteCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion
    }
}