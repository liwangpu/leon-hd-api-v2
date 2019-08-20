using App.Base.Domain.Common;
using App.Base.Domain.Consts;
using System.Collections.Generic;

namespace App.Basic.Domain.AggregateModels.PermissionAggregate
{
    public class SystemRole : Enumeration
    {
        public static SystemRole ApplicationManager = new ApplicationManagerRole();
        public static SystemRole ApplicationService = new ApplicationServiceRole();
        public static SystemRole BrandOrganizationAdmin = new BrandOrganizationAdminRole();
        public static SystemRole PartnerOrganizationAdmin = new PartnerOrganizationAdminRole();
        public static SystemRole SupplierOrganizationAdmin = new SupplierOrganizationAdminRole();

        public static SystemRole NormalUser = new NormalUserRole();
        public string AccessPointKeys { get; set; }
        #region ctor
        public SystemRole(int id, string name, string description, string accessPointKeys)
        : base(id, name, description)
        {
            AccessPointKeys = accessPointKeys;
        }
        #endregion


        /// <summary>
        /// 系统超级管理员
        /// 应用级别的角色信息,管理整个应用的所有数据
        /// </summary>
        private class ApplicationManagerRole : SystemRole
        {
            public ApplicationManagerRole()
                      : base(1, "Role.ApplicationManager", "Role.ApplicationManager.Description", "")
            {
            }
        }

        /// <summary>
        /// 系统客服
        /// 应用级别的角色信息,管理整个应用的所有数据
        /// </summary>
        private class ApplicationServiceRole : SystemRole
        {
            public ApplicationServiceRole()
                      : base(2, "Role.ApplicationService", "Role.ApplicationService.Description", "")
            {
            }
        }

        /// <summary>
        /// 组织超级管理员
        /// 组织级别的角色信息,管理整个组织的数据信息
        /// </summary>
        private class BrandOrganizationAdminRole : SystemRole
        {
            public BrandOrganizationAdminRole()
                      : base(3, "Role.BrandOrganizationAdmin", "Role.BrandOrganizationAdmin.Description", "")
            {
                var keys = new List<string>();
                //添加零售价权限
                keys.Add(AccessPointInnerPointKeyConst.PriceRetrieve);
                keys.Add(AccessPointInnerPointKeyConst.PartnerPriceRetrieve);
                keys.Add(AccessPointInnerPointKeyConst.PurchasePriceRetrieve);
                keys.Add(AccessPointInnerPointKeyConst.PriceEdit);
                keys.Add(AccessPointInnerPointKeyConst.PartnerPriceEdit);
                keys.Add(AccessPointInnerPointKeyConst.PurchasePriceEdit);
                keys.Add(AccessPointInnerPointKeyConst.ProductBasicInfoManagement);
                keys.Add(AccessPointInnerPointKeyConst.ClientAssetManagement);
                AccessPointKeys = string.Join(",", keys);
            }
        }

        private class PartnerOrganizationAdminRole : SystemRole
        {
            public PartnerOrganizationAdminRole()
                      : base(4, "Role.PartnerOrganizationAdmin", "Role.PartnerOrganizationAdmin.Description", "")
            {
                var keys = new List<string>();
                keys.Add(AccessPointInnerPointKeyConst.PartnerPriceRetrieve);
                AccessPointKeys = string.Join(",", keys);
            }
        }

        private class SupplierOrganizationAdminRole : SystemRole
        {
            public SupplierOrganizationAdminRole()
                      : base(5, "Role.SupplierOrganizationAdmin", "Role.SupplierOrganizationAdmin.Description", "")
            {
                var keys = new List<string>();
                keys.Add(AccessPointInnerPointKeyConst.PurchasePriceRetrieve);
                AccessPointKeys = string.Join(",", keys);
            }
        }

        private class NormalUserRole : SystemRole
        {
            public NormalUserRole()
                 : base(100, "Role.NormalUser", "", "")
            {

            }
        }

    }
}
