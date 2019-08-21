using App.Base.API.Infrastructure.Services;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Infrastructure.Repositories;
using Autofac;

namespace App.MoreJee.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();
            builder.RegisterType<UriService>().As<IUriService>().InstancePerLifetimeScope();
            builder.RegisterType<MapRepository>().As<IMapRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MaterialRepository>().As<IMaterialRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaticMeshRepository>().As<IStaticMeshRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TextureRepository>().As<ITextureRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductSpecRepository>().As<IProductSpecRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PackageMapRepository>().As<IPackageMapRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductPermissionGroupRepository>().As<IProductPermissionGroupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ClientAssetPermissionControlService>().As<IClientAssetPermissionControlService>().InstancePerLifetimeScope();
            builder.RegisterType<SolutionRepository>().As<ISolutionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductPermissionRepository>().As<IProductPermissionRepository>().InstancePerLifetimeScope();



        }
    }
}
