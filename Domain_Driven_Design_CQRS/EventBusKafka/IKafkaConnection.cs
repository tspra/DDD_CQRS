using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusKafka
{
    public interface IKafkaConnection
    {
        public string Host { get; set; }
        public string Topic { get; set; }
        public string Port { get; set; }
    }
}
