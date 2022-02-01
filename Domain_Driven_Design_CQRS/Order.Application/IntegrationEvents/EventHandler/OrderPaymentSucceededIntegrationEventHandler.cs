using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Order.Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.IntegrationEvents.EventHandler
{
    public class OrderPaymentSucceededIntegrationEventHandler :
        IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentSucceededIntegrationEventHandler> logger;
        public OrderPaymentSucceededIntegrationEventHandler(
            ILogger<OrderPaymentSucceededIntegrationEventHandler> integrationLogger)
        {
            logger = integrationLogger ?? throw new ArgumentNullException(nameof(integrationLogger));
        }
        public Task Handle(OrderPaymentSucceededIntegrationEvent orderPaymentSucceededIntegrationEvent)
        {
            throw new NotImplementedException();
        }
    }
}
