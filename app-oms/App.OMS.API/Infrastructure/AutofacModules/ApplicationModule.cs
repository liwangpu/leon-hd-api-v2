#pragma warning disable 1591
using App.Base.API.Infrastructure.Services;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using App.OMS.Infrastructure.Repositories;
using Autofac;

namespace App.OMS.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();
            builder.RegisterType<UriService>().As<IUriService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<StaticMeshRepository>().As<IStaticMeshRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<TextureRepository>().As<ITextureRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductSpecRepository>().As<IProductSpecRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<PackageMapRepository>().As<IPackageMapRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductPermissionGroupRepository>().As<IProductPermissionGroupRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<ClientAssetPermissionControlService>().As<IClientAssetPermissionControlService>().InstancePerLifetimeScope();


        }
    }
}
