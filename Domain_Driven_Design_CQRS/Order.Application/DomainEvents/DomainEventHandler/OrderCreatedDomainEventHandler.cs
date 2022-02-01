using MediatRAbstraction;
using Order.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.DomainEvents.DomainEventHandler
{
    public class OrderCreatedDomainEventHandler : INotificationRequestHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
