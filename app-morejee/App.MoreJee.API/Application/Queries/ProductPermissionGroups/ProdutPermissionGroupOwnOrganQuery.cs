using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProdutPermissionGroupOwnOrganQuery : IRequest<List<ProdutPermissionGroupOwnOrganQueryDTO>>
    {
        [JsonIgnore]
        public string ProductPermissionGroupId { get; set; }
        public string Search { get; set; }
    }

    public class ProdutPermissionGroupOwnOrganQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProdutPermissionGroupOwnOrganQueryDTO From(string id, string name, string description)
        {
            return new ProdutPermissionGroupOwnOrganQueryDTO
            {
                Id = id,
                Name = name,
                Description = description
            };
        }
    }
}
