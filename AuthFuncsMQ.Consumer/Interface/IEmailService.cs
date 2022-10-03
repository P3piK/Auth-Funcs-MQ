using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Interface
{
    public interface IEmailService
    {
        Task MessageHandler(ProcessMessageEventArgs args);
        Task ErrorHandler(ProcessErrorEventArgs args);
    }
}
