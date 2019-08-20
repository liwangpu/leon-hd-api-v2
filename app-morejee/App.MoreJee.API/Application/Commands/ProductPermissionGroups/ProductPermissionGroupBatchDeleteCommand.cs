using MediatR;
namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupBatchDeleteCommand : IRequest
    {
        public string Ids { get; set; }
    }
}
