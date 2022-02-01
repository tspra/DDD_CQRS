using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.IntegrationEvents
{
    public class OrderIntegrationEventService : IOrderIntegrationEventService
    {
        private readonly IEventBus eventBus;
        private readonly ILogger<OrderIntegrationEventService> logger;
        public OrderIntegrationEventService(IEventBus bus, ILogger<OrderIntegrationEventService> orderLogger)
        {
          
            eventBus = bus ?? throw new ArgumentNullException(nameof(bus));
         
            logger = orderLogger ?? throw new ArgumentNullException(nameof(orderLogger));
        }
        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            // Save the events to databse if necessary, if do so, check the events published or not  before publishing
            //throw new NotImplementedException();

            await eventBus.PublishAsync(evt);
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingEvents = new List<IntegrationEvent>();
            foreach (var intEvent in pendingEvents)
            {
                logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", intEvent.Id, intEvent);

                try
                {
                    // 
                   await eventBus.PublishAsync(intEvent);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", intEvent.Id);

                }
            }
        }
    }
}
