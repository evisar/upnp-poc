using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;
using System.Dynamic;

namespace upnphost
{
    public class UPnPServiceProxy:  IUPnPService
    {
        readonly UPnPService _upnp;


        public UPnPServiceProxy(Type serviceType, Type contractType)
        {
            var service = Activator.CreateInstance(serviceType);
            _upnp = new UPnPService(1, contractType.GUID.ToString(), contractType.FullName, true, service);

            var methods = (from meth in contractType.GetMethods()
                          where meth.GetCustomAttributes(false).Where( atr => atr.GetType() == typeof(OperationContractAttribute)).Count()>0
                          select meth).ToArray();

            foreach (var meth in methods)
            {
                _upnp.AddMethod(meth.Name);
            }
        }

        public UPnPService GetUPnPService()
        {
            return _upnp;
        }

        public object service { get; set; }
    }
}
