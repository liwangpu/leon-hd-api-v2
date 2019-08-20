using App.Base.API.Application.Queries;
using App.Basic.API.Application.Commands.Accounts;
using App.Basic.API.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize]
    [Route("Basic/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get 获取用户信息列表
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagingQueryResult<AccountPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] AccountPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new AccountIdentityQuery() { Id = id });
            return Ok(dto);
        }
        #endregion

        #region Post 新建用户信息
        /// <summary>
        /// 新建用户信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] AccountCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新用户信息
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<AccountPatchCommand> patchDoc)
        {
            await _mediator.Send(new AccountPatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Delete 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new AccountDeleteCommand { Id = id });
            return NoContent();
        }
        #endregion

        #region Get GetProfile 获取当前用户个人信息
        /// <summary>
        /// 获取当前用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Profile")]
        [ProducesResponseType(typeof(AccountProfileQueryDTO), 200)]
        public async Task<IActionResult> GetProfile()
        {
            var dto = await _mediator.Send(new AccountProfileQuery());
            return Ok(dto);
        }
        #endregion

        #region Get GetAccessPoint 获取用户所有权限点
        /// <summary>
        /// 获取用户所有权限点
        /// </summary>
        /// <returns></returns>
        [HttpGet("AccessPointKey")]
        [ProducesResponseType(typeof(AccountAccessPointQueryDTO), 200)]
        public async Task<IActionResult> GetAccessPoint()
        {
            var dto = await _mediator.Send(new AccountAccessPointQuery());
            return Ok(dto);
        } 
        #endregion

        #region Post ResetPassword 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        #endregion

        #region Post BatchDelete 批量删除用户
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete([FromBody]AccountBatchDeleteCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        #endregion

        #region Patch UpdateUserRole 更新用户角色信息
        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("UserRole/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateUserRole(string id, [FromBody] JsonPatchDocument<AccountUserRolePatchCommand> patchDoc)
        {
            await _mediator.Send(new AccountUserRolePatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion
    }
}