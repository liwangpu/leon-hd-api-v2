using App.MoreJee.Domain.SeedWork;
using System;

namespace App.MoreJee.Domain.AggregateModels.CategoryAggregate
{
    public class Category : TreeEntity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Icon { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public DateTime CreatedTime { get; protected set; }
        public DateTime ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string Resource { get; protected set; }

        #region ctor
        protected Category()
        {
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = CreatedTime;
        }
        public Category(string name, string description, string nodeType, string resource, string organizationId, string creator, string parentId = null, string icon = null, int displayIndex = 0)
            : this()
        {
            Name = name;
            Description = description;
            Icon = icon;
            Resource = resource;
            NodeType = nodeType;
            ParentId = parentId;
            DisplayIndex = displayIndex;
            OrganizationId = organizationId;
            Creator = creator;
            Modifier = Creator;
            //AddDomainEvent(new CategoryCreatedEvent(Id, Name, Resource, Description, Icon));
        }
        #endregion

        public void UpdateBasicInfo(string name, string description, string icon, string modifier)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow;
        }

        public void SetResource(string resource)
        {
            Resource = resource;
        }

        public void SetDisplayIndex(int index)
        {
            DisplayIndex = index;
        }
    }
}
