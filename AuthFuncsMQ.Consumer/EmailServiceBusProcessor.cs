using AuthFuncsMQ.Consumer.Configuration;
using AuthFuncsMQ.Consumer.Interface;
using Azure.Core;
using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer
{
    public class EmailServiceBusProcessor
    {
        #region Fields

        private ServiceBusClient client;
        private ServiceBusProcessor processor;
        private ServiceBusClientOptions clientOptions;

        #endregion

        public EmailServiceBusProcessor(ServiceBusConfig serviceBusConfig, IEmailService emailService)
        {
            clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
            client = new ServiceBusClient(serviceBusConfig.ConnectionString, clientOptions);
            processor = client.CreateProcessor(serviceBusConfig.QueueName, new ServiceBusProcessorOptions());

            EmailService = emailService;
        }

        #region Properties

        public IEmailService EmailService { get; }

        #endregion

        public async Task RunAsync()
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443. 
            // If you use the default AmqpTcp, you will need to make sure that the ports 5671 and 5672 are open

            try
            {
                processor.ProcessMessageAsync += EmailService.MessageHandler;
                processor.ProcessErrorAsync += EmailService.ErrorHandler;

                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
