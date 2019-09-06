using Apps.Basic.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.Basic.Domain.AggregateModels.UserAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Name
        {
            get
            {
                return $"{LastName} ${FirstName}";
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


        protected Account()
        {

        }




    }
}
