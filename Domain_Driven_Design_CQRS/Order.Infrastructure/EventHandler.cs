using MediatRAbstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure
{
    //public class EventHandler : IEventHandler
    //{
    //    private IServiceProvider serviceProvider;
    //    public EventHandler(IServiceProvider serviceProvider)
    //    {
    //        this.serviceProvider = serviceProvider;
    //    }

    //    public async Task HandleAsync<T>(T notiifcation) where T : INotificationRequest
    //    {
    //        using (var scope = serviceProvider.CreateScope())
    //        {
               
    //            var handler = (INotificationRequestHandler<T>)scope.ServiceProvider.GetService(typeof(INotificationRequestHandler<T>));

          
    //        if (handler != null)
    //              {
    //                 await handler.HandleAsync(notiifcation);
                    
    //              }

    //        }
    //    }
    //}
}
