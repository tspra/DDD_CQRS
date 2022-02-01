using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRAbstraction
{
    public interface INotificationRequestHandler<TNotification> : INotificationHandler<TNotification> where TNotification : INotificationRequest
    {
    }
}
