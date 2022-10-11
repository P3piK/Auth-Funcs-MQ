using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AuthFuncsMQ.Consumer;
using AuthFuncsMQ.Consumer.Configuration;
using AuthFuncsMQ.Consumer.Interface;
using AuthFuncsMQ.Consumer.Service;
using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthFuncsMQ
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    var keyVaultEndpoint = new Uri("https://authfuncsvault.vault.azure.net/");
                    configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
                })
                .ConfigureServices((_, services) =>
                    services.AddHostedService<EmailServiceBusProcessor>()
                            .AddScoped<IEmailService, EmailService>()
                            .AddSingleton<ServiceBusConfig>())
                .Build();

            await host.RunAsync();
        }
    }
}