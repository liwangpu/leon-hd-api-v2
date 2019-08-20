using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using System.Collections.Generic;

namespace App.Basic.API.Application.Queries.UserRoles
{
    public class UserRoleQuery : IRequest<List<UserRolePagingQueryDTO>>
    {
        public string AccountId { get; set; }
    }

    public class UserRolePagingQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string CustomRoleId { get; set; }

        public static UserRolePagingQueryDTO From(UserRole userRole)
        {
            return new UserRolePagingQueryDTO
            {
                Id = userRole.Id,
                AccountId = userRole.AccountId,
                CustomRoleId = userRole.CustomRoleId
            };
        }
    }
}
