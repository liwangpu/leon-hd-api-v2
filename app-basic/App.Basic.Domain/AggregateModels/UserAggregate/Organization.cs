using App.Basic.Domain.Consts;
using App.Basic.Domain.Events.UserEvents;
using App.Basic.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    /// <summary>
    /// 组织
    /// </summary>
    public class Organization : TreeEntity, IAggregateRoot
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

        #region ctor
        protected Organization()
        {
            _ownAccounts = new List<Account>();
        }

        public Organization(OrganizationType organType, string name, string description, string mail, string phone, string creator, string parentId = null)
            : this()
        {
            Active = EntityStateConst.Active;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Description = description;
            Mail = mail;
            Phone = phone;
            Creator = creator;
            Modifier = Creator;
            ParentId = parentId;
            OrganizationTypeId = organType.Id;
            AddDomainEvent(new OrganizationCreatedEvent(this));
        }
        #endregion

        /******** methods ********/

        #region UpdateBasicInfo 更新组织基础信息
        /// <summary>
        /// 更新组织基础信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="modifier"></param>
        public void UpdateBasicInfo(string name, string description, string modifier)
        {
            Name = name;
            Description = description;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            AddDomainEvent(new OrganizationUpdatedEvent(this));
        }
        #endregion

        #region UpdateContactInfo 更新组织联系信息
        /// <summary>
        /// 更新组织联系信息
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="phone"></param>
        public void UpdateContactInfo(string mail, string phone)
        {
            Mail = mail;
            Phone = phone;
        } 
        #endregion

        #region Delete 删除组织
        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="operatorId"></param>
        public void Delete(string operatorId)
        {
            Active = EntityStateConst.InActive;
            Modifier = operatorId;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }
        #endregion

        #region SetOwner 设置法定负责人
        /// <summary>
        /// 设置法定负责人
        /// </summary>
        /// <param name="accountId"></param>
        public void SetOwner(string accountId)
        {
            OwnerId = accountId;
            AddDomainEvent(new OrganizationOwnerSettingEvent(Id, OrganizationTypeId, accountId));
        }
        #endregion

    }
}
