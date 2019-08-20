using MediatR;

namespace App.Basic.API.Application.Commands.CustomRoles
{
    public class CustomRoleCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
