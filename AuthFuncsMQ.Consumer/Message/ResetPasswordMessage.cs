using Azure.Communication.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Message
{
    internal class ResetPasswordMessage : EmailMessageBase
    {
        public override string Sender => EmailResource.ResetPasswordEmailSender;

        public override string Subject => EmailResource.ResetPasswordEmailSubject;

        public override string Body => EmailResource.ResetPasswordEmailBody;
    }
}
