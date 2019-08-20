using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Textures
{
    public class TextureIdentityQuery : IRequest<TextureIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public TextureIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class TextureIdentityQueryDTO
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
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; protected set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Modifier { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreatedTime { get; protected set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long ModifiedTime { get; protected set; }

        public static TextureIdentityQueryDTO From(Texture texture)
        {
            return new TextureIdentityQueryDTO
            {
                Id = texture.Id,
                Name = texture.Name,
                Icon = texture.Icon,
                Creator = texture.Creator,
                Modifier = texture.Modifier,
                CreatedTime = texture.CreatedTime,
                ModifiedTime = texture.ModifiedTime
            };
        }
    }
}
