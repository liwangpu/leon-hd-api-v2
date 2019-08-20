using MediatR;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountProfileQuery : IRequest<AccountProfileQueryDTO>
    { }

    public class AccountProfileQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 组织Id
        /// </summary>
        public string OrganizationId { get; set; }
    }
}
