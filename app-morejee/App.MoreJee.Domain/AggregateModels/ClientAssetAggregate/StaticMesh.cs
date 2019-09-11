using App.MoreJee.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.MoreJee.Domain.AggregateModels.ClientAssetAggregate
{
    public class StaticMesh : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Icon { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string RelatedProductSpecIds { get; protected set; }


        #region ctor
        protected StaticMesh()
        {

        }

        public StaticMesh(string name, string icon, string organizationId, string creator)
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

        public void SignRelatedProductSpec(string productSpecId)
        {
            if (string.IsNullOrWhiteSpace(productSpecId))
                return;


            var list = string.IsNullOrWhiteSpace(RelatedProductSpecIds) ? new List<string>() : RelatedProductSpecIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!list.Any(x => x == productSpecId))
                list.Add(productSpecId);

            RelatedProductSpecIds = string.Join(",", list);
        }

        public void DeleteClientAsset()
        {
            //AddDomainEvent(new ClientAssetDeleteEvent(Id));
        }
    }
}
