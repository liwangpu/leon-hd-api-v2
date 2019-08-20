using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Maps
{
    public class MapPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<MapPagingQueryDTO>>
    {
    }

    public class MapPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public static MapPagingQueryDTO From(string id, string name, long createdTime, long modifiedTime)
        {
            return new MapPagingQueryDTO
            {
                Id = id,
                Name = name,
                CreatedTime = createdTime,
                ModifiedTime = modifiedTime
            };
        }
    }
}
