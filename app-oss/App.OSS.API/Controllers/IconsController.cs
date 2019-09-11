using App.Base.API.Infrastructure.Exceptions;
using App.OSS.API.Infrastructure.Consts;
using App.OSS.API.Infrastructure.Extensions;
using App.OSS.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace App.OSS.API.Controllers
{
    /// <summary>
    /// 上传图标
    /// </summary>
    [Route("OSS/[controller]")]
    [ApiController]
    public class IconsController : ControllerBase
    {
        private string iconFolder;
        private string tmpFileFolder;

        #region ctor
        public IconsController(IHostingEnvironment env)
        {
            iconFolder = Path.Combine(env.WebRootPath, OSSConst.IconFolder);
            tmpFileFolder = Path.Combine(env.WebRootPath, OSSConst.TmpFolder);
        }
        #endregion

        #region UploadStreamFile UploadStreamFile 通过文件流方式上传文件(流文件放入请求body)
        /// <summary>
        /// UploadStreamFile 通过文件流方式上传文件(流文件放入请求body)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Stream")]
        public IActionResult UploadStreamFile()
        {
            string iconExt = "";
            Microsoft.Extensions.Primitives.StringValues headerVar;
            Request.Headers.TryGetValue("fileExt", out headerVar); if (headerVar.Count > 0) iconExt = headerVar[0].Trim();

            if (string.IsNullOrWhiteSpace(iconExt))
                throw new HttpBadRequestException("请在Headers上添加fileExt标识icon的扩展名");

            iconExt = iconExt.Replace(".", string.Empty);


            //先把文件保存到临时文件夹,计算md5
            var tmpPath = Path.Combine(tmpFileFolder, $"{Guid.NewGuid()}.{iconExt}");
            using (FileStream fs = System.IO.File.Create(tmpPath))
            {
                HttpContext.Request.Body.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }


            var md5 = MD5Generator.CalcFile(tmpPath);
            var fileName = $"{md5}.{iconExt}";
            var iconPath = Path.Combine(iconFolder, fileName);
            //如果文件不存在资源文件夹,拷贝存储
            if (!System.IO.File.Exists(iconPath))
                System.IO.File.Copy(tmpPath, iconPath);

            //为文件生成缩略图标
            ImageThumbnailCreator.SaveImageThumbnails(iconPath);

            //删除临时文件
            if (System.IO.File.Exists(tmpPath))
                System.IO.File.Delete(tmpPath);

            return Ok($"/{OSSConst.AppRouteArea}/{OSSConst.IconFolder}/{fileName}");
        }
        #endregion

        #region UploadFormFile 通过表单上传文件
        /// <summary>
        /// 通过表单上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Form")]
        public IActionResult UploadFormFile(IFormFile file)
        {
            if (file == null)
                throw new HttpBadRequestException("请将Content-Type设置application/x-www-form-urlencoded,然后在formData的file附上图标文件");

            string iconExt = "";
            Microsoft.Extensions.Primitives.StringValues headerVar;
            Request.Headers.TryGetValue("fileExt", out headerVar); if (headerVar.Count > 0) iconExt = headerVar[0].Trim();

            if (string.IsNullOrWhiteSpace(iconExt))
                throw new HttpBadRequestException("请在Headers上添加fileExt标识icon的扩展名");

            iconExt = iconExt.Replace(".", string.Empty);

            //先把文件保存到临时文件夹,计算md5
            var tmpPath = Path.Combine(tmpFileFolder, $"{Guid.NewGuid()}.{iconExt}");
            using (FileStream fs = System.IO.File.Create(tmpPath))
            {
                file.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }

            var md5 = MD5Generator.CalcFile(tmpPath);
            var fileName = $"{md5}.{iconExt}";
            var iconPath = Path.Combine(iconFolder, fileName);
            //如果文件不存在资源文件夹,拷贝存储
            if (!System.IO.File.Exists(iconPath))
                System.IO.File.Copy(tmpPath, iconPath);

            //为文件生成缩略图标
            ImageThumbnailCreator.SaveImageThumbnails(iconPath);

            //删除临时文件
            if (System.IO.File.Exists(tmpPath))
                System.IO.File.Delete(tmpPath);

            return Ok($"/{OSSConst.AppRouteArea}/{OSSConst.IconFolder}/{fileName}");
        }
        #endregion


    }
}