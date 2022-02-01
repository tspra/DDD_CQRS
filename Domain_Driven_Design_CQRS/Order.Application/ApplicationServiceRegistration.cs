
using FluentValidation;
using MediatR;
using MediatRAbstraction;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.DomainEvents.DomainEventHandler;
using Order.Application.Features;
using Order.Application.Features.Commands;
using Order.Application.IntegrationEvents;
using Order.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Order.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSpecficiTypes(typeof(INotificationRequestHandler<>));
            services.AddScoped<IOrderIntegrationEventService, OrderIntegrationEventService>();
            return services;
        }
        private static void AddSpecficiTypes(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(ApplicationServiceRegistration).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)

            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }

        }
        
    }
    
}
