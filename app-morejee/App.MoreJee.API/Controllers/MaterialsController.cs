using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Materials;
using App.MoreJee.API.Application.Queries.Materials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 材质管理器
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public MaterialsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取材质列表
        /// <summary>
        /// 获取材质列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<MaterialPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] MaterialPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取材质信息
        /// <summary>
        /// 获取材质信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MaterialIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new MaterialIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建材质信息
        /// <summary>
        /// 新建材质信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MaterialIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] MaterialCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新材质信息
        /// <summary>
        /// 更新材质信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<MaterialPatchCommand> patchDoc)
        {
            await _mediator.Send(new MaterialPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除材质
        /// <summary>
        /// 批量删除材质
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]MaterialBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion
    }
}