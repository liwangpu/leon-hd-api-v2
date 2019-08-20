using App.Base.API.Application.Queries;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.CustomRoles
{
    public class CustomRolePagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<CustomRolePagingQueryDTO>>
    {

    }

    public class CustomRolePagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static CustomRolePagingQueryDTO From(CustomRole data)
        {
            return new CustomRolePagingQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
        }
    }
}
