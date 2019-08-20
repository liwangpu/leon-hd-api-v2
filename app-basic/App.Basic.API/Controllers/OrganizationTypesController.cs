using App.Base.API.Application.Queries;
using App.Basic.API.Application.Queries.OrganizationTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 组织类型管理
    /// </summary>
    //[Authorize]
    [AllowAnonymous]
    [Route("Basic/[controller]")]
    [ApiController]
    public class OrganizationTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public OrganizationTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取组织类型信息列表
        /// <summary>
        /// 获取组织类型信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<OrganizationTypePagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] OrganizationTypePagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取组织类型信息
        /// <summary>
        /// 获取组织类型信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizationTypeIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _mediator.Send(new OrganizationTypeIdentityQuery(id));
            return Ok(dto);
        }
        #endregion
    }
}