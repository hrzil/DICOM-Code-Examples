using rzdcxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecureDICOMExample
{
    internal class SecureServer
    {
        public bool isRuning;
        string AET;
        ushort Port;

        DCXACC acc;
        DCXSEC sec;
        private Thread worker;
        DCXAPP app;

        public SecureServer(string aet, ushort port, SecurityContext security=null, DCXAPP logger = null)
        {
            this.app = logger;
            this.AET = aet;
            this.Port = port;

            this.acc = new DCXACC();

            if (security != null)
            {
                this.sec = security.ToDCXSEC();
                this.acc.SecurityContext = this.sec;
            }
        }

        public void Listen()
        {
            this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Lister: Starting");
            this.isRuning= true;
            while (this.isRuning)
            {
                this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Lister: Waiting for connection");
                if (this.acc.WaitForConnection(this.AET, this.Port, 5))
                {
                    this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Lister: Waiting for command");
                    this.acc.WaitForCommand(15);
                }
            }
        }
        public void Stop()
        {
            this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Lister: Stopping");

            this.isRuning = false;
        }
    }
}
