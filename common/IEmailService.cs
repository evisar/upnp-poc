using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [ServiceContract]
    public interface IEmailService
    {
        [OperationContract]
        void Send(string from, string to, string subject, string body);
    }
}
