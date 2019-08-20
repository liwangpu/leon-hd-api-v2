﻿using App.OSS.API.Application.Commands.Files;
using Autofac;
using MediatR;
using System.Reflection;

namespace App.OSS.API.Infrastructure.AutofacModules
{
    /// <summary>
    /// Mediator服务模块
    /// </summary>
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(FileCreateCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


            //// Register the Command's Validators (Validators based on FluentValidation library)
            //builder
            //    .RegisterAssemblyTypes(typeof(AccountCreateValidator).GetTypeInfo().Assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //    .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            //builder.RegisterGeneric(typeof(AccountCreateLoging<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(AccountQueryLoging<, >)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
