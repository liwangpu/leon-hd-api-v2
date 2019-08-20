using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Textures;
using App.MoreJee.API.Application.Queries.Textures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 贴图管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class TexturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public TexturesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取贴图列表
        /// <summary>
        /// 获取贴图列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<TexturePagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] TexturePagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取贴图信息
        /// <summary>
        /// 获取贴图信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TextureIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new TextureIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建贴图信息
        /// <summary>
        /// 新建贴图信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TextureIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] TextureCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新贴图信息
        /// <summary>
        /// 更新贴图信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<TexturePatchCommand> patchDoc)
        {
            await _mediator.Send(new TexturePatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除贴图
        /// <summary>
        /// 批量删除贴图
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]TextureBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion

    }
}