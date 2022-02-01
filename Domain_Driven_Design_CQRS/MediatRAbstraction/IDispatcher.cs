using System;
using System.Threading.Tasks;

namespace MediatRAbstraction
{
    public interface IDispatcher
    {
        Task<T> Send<T>(IDispatcherRequest<T> request);
        Task Publish(INotificationRequest request);
    }
}
