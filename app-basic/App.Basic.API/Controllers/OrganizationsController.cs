using App.Base.API.Application.Queries;
using App.Basic.API.Application.Commands.Organizations;
using App.Basic.API.Application.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 组织管理
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Basic/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取组织信息列表
        /// <summary>
        /// 获取组织信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<OrganizationPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] OrganizationPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取组织信息
        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizationIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new OrganizationIdentityQuery() { Id = id });
            return Ok(dto);
        }
        #endregion

        #region Post 新建组织信息
        /// <summary>
        /// 新建组织信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrganizationIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] OrganizationCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 修改组织信息
        /// <summary>
        /// 修改组织信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<OrganizationPatchCommand> patchDoc)
        {
            await _mediator.Send(new OrganizationPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Post BatchDelete 批量删除组织
        /// <summary>
        /// 批量删除组织
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]OrganizationBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion

        #region Get GetBriefByIds 根据Ids获取组织简要信息
        /// <summary>
        /// 根据Ids获取组织简要信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Brief")]
        [ProducesResponseType(typeof(List<OrganizationBriefIdentitiesQueryDTO>), 200)]
        public async Task<IActionResult> GetBriefByIds([FromQuery] OrganizationBriefIdentitiesQuery query)
        {
            var dto = await _mediator.Send(query);
            return Ok(dto);
        }
        #endregion

        #region Get ClientAssetOrganIdRedirection 客户端资源组织Id重定向
        /// <summary>
        /// 客户端资源组织Id重定向
        /// </summary>
        /// <returns></returns>
        [HttpGet("ClientAssetOrganIdRedirection")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ClientAssetOrganIdRedirection()
        {
            var dto = await _mediator.Send(new OrganizationClientAssetOrganIdRedirectionQuery());
            return Ok(dto);
        } 
        #endregion
    }
}