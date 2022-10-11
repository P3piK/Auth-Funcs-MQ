using AuthFuncsMQ.Consumer.Configuration;
using AuthFuncsMQ.Consumer.Interface;
using Azure.Core;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer
{
    public class EmailServiceBusProcessor : BackgroundService
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

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("\nStarting the receiver...");

            processor.ProcessMessageAsync += EmailService.MessageHandler;
            processor.ProcessErrorAsync += EmailService.ErrorHandler;

            await processor.StartProcessingAsync();

            Console.WriteLine("Receiving messages started!");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("\nStopping the receiver...");

            await processor.StopProcessingAsync();

            Console.WriteLine("Stopped receiving messages");

            await processor.DisposeAsync();
            await client.DisposeAsync();

            await base.StopAsync(cancellationToken);
        }
    }
}
