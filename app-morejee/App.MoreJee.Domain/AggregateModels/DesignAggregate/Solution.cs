using App.MoreJee.Domain.SeedWork;
using System;

namespace App.MoreJee.Domain.AggregateModels.DesignAggregate
{
    public class Solution : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Icon { get; protected set; }
        public string Description { get; protected set; }
        //public string Properties { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }

        #region ctor
        protected Solution()
        {

        }

        public Solution(string name, string description, string icon, string organizationId, string creator)
            : this()
        {
            Name = name;
            Icon = icon;
            Description = description;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
        }
        #endregion


        public void UpdateBasicInfo(string name, string description, string icon, string modifier)
        {
            Name = name;
            Icon = icon;
            Description = description;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }
    }
}
