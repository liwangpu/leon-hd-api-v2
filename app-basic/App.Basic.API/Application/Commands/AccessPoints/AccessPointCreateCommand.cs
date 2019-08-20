using MediatR;

namespace App.Basic.API.Application.Commands.AccessPoints
{
    public class AccessPointCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string PointKey { get; set; }
        public string Description { get; set; }
    }
}
