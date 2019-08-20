using App.Base.API.Application.Queries;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class AccessPointPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<AccessPointPagingQueryDTO>>
    {

    }

    public class AccessPointPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PointKey { get; set; }
        public int IsInner { get; set; }
        public string IsInnerName { get; set; }

        public static AccessPointPagingQueryDTO From(AccessPoint acc)
        {
            return new AccessPointPagingQueryDTO
            {
                Id = acc.Id,
                Name = acc.Name,
                Description = acc.Description,
                PointKey = acc.PointKey,
                IsInner = acc.IsInner
            };
        }
    }
}
