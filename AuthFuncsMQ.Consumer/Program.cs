using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AuthFuncsMQ.Consumer;
using AuthFuncsMQ.Consumer.Configuration;
using AuthFuncsMQ.Consumer.Service;
using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AuthFuncsMQ
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var serviceBusConfig = config.GetRequiredSection("ServiceBusConfig").Get<ServiceBusConfig>();

            var emailService = new EmailService();
            await new EmailServiceBusProcessor(serviceBusConfig, emailService)
                .RunAsync();
        }
    }
}