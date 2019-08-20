using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class AccessPointIdentityQuery : IRequest<AccessPointIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public AccessPointIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class AccessPointIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PointKey { get; set; }

        public static AccessPointIdentityQueryDTO From(AccessPoint acc)
        {
            return new AccessPointIdentityQueryDTO
            {
                Id = acc.Id,
                Name = acc.Name,
                Description = acc.Description,
                PointKey = acc.PointKey
            };
        }
    }
}
