using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupIdentityQuery : IRequest<ProductPermissionGroupIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public ProductPermissionGroupIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class ProductPermissionGroupIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProductPermissionGroupIdentityQueryDTO From(ProductPermissionGroup data)
        {
            return new ProductPermissionGroupIdentityQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
        }
    }
}
