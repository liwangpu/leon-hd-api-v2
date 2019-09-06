using Apps.OSS.Domain.AggregateModels.FileAssetAggregate;
using Apps.OSS.Infrastructure.Repositories;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.OSS.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileAssetRepository>().As<IFileAssetRepository>().InstancePerLifetimeScope();
        }
    }
}
