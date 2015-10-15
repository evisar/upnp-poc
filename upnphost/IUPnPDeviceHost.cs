using OpenSource.UPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upnphost
{
    public interface IUPnPDeviceHost: IUPnPDevice
    {
        string Name { get; }
        string Namespace { get; }
        IList<IUPnPService> Services { get; }

        void Start();
        void Stop();
    }
}
