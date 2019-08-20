using App.Base.API.Application.Behaviors;
using App.Basic.API.Application.Commands.Organizations;
using App.Basic.API.Application.DomainEventHandlers.Users;
using App.Basic.API.Application.Validations.Users;
using Autofac;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Reflection;

namespace App.Basic.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(OrganizationCreateCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(OrganizationCreateValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(CreateOrganizationOwnerAccount).GetTypeInfo().Assembly)
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
