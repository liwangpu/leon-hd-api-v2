using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Textures
{
    public class TexturePagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<TexturePagingQueryDTO>>
    {

    }

    public class TexturePagingQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreatedTime { get; protected set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long ModifiedTime { get; protected set; }

        public static TexturePagingQueryDTO From(string id, string name, long createdTime, long modifiedTime)
        {
            return new TexturePagingQueryDTO
            {
                Id=id,
                Name= name,
                CreatedTime = createdTime,
                ModifiedTime = modifiedTime
            };
        }
    }
}
