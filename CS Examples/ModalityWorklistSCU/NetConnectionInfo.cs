using System;
using System.Collections.Generic;
using System.Text;

namespace ModalityWorklistSCU
{
    class NetConnectionInfo
    {
        public string CallingAETitle { set; get; }
        public string CalledETitle { set; get; }
        public string Host { set; get; }
        public UInt16 Port { set; get; }
    }
}
