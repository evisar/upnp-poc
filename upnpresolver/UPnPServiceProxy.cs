using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImpromptuInterface;

namespace upnpresolver
{
    public class UPnPServiceProxy: DynamicObject
    {


        readonly static UPnPSmartControlPoint _sc;
        static List<UPnPDevice> _devices = new List<UPnPDevice>();
        static UPnPServiceProxy()
        {
            lock (_devices)
            {
                _sc = new UPnPSmartControlPoint((sender, device) =>
                {
                    _devices.Add(device);
                    Console.WriteLine(device.FriendlyName);
                });
            }
        }

        public static void Discover() {
            _sc.Rescan();
        }

        public static T Resolve<T>()
            where T: class
        {
            var proxy = new UPnPServiceProxy(typeof(T)).ActLike<T>();
            return proxy;
        }

        readonly UPnPService _service;
        private UPnPServiceProxy(Type type)
        {
            _service = _devices.SelectMany(device => device.Services).Where(svc => svc.ServiceID == string.Format("urn:upnp-org:serviceId:{0}", type.GUID)).FirstOrDefault();            
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            List<UPnPArgument> upnpargs = new List<UPnPArgument>();
            var action = _service.GetAction(binder.Name);
            int count = 0;
            foreach(var arg in action.ArgumentList)
            {
                upnpargs.Add(new UPnPArgument(arg.Name, args[count++]));
            }            
            result = _service.InvokeSync(binder.Name, upnpargs.ToArray());
            return true;
        }
    }
}
