using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusKafka
{
    public class KafkaPersistantConnection : IKafkaPersistantConnection
    {
        private readonly IKafkaConnection connection;
        private readonly ILogger<KafkaPersistantConnection> logger;
        public KafkaPersistantConnection(IKafkaConnection kafkaConnection, ILogger<KafkaPersistantConnection> kafkaLogger)
        {
            connection = kafkaConnection;
            logger = kafkaLogger;
            Connection = connection;
        }
 
        public bool IsConnected
        {
            get
            {
                return connection.Host != String.Empty && connection.Port != String.Empty && connection.Topic != String.Empty;
            }
        }

        public IKafkaConnection Connection { get ; set; }

        public ConsumerConfig GetConsumerConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = $"{connection.Host}:{connection.Port}",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = $"{connection.Host}:{connection.Port}"
            };
        }
    }
}
