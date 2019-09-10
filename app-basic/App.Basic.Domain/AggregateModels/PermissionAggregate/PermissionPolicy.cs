using App.Basic.Domain.SeedWork;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public class PermissionPolicy : Enumeration
    {
        public static PermissionPolicy ApplicationShare = new ApplicationSharePolicy();
        public static PermissionPolicy OrganizationShare = new OrganizationSharePolicy();
        public static PermissionPolicy DepartmentShare = new DepartmentSharePolicy();

        #region ctor
        public PermissionPolicy(int id, string name, string description)
        : base(id, name, description)
        {

        }
        #endregion


        private class ApplicationSharePolicy : PermissionPolicy
        {
            public ApplicationSharePolicy()
                 : base(1, "系统应用级别共享", "")
            {
            }
        }

        private class OrganizationSharePolicy : PermissionPolicy
        {
            public OrganizationSharePolicy()
                 : base(2, "组织级别共享", "整个组织共享")
            {
            }
        }

        private class DepartmentSharePolicy : PermissionPolicy
        {
            public DepartmentSharePolicy()
                 : base(3, "部门级别共享", "整个部门共享")
            {
            }
        }


    }
}
