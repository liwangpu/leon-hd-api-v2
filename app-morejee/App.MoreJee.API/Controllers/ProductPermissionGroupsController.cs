using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.ProductPermissionGroups;
using App.MoreJee.API.Application.Queries.ProductPermissionGroups;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// 产品权限组管理
    /// </summary>
    [Authorize]
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class ProductPermissionGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public ProductPermissionGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取产品权限组列表
        /// <summary>
        /// 获取产品权限组列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<ProductPermissionGroupPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] ProductPermissionGroupPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取产品权限组信息
        /// <summary>
        /// 获取产品权限组信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductPermissionGroupIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new ProductPermissionGroupIdentityQuery(id));
            return Ok(dto);
        }
        #endregion

        #region Post 新建产品权限组信息
        /// <summary>
        /// 新建产品权限组信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductPermissionGroupIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] ProductPermissionGroupCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新产品权限组信息
        /// <summary>
        /// 更新产品权限组信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<ProductPermissionGroupPatchCommand> patchDoc)
        {
            await _mediator.Send(new ProductPermissionGroupPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除产品权限组
        /// <summary>
        /// 批量删除产品权限组
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]ProductPermissionGroupBatchDeleteCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region Get GetOwnOrganization 获取产品权限组的所有组织
        /// <summary>
        /// 获取产品权限组的所有组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}/Organization")]
        [ProducesResponseType(typeof(List<ProdutPermissionGroupOwnOrganQueryDTO>), 200)]
        public async Task<IActionResult> GetOwnOrganization(string id, [FromQuery]ProdutPermissionGroupOwnOrganQuery query)
        {
            query.ProductPermissionGroupId = id;
            var dto = await _mediator.Send(query);
            return Ok(dto);
        }
        #endregion

        #region Post AddOwnOrganization 添加产品权限组的组织
        /// <summary>
        /// 添加产品权限组的组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/Organization")]
        public async Task<IActionResult> AddOwnOrganization(string id, [FromBody]ProductPermissionGroupAddOrganCommand command)
        {
            command.ProductPermissionGroupId = id;
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region Post DeleteOwnOrganization 批量删除产品权限组的组织
        /// <summary>
        /// 批量删除产品权限组的组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/Organization/BatchDelete")]
        public async Task<IActionResult> DeleteOwnOrganization(string id, [FromBody]ProductPermissionGroupDeleteOrganCommand command)
        {
            command.ProductPermissionGroupId = id;
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region Post AddOwnProduct 添加产品权限组的产品
        /// <summary>
        /// 添加产品权限组的产品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/Product")]
        public async Task<IActionResult> AddOwnProduct(string id, [FromBody]ProductPermissionGroupAddProductCommand command)
        {
            command.ProductPermissionGroupId = id;
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region Get GetOwnProduct 获取产品权限组的所有产品
        /// <summary>
        /// 获取产品权限组的所有产品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}/Product")]
        [ProducesResponseType(typeof(List<ProductPermissionGroupOwnProductItemDTO>), 200)]
        public async Task<IActionResult> GetOwnProduct(string id, [FromQuery]ProductPermissionGroupOwnProductQuery query)
        {
            query.ProductPermissionGroupId = id;
            var dto = await _mediator.Send(query);
            return Ok(dto);
        }
        #endregion

        #region Post DeleteOwnProduct 批量删除产品权限组的产品
        /// <summary>
        /// 批量删除产品权限组的产品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/Product/BatchDelete")]
        public async Task<IActionResult> DeleteOwnProduct(string id, [FromBody]ProductPermissionGroupDeleteProductCommand command)
        {
            command.ProductPermissionGroupId = id;
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion









        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //[HttpGet("Product")]
        //[ProducesResponseType(typeof(List<OrganizationAllPermissionProductDTO>), 200)]

        //public async Task<IActionResult> GetOwnProduct([FromQuery]OrganizationAllPermissionProductQuery query)
        //{
        //    var dto = await _mediator.Send(query);
        //    return Ok(dto);
        //}
    }
}