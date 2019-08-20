using MediatR;
using Newtonsoft.Json;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupAddOrganCommand : IRequest
    {
        [JsonIgnore]
        public string ProductPermissionGroupId { get; set; }
        /// <summary>
        /// 组织Ids,逗号分隔
        /// </summary>
        public string OrganizationIds { get; set; }
    }
}
