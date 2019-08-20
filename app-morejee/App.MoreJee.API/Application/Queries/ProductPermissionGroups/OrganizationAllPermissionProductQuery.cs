using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class OrganizationAllPermissionProductQuery : IRequest<List<OrganizationAllPermissionProductDTO>>
    {
        [JsonIgnore]
        public string OrganizationId { get; set; }

        public string Search { get; set; }
    }
}
