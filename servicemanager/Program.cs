using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace servicemanager
{

    public class Service
    {
        readonly ServiceController _sc;

        public Service(ServiceController sc)
        {
            _sc = sc;
        }
        public void Start()
        {
            _sc.Start();
        }

        public void Stop()
        {
            _sc.Stop();
        }

        public ServiceControllerStatus GetStatus()
        {
            return _sc.Status;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var device = UPnPDevice.CreateRootDevice(int.MaxValue, 1.0, "\\");
            device.FriendlyName = Environment.MachineName + ":" + "Service Manager";
            device.DeviceURN = "ServiceManager";
            device.HasPresentation = false;

            foreach(var svc in ServiceController.GetServices())
            {
                var upnsvc = new UPnPService(1.0, typeof(ServiceController).GUID.ToString(), svc.DisplayName, true, new Service(svc));
                upnsvc.AddMethod("Start");
                upnsvc.AddMethod("Stop");
                upnsvc.AddMethod("GetStatus");
                device.AddService(upnsvc);
            }

            device.StartDevice();

            Console.ReadLine();

            device.StopDevice();
        }
    }
}
