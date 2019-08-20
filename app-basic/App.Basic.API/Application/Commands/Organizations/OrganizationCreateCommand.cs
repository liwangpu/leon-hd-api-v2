using MediatR;

namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationCreateCommand : IRequest<string>
    {
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
        /// 组织类型
        /// </summary>
        public int OrganizationTypeId { get; set; }
    }

}
