using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsMQ.Consumer.Service
{
    public enum EmailWorkerActionName
    {
        PasswordReset,
    }

    public class EmailServiceRequest
    {
        public string Recipient { get; set; }
        public string Action { get; set; }
        public EmailWorkerActionName ActionName => 
            (EmailWorkerActionName)Enum.Parse(typeof(EmailWorkerActionName), Action);
    }
}
