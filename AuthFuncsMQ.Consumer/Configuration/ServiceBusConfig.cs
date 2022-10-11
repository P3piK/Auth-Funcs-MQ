using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Configuration
{
    public class ServiceBusConfig
    {
        public ServiceBusConfig(IConfiguration configuration)
        {
            ConnectionString = configuration["BusServiceConnectionString"];
            QueueName = configuration["BusServiceQueueName"];
        }

        public string ConnectionString { get; }
        public string QueueName { get; }
    }
}
