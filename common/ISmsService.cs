using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace common
{
    [ServiceContract]
    public interface ISmsService
    {
        [OperationContract]
        void Send(string number, string message);
    }
}

