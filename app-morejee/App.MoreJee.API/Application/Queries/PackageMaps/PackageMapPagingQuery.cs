using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.PackageMaps
{
    public class PackageMapPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<PackageMapPagingQueryDTO>>
    {
    }

    public class PackageMapPagingQueryDTO
    {
        public string Id { get; set; }
        public string PackageName { get; set; }
        public string ResourceId { get; set; }
        public string ResourceType { get; set; }

        public static PackageMapPagingQueryDTO From(string id, string pckName, string resourceId, string resourceType)
        {
            return new PackageMapPagingQueryDTO
            {
                Id = id,
                PackageName = pckName,
                ResourceId = resourceId,
                ResourceType = resourceType
            };
        }
    }
}
