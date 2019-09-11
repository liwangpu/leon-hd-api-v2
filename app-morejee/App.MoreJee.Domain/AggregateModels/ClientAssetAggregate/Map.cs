using App.MoreJee.Domain.SeedWork;
using System;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public class Map : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Icon { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }


        #region ctor
        protected Map()
        {

        }

        public Map(string name, string icon,  string organizationId, string creator)
            : this()
        {
            Name = name;
            Icon = icon;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
        }
        #endregion

        public void UpdateBasicInfo(string name, string modifier)
        {
            Name = name;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }

        public void DeleteClientAsset()
        {
            //AddDomainEvent(new ClientAssetDeleteEvent(Id));
        }

    }
}
