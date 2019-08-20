using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.Products;
using App.MoreJee.API.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取产品列表
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<ProductPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] ProductPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取产品信息
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new ProductIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建产品信息
        /// <summary>
        /// 新建产品信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] ProductCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新产品信息
        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<ProductPatchCommand> patchDoc)
        {
            await _mediator.Send(new ProductPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Delete 删除产品信息
        /// <summary>
        /// 删除产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok();
        } 
        #endregion

        #region Get GetBrief 获取简洁的产品信息

        private async Task<ProductBriefIdentityQueryDTO> _GetBrief(string id)
        {
            return await _mediator.Send(new ProductBriefIdentityQuery(id));
        }

        /// <summary>
        /// 获取简洁的产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}/Brief")]
        [ProducesResponseType(typeof(ProductBriefIdentityQueryDTO), 200)]
        public async Task<IActionResult> GetBrief(string id)
        {
            var dto = await _GetBrief(id);
            return Ok(dto);
        }

        /// <summary>
        /// 批量获取简洁的产品信息
        /// </summary>
        /// <param name="ids">逗号分隔的id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Brief")]
        [ProducesResponseType(typeof(List<ProductBriefIdentityQueryDTO>), 200)]
        public async Task<IActionResult> GetBriefByIds([FromQuery]string ids)
        {
            var dtos = new List<ProductBriefIdentityQueryDTO>();
            var idArr = ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (var id in idArr)
                {
                    var dto = await _GetBrief(id);
                    dtos.Add(dto);
                }
            }
            catch
            { }
            return Ok(dtos);
        }
        #endregion

        #region Post BatchDelete 批量删除产品
        /// <summary>
        /// 批量删除产品
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]ProductBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion

    }
}