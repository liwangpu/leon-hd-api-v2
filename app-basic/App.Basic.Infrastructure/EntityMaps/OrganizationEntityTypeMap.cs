namespace App.Basic.Infrastructure.EntityMaps
{
    public class OrganizationEntityTypeMap : IEntityTypeMap
    {
        public const string TableNameAlias = "organization";
        public const string Id = "id";
        public const string Name = "name";
        public const string Description = "description";
        public const string Mail = "mail";
        public const string Phone = "phone";
        public const string Active = "active";
        public const string Creator = "creator";
        public const string Modifier = "modifier";
        public const string CreatedTime = "created_time";
        public const string ModifiedTime = "modified_time";
        public const string ParentId = "parent_id";
        public const string OrganizationTypeId = "organization_type_id";
        public const string OwnerId = "owner_id";
    }
}
