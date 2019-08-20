using MediatR;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryDeleteCommand : IRequest
    {
        public string Id { get; protected set; }

        public CategoryDeleteCommand(string id)
        {
            Id = id;
        }
    }
}
