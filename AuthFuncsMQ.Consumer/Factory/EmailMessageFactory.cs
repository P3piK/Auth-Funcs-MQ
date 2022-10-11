using AuthFuncsMQ.Consumer.Message;
using AuthFuncsMQ.Consumer.Service;
using Azure.Communication.Email.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Factory
{
    internal class EmailMessageFactory
    {
        public EmailMessage? CreateMessage(string recipient, EmailWorkerActionName action)
        {
            var messageBase = default(EmailMessageBase);

            switch (action)
            {
                case EmailWorkerActionName.PasswordReset:
                    messageBase = new ResetPasswordMessage();
                    break;
                default:
                    // logger
                    break;
            }

            return messageBase?.GenerateMessage(recipient);
        }
    }
}
