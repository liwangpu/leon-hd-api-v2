using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.CustomRoles
{
    public class CustomRoleIdentityQuery : IRequest<CustomRoleIdentityQueryDTO>
    {
        public string Id { get; set; }

        public CustomRoleIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class CustomRoleIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccessPointKeys { get; set; }

        public static CustomRoleIdentityQueryDTO From(CustomRole data)
        {
            return new CustomRoleIdentityQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                AccessPointKeys = data.AccessPointKeys
            };
        }
    }
}
