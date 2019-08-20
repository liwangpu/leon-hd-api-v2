using App.Base.API.Application.Behaviors;
using App.MoreJee.API.Application.Commands.Textures;
using App.MoreJee.API.Application.DomainEventHandlers.Products;
using App.MoreJee.API.Application.Validations.Products;
using Autofac;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace App.MoreJee.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(TextureCreateCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(ProductSpecValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(CreateDefaultProductSpecHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            //builder.RegisterGeneric(typeof(AccountCreateLoging<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(AccountQueryLoging<, >)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
