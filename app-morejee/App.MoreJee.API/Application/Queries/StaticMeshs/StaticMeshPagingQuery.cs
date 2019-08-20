using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.StaticMeshs
{
    public class StaticMeshPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<StaticMeshPagingQueryDTO>>
    {
    }

    public class StaticMeshPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public string HasRelatedProduct { get; set; }
        public static StaticMeshPagingQueryDTO From(string id, string name, string hasRelatedProduct, long createdTime, long modifiedTime)
        {
            return new StaticMeshPagingQueryDTO
            {
                Id = id,
                Name = name,
                HasRelatedProduct = hasRelatedProduct,
                CreatedTime = createdTime,
                ModifiedTime = modifiedTime
            };
        }
    }

}
