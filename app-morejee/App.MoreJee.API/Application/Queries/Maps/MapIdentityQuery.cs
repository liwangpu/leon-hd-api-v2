using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Maps
{
    public class MapIdentityQuery : IRequest<MapIdentityQueryDTO>
    {
        public string Id { get; protected set; }

        public MapIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class MapIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }

        public static MapIdentityQueryDTO From(Map map)
        {
            return new MapIdentityQueryDTO
            {
                Id = map.Id,
                Name = map.Name,
                Icon = map.Icon,
                Creator = map.Creator,
                Modifier = map.Modifier,
                CreatedTime = map.CreatedTime,
                ModifiedTime = map.ModifiedTime,
            };
        }
    }
}
