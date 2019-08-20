using MediatR;
using System;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationIdentityQuery : IRequest<OrganizationIdentityQueryDTO>
    {
        public string Id { get; set; }
    }

    public class OrganizationIdentityQueryDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string OwnerId { get; set; }
        /// <summary>
        /// 组织类型Id
        /// </summary>
        public int OrganizationTypeId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long ModifiedTime { get; set; }
    }
}
