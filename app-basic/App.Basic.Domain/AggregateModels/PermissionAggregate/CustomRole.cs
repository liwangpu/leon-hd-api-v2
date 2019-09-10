using App.Basic.Domain.SeedWork;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public class CustomRole : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string AccessPointKeys { get; protected set; }

        #region ctor
        protected CustomRole()
        {

        }

        public CustomRole(string name, string description, string organizationId)
            : this()
        {
            Name = name;
            Description = description;
            OrganizationId = organizationId;
        }
        #endregion


        public void UpdateBasicInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void UpdateAccessPoint(string keys)
        {
            AccessPointKeys = keys;
        }
    }
}
