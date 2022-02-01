
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.AggregateModels;
using Order.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Order.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services)
        {
           
            services.AddScoped<IOrderRepository, OrderRepository>();
           // services.AddScoped<IEventHandler, EventHandler>();
            return services;
        }
    }
    
}
