using MediatR;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
