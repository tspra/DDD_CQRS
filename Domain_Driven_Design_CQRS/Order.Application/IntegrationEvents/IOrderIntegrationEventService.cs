using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.IntegrationEvents
{
        public interface IOrderIntegrationEventService
        {
            Task PublishEventsThroughEventBusAsync(Guid transactionId);
            Task AddAndSaveEventAsync(IntegrationEvent evt);
        }
}
