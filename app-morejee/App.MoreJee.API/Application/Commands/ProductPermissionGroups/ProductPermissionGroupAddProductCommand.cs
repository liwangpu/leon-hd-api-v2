using MediatR;
using Newtonsoft.Json;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupAddProductCommand : IRequest
    {
        [JsonIgnore]
        public string ProductPermissionGroupId { get; set; }
        /// <summary>
        /// 组织Ids,逗号分隔
        /// </summary>
        public string ProductIds { get; set; }

    }
}
