using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.impl
{
    public class SmsService: ISmsService
    {
        public void Send(string number, string message)
        {
            Console.WriteLine("{0}: sms://{1}/{2}", DateTime.Now, number, message);
        }
    }
}
