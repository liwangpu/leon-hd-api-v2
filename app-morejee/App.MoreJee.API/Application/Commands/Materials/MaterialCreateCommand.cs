using MediatR;

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Icon { get; set; }
    }
}
