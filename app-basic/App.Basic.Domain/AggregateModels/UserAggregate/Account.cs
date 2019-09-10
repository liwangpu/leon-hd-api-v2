using App.Basic.Domain.Consts;
using App.Basic.Domain.Events.UserEvents;
using App.Basic.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Account : Entity, IAggregateRoot
    {
        private readonly List<UserRole> _ownRoles;
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Name
        {
            get
            {
                return LastName + FirstName;
            }
        }
        public string Description { get; protected set; }
        public string Password { get; protected set; }
        public string Mail { get; protected set; }
        public string Phone { get; protected set; }
        public int Active { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        /// <summary>
        /// 法人标记
        /// </summary>
        public int LegalPerson { get; protected set; }
        public int LanguageTypeId { get; protected set; }
        public string OrganizationId { get; protected set; }
        public Organization Organization { get; protected set; }
        public int SystemRoleId { get; protected set; }
        public IReadOnlyCollection<UserRole> OwnRoles => _ownRoles;

        #region ctor
        protected Account()
        {
            _ownRoles = new List<UserRole>();
        }

        public Account(string firstName, string lastName, string password, string mail, string phone, int systemRoleId, string organizationId, string creator)
            : this()
        {
            Active = EntityStateConst.Active;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Mail = mail;
            Phone = phone;
            SystemRoleId = systemRoleId;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            AddDomainEvent(new AccountCreatedEvent(this));
        }
        #endregion

        #region UpdateBasicInfo 修改用户基础信息

        public void UpdateBasicInfo(string firstName, string lastName, string description, string mail, string phone, string modifier)
        {
            //当前用户是组织法定负责人,当他/她的邮箱或电话改变的时候
            //需要关联组织的修改联系信息
            //不过当AccountUpdateContactInfoEvent触发时候,需要判断当前用户是否是组织法定负责人
            var mailChange = Mail != mail;
            var phoneChange = Phone != phone;
            if (mailChange || phoneChange)
                AddDomainEvent(new AccountUpdateContactInfoEvent(Id, OrganizationId, mail, phone));

            FirstName = firstName;
            LastName = lastName;
            Description = description;
            Mail = mail;
            Phone = phone;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }
        #endregion


        //public UserRole AddRole(int roleId, bool isDefaultRole = false)
        //{
        //    if (Organization == null)
        //        throw new Exception("使用AddRole时Organization不能为空");

        //    var roleType = Enumeration.FromValue<Role>(roleId);

        //    var bRoleDuplicated = OwnRoles.Any(x => x.RoleId == roleId);
        //    if (bRoleDuplicated)
        //        throw new UserRoleDuplicationException("角色重复分配");

        //    var role = new UserRole(roleId, isDefaultRole);
        //    _ownRoles.Add(role);
        //    return role;
        //}

        public void UpdateCustomRoles(IEnumerable<string> customRoleIds)
        {
            _ownRoles.Clear();

            foreach (var cusId in customRoleIds)
            {
                var ur = new UserRole(Id, cusId);
                _ownRoles.Add(ur);
            }
        }

        /// <summary>
        /// 将该用户标记为法人信息
        /// 注意,该动作只是为用户标记一个法人标记,为查询提供方便
        /// </summary>
        public void SignLegalPerson()
        {
            LegalPerson = EntityStateConst.Yes;
        }

        /// <summary>
        /// 修改用户语言
        /// </summary>
        /// <param name="language"></param>
        public void CustomizeLannguage(LanguageType language)
        {
            LanguageTypeId = language.Id;
            AddDomainEvent(new AccountChangeLanguageEvent(this));
        }

        public void ChangePassword(string password, string operatorId)
        {
            Password = password;
            Modifier = operatorId;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }

        #region Delete 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="operatorId"></param>
        public void Delete(string operatorId)
        {
            Active = EntityStateConst.InActive;
            Modifier = operatorId;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }
        #endregion

    }
}
