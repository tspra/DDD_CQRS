using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusKafka
{
    public interface IKafkaPersistantConnection
    {
       
        public bool IsConnected { get; }

        public ProducerConfig GetProducerConfig();

        public ConsumerConfig GetConsumerConfig();

        public IKafkaConnection Connection { get; set; }
    }
}
