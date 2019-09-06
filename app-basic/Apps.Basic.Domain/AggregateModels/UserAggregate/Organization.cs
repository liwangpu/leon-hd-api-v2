using Apps.Basic.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.Basic.Domain.AggregateModels.UserAggregate
{
    public class Organization : Entity, IAggregateRoot
    {
        private readonly List<Account> _ownAccounts;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        /// <summary>
        /// 组织上的邮件只是组织法定负责人邮件的一个冗余字段
        /// 在创建组织时候会根据该邮箱创建组织法定负责人账号
        /// 往后应该根据法定负责人的邮箱自动更新
        /// </summary>
        public string Mail { get; protected set; }
        /// <summary>
        /// 电话字段同Mail
        /// </summary>
        public string Phone { get; protected set; }
        public int Active { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public int OrganizationTypeId { get; protected set; }
        public string OwnerId { get; protected set; }
        public IReadOnlyCollection<Account> OwnAccounts => _ownAccounts;
    }
}
