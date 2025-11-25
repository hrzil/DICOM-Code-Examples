using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzdcxLib;

namespace SecureDICOMExample
{
    internal class SecureClient
    {
        /// <summary>
        /// Send a C-ECHO, with optional security layer
        /// </summary>
        /// <param name="callingAE"></param>
        /// <param name="calledAE"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public string SendEcho(string callingAE, string calledAE, string host, ushort port, SecurityContext security=null)
        {
            // a requester to send echo
            DCXREQ req = new DCXREQ();
            if(security != null)
            {
                // create a DCXSEC object from the SecurityContext
                DCXSEC sec = security.ToDCXSEC();
                // and insert it to the requester
                req.SecurityContext = sec;
            }
            try
            {
                //send echo
                req.Echo(callingAE, calledAE, host, port);
                return "";
            }
            catch(Exception ex)
            {
                // something went wrong
                return ex.Message;
            }
        }
    }
}
