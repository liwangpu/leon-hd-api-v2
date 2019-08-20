using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.API.Application.Commands.UserRoles
{
    public class UserRoleCreateCommand : IRequest<UserRoleCreateCommandDTO>
    {
        public string AccountId { get; set; }
        public int RoleId { get; set; }
    }

    public class UserRoleCreateCommandDTO
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
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescription { get; set; }
        /// <summary>
        /// 是否默认角色
        /// </summary>
        public bool IsDefault { get; set; }

        public void SetDetail(string roleName, string roleDescription)
        {
            RoleName = roleName;
            RoleDescription = roleDescription;
        }

        public static UserRoleCreateCommandDTO From(UserRole userRole)
        {
            return new UserRoleCreateCommandDTO
            {
                Id = userRole.Id,
                AccountId = userRole.AccountId,
                RoleId = userRole.RoleId,
                IsDefault = userRole.IsDefault == EntityStateConst.IsDefault
            };
        }
    }
}
