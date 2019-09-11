using App.Base.API.Infrastructure.Exceptions;
using App.OSS.API.Infrastructure.Consts;
using App.OSS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace App.OSS.API.Controllers
{
    [Route("OSS/[controller]")]
    [ApiController]
    public class SrcClientAssetsController : ControllerBase
    {
        private string srcClientAssetFolder;
        private string tmpFileFolder;

        #region ctor
        public SrcClientAssetsController(IHostingEnvironment env)
        {
            srcClientAssetFolder = Path.Combine(env.WebRootPath, OSSConst.SrcClientAssetFolder);
            tmpFileFolder = Path.Combine(env.WebRootPath, OSSConst.TmpFolder);
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
        public IActionResult CheckFileExist(string md5)
        {
            var fileName = $"{md5}.uasset";
            var filePath = Path.Combine(srcClientAssetFolder, fileName);
            if (System.IO.File.Exists(filePath))
                return Ok($"/{OSSConst.AppRouteArea}/{OSSConst.SrcClientAssetFolder}/{fileName}");
            return Ok(string.Empty);
        }
        #endregion

        #region Post UploadStreamFile UploadStreamFile 通过文件流方式上传文件(流文件放入请求body)
        /// <summary>
        /// UploadStreamFile 通过文件流方式上传文件(流文件放入请求body)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Stream")]
        public IActionResult UploadStreamFile()
        {
            string assetExt = "";
            Microsoft.Extensions.Primitives.StringValues headerVar;
            Request.Headers.TryGetValue("fileExt", out headerVar); if (headerVar.Count > 0) assetExt = headerVar[0].Trim();

            if (string.IsNullOrWhiteSpace(assetExt))
                throw new HttpBadRequestException("请在Headers上添加fileExt标识Asset的扩展名");

            assetExt = assetExt.Replace(".", string.Empty);


            //先把文件保存到临时文件夹,计算md5
            var tmpPath = Path.Combine(tmpFileFolder, $"{Guid.NewGuid()}.{assetExt}");
            using (FileStream fs = System.IO.File.Create(tmpPath))
            {
                HttpContext.Request.Body.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }

            var md5 = MD5Generator.CalcFile(tmpPath);
            var fileName = $"{md5}.{assetExt}";
            var iconPath = Path.Combine(srcClientAssetFolder, fileName);
            //如果文件不存在资源文件夹,拷贝存储
            if (!System.IO.File.Exists(iconPath))
                System.IO.File.Copy(tmpPath, iconPath);

            //删除临时文件
            if (System.IO.File.Exists(tmpPath))
                System.IO.File.Delete(tmpPath);

            return Ok($"/{OSSConst.AppRouteArea}/{OSSConst.SrcClientAssetFolder}/{fileName}");
        }
        #endregion

    }
}