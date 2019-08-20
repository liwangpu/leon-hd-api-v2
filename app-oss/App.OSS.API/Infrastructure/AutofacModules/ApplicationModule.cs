#pragma warning disable 1591
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using App.OSS.Infrastructure.Repositories;
using Autofac;
namespace App.OSS.API.Infrastructure.AutofacModules

{
    public class ApplicationModule : Autofac.Module
    {

        #region ctor
        public ApplicationModule()
        {

        }
        #endregion

        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c=>new FileAssetRepository(_mongoDbConnectionString)).As<IFileAssetRepository>().SingleInstance();
            builder.RegisterType<FileAssetRepository>().As<IFileAssetRepository>().InstancePerLifetimeScope();
        }
    }
}
