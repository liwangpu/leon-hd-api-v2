namespace App.Basic.Infrastructure.EntityMaps
{
    public class RoleEntityTypeMap : IEntityTypeMap
    {
        public const string TableNameAlias = "role";
        public const string Id = "id";
        public const string Name = "name";
        public const string Description = "description";
        public const string PermissionPolicyTypeId = "permission_policy_type_id";
    }
}
