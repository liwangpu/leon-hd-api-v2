using MediatR;

namespace App.MoreJee.API.Application.Commands.ProductSpecs
{
    public class ProductSpecCreateCommand : IRequest<string>
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PartnerPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string IconAssetId { get; set; }
    }
}
