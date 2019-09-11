using App.MoreJee.Domain.SeedWork;
using System;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public class Texture : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Icon { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; set; }


        #region ctor
        protected Texture()
        {

        }

        public Texture(string name, string icon, string organizationId, string creator)
            : this()
        {
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            Name = name;
            Icon = icon;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
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
