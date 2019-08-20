using App.Base.Domain.Common;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public class OrganizationType : Enumeration
    {
        public static OrganizationType ServiceProvider = new ServiceProviderType();
        public static OrganizationType Brand = new BrandType();
        public static OrganizationType Partner = new PartnerType();
        public static OrganizationType Supplier = new SupplierType();

        public OrganizationType(int id, string name, string description)
          : base(id, name, description)
        {

        }

        private class ServiceProviderType : OrganizationType
        {
            public ServiceProviderType()
                : base(1, "OrganizationType.ServiceProvider", "OrganizationType.ServiceProvider.Description")
            {
            }
        }

        private class BrandType : OrganizationType
        {
            public BrandType()
             : base(2, "OrganizationType.Brand", "OrganizationType.Brand.Description")
            {
            }
        }

        private class PartnerType : OrganizationType
        {
            public PartnerType()
             : base(3, "OrganizationType.Partner", "OrganizationType.Partner.Description")
            {
            }
        }

        private class SupplierType : OrganizationType
        {
            public SupplierType()
             : base(4, "OrganizationType.Supplier", "OrganizationType.Supplier.Description")
            {
            }
        }
    }
}
