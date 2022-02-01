
using MediatRAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Events
{
    public class OrderCreatedEvent : INotificationRequest
    {
        public AggregateModels.Order Order { get; }

        public OrderCreatedEvent(AggregateModels.Order order)
        {
            Order = order;
        }
    }
}
