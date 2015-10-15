using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplelight
{

    public class SwitchPower
    {
        bool _value;
        public void SetTarget(int value)
        {
            _value = value==0 ? false : true;
            Console.WriteLine("Power: {0}", _value ? "On": "Off");
        }

        public int GetTarget()
        {
            return _value ? 1 : 0;
        }

        public bool GetStatus()
        {
            return _value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var device = UPnPDevice.CreateRootDevice(int.MaxValue, 1.0, "\\");
            device.FriendlyName = Environment.MachineName + ":" + "Network Light";
            device.DeviceURN = "NetworkLight";
            device.HasPresentation = false;

            var switchPower = new UPnPService(1.0, typeof(SwitchPower).GUID.ToString(), typeof(SwitchPower).FullName, true, new SwitchPower());
            switchPower.AddMethod("SetTarget");
            switchPower.AddMethod("GetTarget");
            switchPower.AddMethod("GetStatus");
            device.AddService(switchPower);

            device.StartDevice();

            Console.ReadLine();

            device.StopDevice();
        }
    }
}
