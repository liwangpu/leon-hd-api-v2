using App.Base.API.Application.Queries;
using App.Base.Domain.Common;
using App.OSS.API.Application.Commands.Files;
using App.OSS.API.Application.Queries.Files;
using App.OSS.API.Infrastructure.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App.OSS.API.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    //[Authorize]
    [Authorize]
    [Route("OSS/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private string clientAssetFolder;
        private string tmpFileFolder;

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="env"></param>
        public FilesController(IMediator mediator, IHostingEnvironment env)
        {
            _mediator = mediator;
            clientAssetFolder = Path.Combine(env.WebRootPath, OSSConst.ClientAssetFolder);

            tmpFileFolder = Path.Combine(env.WebRootPath, OSSConst.TmpFolder);

        }
        #endregion

        #region Get 获取文件列表信息列表
        /// <summary>
        /// 获取文件列表信息列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagingQueryResult<FilePagingQueryDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] FilePagingQuery query)
        {
            var list = await _mediator.Send(query);
            return Ok(list);
        }
        #endregion

        #region Get 获取文件信息
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FileIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _mediator.Send(new FileIdentityQuery() { Id = id });
            return Ok(dto);
        }
        #endregion

        #region UploadStreamFile 通过文件流方式上传文件(流文件放入请求body)
        /// <summary>
        /// 通过文件流方式上传文件(流文件放入请求body)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Stream")]
        [ProducesResponseType(typeof(FileIdentityQueryDTO), 200)]
        public async Task<IActionResult> UploadStreamFile()
        {
            var command = new FileCreateCommand(Request.Headers);
            //先把文件保存到临时文件夹,计算md5
            var tmpPath = Path.Combine(tmpFileFolder, $"{Guid.NewGuid()}.{command.FileExt}");
            using (FileStream fs = System.IO.File.Create(tmpPath))
            {
                HttpContext.Request.Body.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }

            command.MD5 = MD5Gen.CalcFile(tmpPath);

            var filePath = Path.Combine(clientAssetFolder, $"{command.MD5}.{command.FileExt}");

            //如果文件不存在资源文件夹,拷贝存储
            if (!System.IO.File.Exists(filePath))
                System.IO.File.Copy(tmpPath, filePath);

            //删除临时文件
            if (System.IO.File.Exists(tmpPath))
                System.IO.File.Delete(tmpPath);

            if (string.IsNullOrWhiteSpace(command.Name))
                command.Name = command.MD5;
            command.Size = new FileInfo(filePath).Length;

            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region UploadFormFile 通过表单形式上传文件
        /// <summary>
        /// 通过表单形式上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost("Form")]
        [ProducesResponseType(typeof(FileIdentityQueryDTO), 200)]
        public async Task<IActionResult> UploadFormFile(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            var command = new FileCreateCommand(Request.Headers);

            //先把文件保存到临时文件夹,计算md5
            var tmpPath = Path.Combine(tmpFileFolder, $"{Guid.NewGuid()}.{command.FileExt}");
            using (FileStream fs = System.IO.File.Create(tmpPath))
            {
                file.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }
            var finfo = new FileInfo(tmpPath);
            command.MD5 = MD5Gen.CalcFile(tmpPath);
            command.Size = finfo.Length;
            var filePath = Path.Combine(clientAssetFolder, $"{command.MD5}.{command.FileExt}");

            //如果文件不存在资源文件夹,拷贝存储
            if (!System.IO.File.Exists(filePath))
                System.IO.File.Copy(tmpPath, filePath);

            //删除临时文件
            if (System.IO.File.Exists(tmpPath))
                System.IO.File.Delete(tmpPath);

            if (string.IsNullOrWhiteSpace(command.Name))
                command.Name = command.MD5;
            command.Size = new FileInfo(filePath).Length;

            var id = await _mediator.Send(command);
            return await Get(id);
        }
        #endregion

        #region Patch 修改文件信息
        /// <summary>
        /// 修改文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<FilePatchCommand> patchDoc)
        {
            await _mediator.Send(new FilePatchCommand(id, patchDoc));
            return NoContent();
        }
        #endregion

        #region Get CheckFileExist 根据MD5校验文件是否存在
        /// <summary>
        /// 根据MD5校验文件是否存在
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        [HttpGet("MD5/{md5}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CheckFileExist(string md5)
        {
            var dto = await _mediator.Send(new FileExistCheckQuery(md5));
            return Ok(dto);
        }
        #endregion
    }
}