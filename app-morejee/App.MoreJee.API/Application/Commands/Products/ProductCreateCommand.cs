using MediatR;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
    }
}
