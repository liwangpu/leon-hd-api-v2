using MediatR;

namespace App.Basic.API.Application.Queries.Roles
{
    public class RoleIdentityQuery : IRequest<RoleIdentityQueryDTO>
    {
        public int Id { get; set; }
    }

    public class RoleIdentityQueryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccessPointKeys { get; set; }
    }
}
