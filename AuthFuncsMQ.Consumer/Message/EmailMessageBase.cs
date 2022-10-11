using Azure.Communication.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Message
{
    public abstract class EmailMessageBase 
    {
        public abstract string Sender { get; }
        public abstract string Subject { get; }
        public abstract string Body { get; }

        public EmailMessage GenerateMessage(string recipient)
        {
            return new EmailMessage(
                Sender,
                new EmailContent(Subject)
                {
                    Html = Body
                },
                new EmailRecipients(new List<EmailAddress>()
                {
                    new EmailAddress(recipient)
                    {
                        DisplayName = "no-reply AuthFuncs"
                    }
                }));
        }
    }
}
