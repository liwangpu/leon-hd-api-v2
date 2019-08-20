using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Basic.API.Application.Commands.UserRoles
{
    public class UserRoleBatchDeleteCommand : IRequest<ObjectResult>
    {
        public UserRoleBatchDeleteCommand(string ids)
        {
            UserRoleIds = ids;
        }

        /// <summary>
        /// 逗号分隔的UserRoleId
        /// </summary>
        public string UserRoleIds { get; set; }

        /// <summary>
        /// 校验ids,将中文逗号(如果有)替换为英文逗号
        /// </summary>
        public void CheckIds()
        {
            if (!string.IsNullOrWhiteSpace(UserRoleIds))
            {
                var idsStr = UserRoleIds.Replace('，', ',');
            }
        }
    }
}
