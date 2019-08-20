using App.Base.API.Application.Queries;
using App.MoreJee.API.Application.Commands.PackageMaps;
using App.MoreJee.API.Application.Queries.PackageMaps;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.MoreJee.API.Controllers
{
    /// <summary>
    /// PackageMap管理
    /// </summary>
    [Route("MoreJee/[controller]")]
    [ApiController]
    public class PackageMapsController : ControllerBase
    {
        private readonly IMediator _mediator;

        #region ctor
        public PackageMapsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region _GetMapById
        protected async Task<PackageMapIdentityQueryDTO> _GetMapById(string id, string mapType)
        {
            return await _mediator.Send(new PackageMapIdentityQuery(id, mapType.ToLower()));
        }
        #endregion

        #region Get 获取PackageMap映射列表
        /// <summary>
        /// 获取PackageMap映射列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<PackageMapPagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] PackageMapPagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取PackageMap信息
        /// <summary>
        /// 获取PackageMap信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mapType">source,uncooked,win64,android,ios</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PackageMapIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id, string mapType = "win64")
        {
            var dto = await _GetMapById(id, mapType);
            return Ok(dto);
        }
        #endregion

        #region GetByPackageNames 根据Packages获取PackageMap信息
        /// <summary>
        /// 根据Packages获取PackageMap信息
        /// </summary>
        /// <param name="packages">逗号分隔的package</param>
        /// <param name="mapType">source,uncooked,win64,android,ios</param>
        /// <returns></returns>
        [HttpGet("Package")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<PackageMapIdentityQueryDTO>), 200)]
        public async Task<IActionResult> GetByPackageNames(string packages, string mapType = "win64")
        {
            var ids = await _mediator.Send(new PackageMapNamesMapQuery(packages));
            var dtos = new List<PackageMapIdentityQueryDTO>();
            foreach (var id in ids)
            {
                var dto = await _GetMapById(id, mapType);
                dtos.Add(dto);
            }
            return Ok(dtos);
        }
        #endregion

        #region Post 新建PackageMap信息
        /// <summary>
        /// 新建PackageMap信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PackageMapIdentityQueryDTO), 200)]
        public async Task<IActionResult> Post([FromBody] PackageMapCreateCommand command)
        {
            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 更新PackageMap信息
        /// <summary>
        /// 更新PackageMap信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<PackageMapPatchComand> patchDoc)
        {
            await _mediator.Send(new PackageMapPatchComand(id, patchDoc));
            return NoContent();
        }
        #endregion
    }
}