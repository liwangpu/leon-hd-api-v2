using MediatR;

namespace App.MoreJee.API.Application.Queries.ProductSpecs
{
    public class ProductSpecBriefIdentityQuery : IRequest<ProductSpecBriefIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public ProductSpecBriefIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class ProductSpecBriefIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
    }
}
