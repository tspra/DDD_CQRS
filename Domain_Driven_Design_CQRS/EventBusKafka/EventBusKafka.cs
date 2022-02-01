using Confluent.Kafka;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using EventBusKafka.DeSerializer;
using EventBusKafka.Serializer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBusKafka
{
    public class EventBusKafka : IEventBus
    {
        private readonly IKafkaPersistantConnection persistantConnection;
        private readonly ILogger<EventBusKafka> logger;
        private readonly IEventBusSubscriptionsManager subsManager;
        
        public EventBusKafka(
            IKafkaPersistantConnection kafkaPersistantConnection, 
            ILogger<EventBusKafka> kafkaLogger, IEventBusSubscriptionsManager manager)
        {
            persistantConnection = kafkaPersistantConnection;
            logger = kafkaLogger;
            subsManager = manager;
            
        }
        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            if (!persistantConnection.IsConnected)
            {
                // Write the reconnect logic here
            }

            var eventName = integrationEvent.GetType().Name;

            logger.LogTrace("Publishing event to Kafka: {EventId} ({EventName})", integrationEvent.Id, eventName);

            using (var producer = new ProducerBuilder<Null, IntegrationEvent>(persistantConnection.GetProducerConfig())
                                                 .SetValueSerializer(new CustomValueSerializer<IntegrationEvent>())
                                                 .Build())
            {
                await producer.ProduceAsync(persistantConnection.Connection.Topic, new Message<Null, IntegrationEvent> { Value = integrationEvent });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {

            var eventName = subsManager.GetEventKey<T>();
         
            //logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
             subsManager.AddSubscription<T, TH>();
             StartBasicConsumeAsync();
            
        }
        private async Task StartBasicConsumeAsync()
        {
            logger.LogTrace("Starting Kafka basic consume");
            using (var consumer = new ConsumerBuilder<Ignore, IntegrationEvent>(persistantConnection.GetConsumerConfig())
                .SetValueDeserializer(new CustomValueDeserializer<IntegrationEvent>())
                .Build())
            {
                consumer.Subscribe(persistantConnection.Connection.Topic);
                await Task.Run(() =>
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume(default(CancellationToken));

                        if (consumeResult != null && consumeResult.Message != null)
                       
                        {
                            if (consumeResult.Message.Value is IntegrationEvent result)
                            {
                                ProcessEventAsync(consumeResult.Key.ToString(), result.ToString());
                            }
                        }

                    }
                });

                consumer.Close();
            }

        }
       
        private async Task ProcessEventAsync(string eventName, string message)
        {
            logger.LogTrace("Processing Kafka event: {EventName}", eventName);

            if (subsManager.HasSubscriptionsForEvent(eventName))
            {

                //using (var scope = serviceProvider.CreateScope())
                //{
                //    var subscriptions = subsManager.GetHandlersForEvent(eventName);

                //    foreach (var subscription in subscriptions)
                //    {
                        
                //            var handler = scope.ResolveOptional(subscription.HandlerType);
                //            if (handler == null) continue;
                //            var eventType = subsManager.GetEventTypeByName(eventName);
                //            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                //            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                //            await Task.Yield();
                //            await(Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        
                //    }

                //}
            }
            else
            {
                logger.LogWarning("No subscription for Kafka event: {EventName}", eventName);
            }
        }
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subsManager.GetEventKey<T>();

            logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            subsManager.RemoveSubscription<T, TH>();
        }
    }
}
