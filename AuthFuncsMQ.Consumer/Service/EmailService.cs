using AuthFuncsMQ.Consumer.Factory;
using AuthFuncsMQ.Consumer.Interface;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Service
{
    public class EmailService : IEmailService
    {
        private EmailClient emailClient;

        public EmailService()
        {
            //emailClient = new EmailClient(connectionString);
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            var request = JsonSerializer.Deserialize<EmailServiceRequest>(body);

            if (request != null)
            {
                var messagefactory = new EmailMessageFactory();
                var emailMessage = messagefactory.CreateMessage(request.Recipient, request.ActionName);

                if (emailMessage != null)
                {
                    await emailClient.SendAsync(emailMessage);

                    await args.CompleteMessageAsync(args.Message);
                }
            }
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}
