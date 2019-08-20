using App.Basic.API.Application.Queries.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace App.Basic.API.Controllers
{
    [Route("Basic/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public TokensController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Post 请求Token信息
        /// <summary>
        /// 请求Token信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TokenRequestQuery query)
        {
            var dto = await _mediator.Send(query);
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(dto.Token), Expires = dto.Expires.ToString("yyyy-MM-dd HH:mm:ss") });
        }
        #endregion
    }
}