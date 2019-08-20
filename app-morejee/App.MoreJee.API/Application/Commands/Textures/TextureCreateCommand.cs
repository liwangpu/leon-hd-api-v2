using MediatR;

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TextureCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
