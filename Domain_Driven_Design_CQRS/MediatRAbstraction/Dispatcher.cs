using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediatRAbstraction
{
    public class Dispatcher : IDispatcher
    {
        private IServiceProvider serviceProvider;
        private IMediator mediator;
        public Dispatcher(IServiceProvider serviceProvider, IMediator mediatorInstance)
        {
            this.serviceProvider = serviceProvider;
            this.mediator = mediatorInstance;
        }

        public Task Publish(INotificationRequest request)
        {
            return mediator.Publish(request);
        }

        public System.Threading.Tasks.Task<T> Send<T>(IDispatcherRequest<T> request)
        {
            return mediator.Send(request);
        }
    }
}
