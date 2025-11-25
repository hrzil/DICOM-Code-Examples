/*
 * 
 * Copyright (c) 2015, H.R.Z. SOftware Services LTD
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions 
 * are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
 *    in the documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from 
 *    this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

/// \page CSTestApplications C# Test Applications
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// \section DCXACCEventsExample Using of DCXACCEvents object Example
/// This example demonstrates how to handle events using RZDCX objects.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using rzdcxLib;

namespace DCXACCEventsExample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                int cookie;
                RzdcxEventsSink mySink;
                mySink = new RzdcxEventsSink();
                DCXACC myServerClass = new DCXACC();
                IConnectionPointContainer icpc = (IConnectionPointContainer)myServerClass;
                System.Runtime.InteropServices.ComTypes.IConnectionPoint icp;
                Guid IID_IMyEvents = typeof(IDCXACCEvents).GUID;

                icpc.FindConnectionPoint(ref IID_IMyEvents, out icp);
                icp.Advise(mySink, out cookie);

                //Call to server function causes events to be raised.
                MainEventLoop(myServerClass);

                icp.Unadvise(cookie);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void MainEventLoop(DCXACC acc)
        {

            DCXAPP app = new DCXAPP();
            app.StartLogging("c:\\tmp\\rzdcxLog.txt");

            int loops = 1000;
            // This is the standard loop to wait for connections and commands
            while (loops-- > 0)
            {
                if (acc.WaitForConnection("RZDCXTEST", 1104, 30))
                {
                    // Can go to new thread
                    bool res;
                    do
                    {
                        if (acc.WaitForCommand(30))
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    } while (res);
                }
            }

            app.StopLogging();

            Marshal.ReleaseComObject(acc);

            Marshal.ReleaseComObject(app);
        }
    }
}
