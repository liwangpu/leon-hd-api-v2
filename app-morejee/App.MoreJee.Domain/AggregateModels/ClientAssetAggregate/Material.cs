using App.Base.Domain.Common;
using App.Base.Domain.Extentions;
using System;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public class Material : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string CategoryId { get; protected set; }
        public string Icon { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; set; }


        #region ctor
        protected Material()
        {

        }

        public Material(string name, string icon, string categoryId, string organizationId, string creator)
            : this()
        {
            Id = GuidGen.NewGUID();
            Name = name;
            Icon = icon;
            CategoryId = categoryId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            OrganizationId = organizationId;
        }
        #endregion


        public void UpdateBasicInfo(string name, string description, string icon, string modifier)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }

        public void UpdateCategory(string categoryId)
        {
            CategoryId = categoryId;
        }

        public void DeleteClientAsset()
        {
            //AddDomainEvent(new ClientAssetDeleteEvent(Id));
        }

    }
}
