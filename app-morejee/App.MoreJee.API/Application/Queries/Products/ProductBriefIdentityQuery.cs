using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductBriefIdentityQuery : IRequest<ProductBriefIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public ProductBriefIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class ProductBriefIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }

        public static ProductBriefIdentityQueryDTO From(Product data)
        {
            return new ProductBriefIdentityQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Icon = data.Icon,
                Description = data.Description,
                CategoryId = data.CategoryId,
                Brand = data.Brand,
                Unit = data.Unit
            };
        }
    }
}
