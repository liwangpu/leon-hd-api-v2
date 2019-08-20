using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    /// <summary>
    /// 组织设立法定负责人事件
    /// </summary>
    public class OrganizationOwnerSettingEvent : INotification
    {
        public string OrganizationId { get; }
        public int OrganizationTypeId { get; }
        public string AccountId { get; }

        public OrganizationOwnerSettingEvent(string organizationId, int organizationTypeId, string accountId)
        {
            OrganizationId = organizationId;
            OrganizationTypeId = organizationTypeId;
            AccountId = accountId;
        }
    }
}
