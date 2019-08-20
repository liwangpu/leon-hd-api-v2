using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Categories;
using App.MoreJee.API.Application.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{

    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region _GetTreeById 根据分类Id获取分类树信息
        /// <summary>
        /// 根据分类Id获取分类树信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filteredIds"></param>
        /// <returns></returns>
        protected async Task<CategoryTreeQueryDTO> _GetTreeById(string id, string filteredIds = null)
        {
            var dto = await _mediator.Send(new CategoryTreeQuery(id, filteredIds));
            return dto;
        } 
        #endregion

        #region Get 获取分类列表
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<CategoryPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] CategoryPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取分类信息
        /// <summary>
        /// 获取分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new CategoryIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建分类信息
        /// <summary>
        /// 新建分类信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] CategoryCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新分类信息
        /// <summary>
        /// 更新分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<CategoryPatchCommand> patchDoc)
        {
            await _mediator.Send(new CategoryPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Delete 删除分类
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new CategoryDeleteCommand(id));
            return NoContent();
        }
        #endregion

        #region Get GetTree 获取分类树
        /// <summary>
        /// 获取分类树
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filteredIds"></param>
        /// <returns></returns>
        [HttpGet("Tree/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryTreeQueryDTO), 200)]
        public async Task<IActionResult> GetTree(string id, [FromQuery]string filteredIds)
        {
            var dto = await _GetTreeById(id, filteredIds);
            return Ok(dto);
        }

        /// <summary>
        /// 根据资源类型获取分类树
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("Tree")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryTreeQueryDTO), 200)]
        public async Task<IActionResult> GetTreeByResource([FromQuery]CategoryResourceTreeQuery query)
        {
            var id = await _mediator.Send(query);
            var dto = await _GetTreeById(id);
            return Ok(dto);
        }
        #endregion

        #region Post ChangeHierarchy 改变分类树层级结构
        /// <summary>
        /// 改变分类树层级结构
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost("ChangeHierarchy")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ChangeHierarchy([FromBody]CategoryHierarchyChangeCommand cmd)
        {
            await _mediator.Send(cmd);
            return NoContent();
        }
        #endregion

        #region Patch MoveUp 上移分类
        /// <summary>
        /// 上移分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("MoveUp/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> MoveUp(string id)
        {
            await _mediator.Send(new CategoryChangeDisplayCommand(id, true));
            return NoContent();
        }
        #endregion

        #region Patch MoveDown 下移分类
        /// <summary>
        /// 下移分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("MoveDown/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> MoveDown(string id)
        {
            await _mediator.Send(new CategoryChangeDisplayCommand(id));
            return NoContent();
        }
        #endregion
    }
}