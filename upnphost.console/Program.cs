using common.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace upnphost.console
{
    class Program
    {
        static IUPnPDeviceHost _host;

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            if (_host != null)
            {
                _host.Stop();
            }

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }

        static Program()
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
        }

        #endregion

        static void Main(string[] args)
        {
            _host = new UPnPDeviceHostServiceProxy(typeof(SmsService).Assembly);
            _host.Start();

            Console.ReadLine();

            _host.Stop();
            _host = null;
        }
    }
}
