using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Repositories;
using Autofac;

namespace App.Basic.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomRoleRepository>().As<ICustomRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();
            builder.RegisterType<UriService>().As<IUriService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserManagedOrganizationService>().As<IUserManagedOrganizationService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserManagedAccountService>().As<IUserManagedAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<AccessPointRepository>().As<IAccessPointRepository>().InstancePerLifetimeScope();
        }
    }
}
