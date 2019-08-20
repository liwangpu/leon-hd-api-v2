using App.Base.API.Application.Queries;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<ProductPermissionGroupPagingQueryDTO>>
    {
    }

    public class ProductPermissionGroupPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProductPermissionGroupPagingQueryDTO From(ProductPermissionGroup data)
        {
            return new ProductPermissionGroupPagingQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
        }
    }
}
