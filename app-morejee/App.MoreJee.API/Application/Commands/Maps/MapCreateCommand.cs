using MediatR;

namespace App.MoreJee.API.Application.Commands.Maps
{
    public class MapCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
