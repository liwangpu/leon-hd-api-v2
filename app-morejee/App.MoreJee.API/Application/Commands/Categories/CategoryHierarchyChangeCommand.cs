using MediatR;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryHierarchyChangeCommand : IRequest
    {
        public string ParentId { get; set; }
        public string CategoryId { get; set; }
    }
}
