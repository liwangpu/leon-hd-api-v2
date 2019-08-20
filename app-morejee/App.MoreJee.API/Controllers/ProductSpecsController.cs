using App.MoreJee.API.Application.Commands.ProductSpecs;
using App.MoreJee.API.Application.Queries.ProductSpecs;
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
    /// 产品规格管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class ProductSpecsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public ProductSpecsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取产品规格信息
        /// <summary>
        /// 获取产品规格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductSpecIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new ProductSpecIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建产品规格信息
        /// <summary>
        /// 新建产品规格信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductSpecIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] ProductSpecCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新产品规格信息
        /// <summary>
        /// 更新产品规格信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<ProductSpecPatchCommand> patchDoc)
        {
            await _mediator.Send(new ProductSpecPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Get GetBrief 获取简洁的规格信息

        private async Task<ProductSpecBriefIdentityQueryDTO> _GetBrief(string id)
        {
            return await _mediator.Send(new ProductSpecBriefIdentityQuery(id));
        }
        /// <summary>
        /// 获取简洁的规格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}/Brief")]
        [ProducesResponseType(typeof(ProductSpecBriefIdentityQueryDTO), 200)]
        public async Task<IActionResult> GetBrief(string id)
        {
            var dto = await _GetBrief(id);
            return Ok(dto);
        }


        /// <summary>
        /// 批量获取简洁的规格信息
        /// </summary>
        /// <param name="ids">逗号分隔的id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Brief")]
        [ProducesResponseType(typeof(List<ProductSpecBriefIdentityQueryDTO>), 200)]
        public async Task<IActionResult> GetBriefByIds([FromQuery]string ids)
        {
            var dtos = new List<ProductSpecBriefIdentityQueryDTO>();
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
    }
}