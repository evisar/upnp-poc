using common;
using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upnpresolver.console
{
    class Program
    {

        

        static void Main(string[] args)
        {

            UPnPServiceProxy.Discover();

            Console.ReadLine();

            var proxy = UPnPServiceProxy.Resolve<ISmsService>();
            proxy.Send("089499254", "hello");
            
        }
    }
}
