using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProductPermissionGroupOwnProductQuery : IRequest<List<ProductPermissionGroupOwnProductItemDTO>>
    {
        [JsonIgnore]
        public string ProductPermissionGroupId { get; set; }
        public string Search { get; set; }
        public string CategoryId { get; set; }
    }

}
