using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.impl
{
    public class EmailService: IEmailService
    {
        public void Send(string from, string to, string subject, string body)
        {
            Console.WriteLine("From:" + from);
            Console.WriteLine("To:" + to);
            Console.WriteLine("Subject:" + subject);
            Console.WriteLine("Body:" + body);
        }
    }
}
