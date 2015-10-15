using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace upnphost
{
    public class UPnPDeviceHostServiceProxy: IUPnPDeviceHost
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }
        public double Version { get; private set; }

        public IList<IUPnPService> Services { get; private set; }

        UPnPDevice _device;

        public UPnPDeviceHostServiceProxy(Assembly assembly)
        {
            this.Name = Environment.MachineName + ":" + assembly.FullName.Split(',').First();
            this.Namespace = assembly.FullName;
            var regex = new Regex("Version=(.\\..)");
            var version = regex.Match(this.Namespace).Groups[1].Value;
            this.Version = double.Parse(version);

            _device = UPnPDevice.CreateRootDevice(int.MaxValue, this.Version, "\\");
            _device.FriendlyName = this.Name;
            _device.DeviceURN = this.Namespace;
            _device.HasPresentation = false;

            var types = (from type in assembly.GetTypes()
                         let ifs = type.GetInterfaces().Where(t => t.GetCustomAttributes<ServiceContractAttribute>().Count() > 0).First()
                         select new { Type = type, Interface = ifs }).ToList();

            List<IUPnPService> services = new List<IUPnPService>();
            foreach (var tc in types)
            {
                var service = new UPnPServiceProxy(tc.Type, tc.Interface);
                services.Add(service);
                _device.AddService(service);
            }

            this.Services = services.ToList();
            _device = this.GetUPnPDevice();
        }

        public void Start()
        {
            _device.StartDevice();
        }

        public void Stop()
        {
            _device.StopDevice();
        }
    
        public UPnPDevice GetUPnPDevice()
        {
            return _device;
        }
    }
}
