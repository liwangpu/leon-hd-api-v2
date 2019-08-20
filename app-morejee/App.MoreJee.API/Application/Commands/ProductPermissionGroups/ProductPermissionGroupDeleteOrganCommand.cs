using MediatR;
using Newtonsoft.Json;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupDeleteOrganCommand : IRequest
    {
        [JsonIgnore]
        public string ProductPermissionGroupId { get; set; }
        public string Ids { get; set; }
    }
}
