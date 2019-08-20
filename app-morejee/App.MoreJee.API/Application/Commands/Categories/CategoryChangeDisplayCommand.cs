using MediatR;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryChangeDisplayCommand : IRequest
    {
        public string Id { get; protected set; }
        public bool MoveUp { get; protected set; }

        public CategoryChangeDisplayCommand(string id, bool moveUp = false)
        {
            Id = id;
            MoveUp = moveUp;
        }
    }
}
