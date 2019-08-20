using App.Base.API.Application.Queries;
using MediatR;

namespace App.OSS.API.Application.Queries.Files
{
    public class FilePagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<FilePagingQueryDTO>>
    {
    }

    public class FilePagingQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string FileExt { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 文件状态
        /// </summary>
        public int FileState { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; protected set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifier { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreatedTime { get; protected set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public long ModifiedTime { get; protected set; }
        /// <summary>
        /// 下载路径
        /// </summary>
        public string Url { get; set; }
    }
}
