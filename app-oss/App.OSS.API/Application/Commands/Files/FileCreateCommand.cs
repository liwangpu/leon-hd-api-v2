#pragma warning disable    1591
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace App.OSS.API.Application.Commands.Files
{
    public class FileCreateCommand : IRequest<string>
    {
        /// <summary>
        /// MD5
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt { get; set; }
        /// <summary>
        /// 文件状态
        /// </summary>
        public int FileState { get; set; }
        public long Size { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #region ctor
        private FileCreateCommand() { }

        public FileCreateCommand(IHeaderDictionary header)
        {
            Microsoft.Extensions.Primitives.StringValues headerVar;
            header.TryGetValue("fileState", out headerVar); if (headerVar.Count > 0) FileState = Convert.ToInt32(headerVar[0].Trim());
            header.TryGetValue("fileName", out headerVar); if (headerVar.Count > 0) Name = headerVar[0].Trim();
            header.TryGetValue("fileExt", out headerVar); if (headerVar.Count > 0) FileExt = headerVar[0].Trim();
            header.TryGetValue("description", out headerVar); if (headerVar.Count > 0) Description = headerVar[0].Trim();


            //几个头信息decode
            if (!string.IsNullOrWhiteSpace(Name))
                Name = System.Web.HttpUtility.UrlDecode(Name);
            if (!string.IsNullOrWhiteSpace(FileExt))
                FileExt = System.Web.HttpUtility.UrlDecode(FileExt);
            if (!string.IsNullOrWhiteSpace(Description))
                Description = System.Web.HttpUtility.UrlDecode(Description);

            //确保扩展名不含.且已经是小写状态(方便进行equal查询)
            if (!string.IsNullOrWhiteSpace(FileExt))
                FileExt = FileExt.Replace(".", string.Empty).ToLower();
        }
        #endregion

    }
}
