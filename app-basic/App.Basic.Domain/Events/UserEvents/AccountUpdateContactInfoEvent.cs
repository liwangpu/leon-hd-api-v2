using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    /// <summary>
    /// 用户修改联系信息
    /// 邮件和电话
    /// </summary>
    public class AccountUpdateContactInfoEvent : INotification
    {
        public string AccountId { get; }
        public string OranizationId { get; }
        public string Mail { get; }
        public string Phone { get; }

        public AccountUpdateContactInfoEvent(string accountId, string organizationId, string mail, string phone)
        {
            AccountId = accountId;
            OranizationId = organizationId;
            Mail = mail;
            Phone = phone;
        }
    }
}
