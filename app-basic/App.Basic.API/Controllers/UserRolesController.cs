using App.Basic.API.Application.Queries.UserRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    [Route("Basic/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public UserRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 根据用户Id获取用户角色信息
        /// <summary>
        /// 根据用户Id获取用户角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserRolePagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] UserRoleQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        #endregion

        //#region Post 新建用户角色信息
        ///// <summary>
        ///// 新建用户角色信息
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(typeof(UserRoleCreateCommandDTO), 200)]
        //public async Task<IActionResult> Post([FromBody] UserRoleCreateCommand command)
        //{
        //    var dto = await _mediator.Send(command);
        //    return Ok(dto);
        //}
        //#endregion

        //[HttpDelete]
        //public async Task<IActionResult> BatchDelete([FromQuery] string ids)
        //{
        //    var result = await _mediator.Send(new UserRoleBatchDeleteCommand(ids));
        //    return result;
        //}
    }
}