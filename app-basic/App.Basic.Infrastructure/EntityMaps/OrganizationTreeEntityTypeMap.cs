namespace App.Basic.Infrastructure.EntityMaps
{
    public class OrganizationTreeEntityTypeMap : IEntityTypeMap
    {
        public const string TableNameAlias = "organization_tree";
        public const string Id = "id";
        public const string Name = "name";
        public const string LValue = "l_value";
        public const string RValue = "r_value";
        public const string ParentId = "parent_id";
        public const string NodeType = "node_type";
        public const string ObjId = "obj_id";
        public const string Group = "group";
    }
}
