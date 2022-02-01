

using EventBus;
using EventBus.Abstractions;
using EventBusKafka;
using MediatR;
using MediatRAbstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Order.Application;
using Order.Application.Features;
using Order.Application.IntegrationEvents.Events;
using Order.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Order.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplicationServices();
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddCustomIntegrations(Configuration);
            services.AddEventBus();
            services.AddApplicationServices();
            services.AddInfrasturctureServices();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });
         

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ConfigureEventBus(app);
        }
        
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderPaymentSucceededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>>();
        }


    }
    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, EventBusKafka.EventBusKafka>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IKafkaPersistantConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusKafka.EventBusKafka>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                return new EventBusKafka.EventBusKafka(persistentConnection, logger, eventBusSubcriptionsManager);
            });
            return services;
        }
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IKafkaPersistantConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<KafkaPersistantConnection>>();
                var factory = new KafkaConnection()
                {
                    Host = configuration["HostName"],
                    Port = configuration["Port"],
                };

                if (!string.IsNullOrEmpty(configuration["Topic"]))
                {
                    factory.Topic = configuration["Topic"];
                }

                return new KafkaPersistantConnection(factory, logger);
            });


            return services;
        }
    }
}
