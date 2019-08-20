using MediatR;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
