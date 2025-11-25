/*
 * 
 * Copyright (c) 2015 - 2023, H.R.Z. SOftware Services LTD
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

/// \page MTTA Multi-Threaded Test Applications
/// The C++ test applications can be downloaded from www.roniza.com/downloads
/// \section CPPMultithreadedServerDemo C++ Multi-Threaded Storage SCP Demo
/// C++ Multithreaded Server Demo
/// This is a test application for the association accepter class.
/// The application listens on a port
/// The Sink class is the way to implement the event callbacks from
/// the dll.
/// This demo is multithreaded and it creates a thread to wait on every connection
/// and process it so connections can be processed simultaniously.

#include "stdafx.h"
#include "CPPMultithreadedServerExample.h"
#include <stdio.h>
#include <time.h>
#include <afx.h>
#include <string>
#include <algorithm>
#include "Sink.h"
#include "AssocThread.h"

using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

/// The one and only application object

CWinApp theApp;

using namespace std;

static bool isRunning = true;

struct Stopper 
{
    static void Stop() { isRunning = false; }
};

HANDLE ConsoleEventManager<Stopper>::m_endEvent;

class MoveThread : public rz::thread
{
public:
    virtual void run();
};

/// \example CPPMultithreadedServerExample.cpp C++ Multi-Threaded Storage SCP Example
/// This is a simple example of how RZDCX can be used in a multithreaded application
/// In this case the application waits for incomming connections and handles each association
/// on a separate thread.
/// First argument is optional certificate subject name. If provided use secure DICOM network.
int main(int argc, char* argv[])
{
    // Create the console event manager so that if the
    // program was interrupted in the middle of the loop,
    // it will finish properly (Allways create it in the beggining
    // of the program)
    ConsoleEventManager<Stopper> cem;

    int nRetCode = 0;

	// initialize MFC and print and error on failure
	if (!AfxWinInit(::GetModuleHandle(NULL), NULL, ::GetCommandLine(), 0))
	{
		// TODO: change error code to suit your needs
		printf("Fatal Error: MFC initialization failed\n");
		nRetCode = 1;
	}
	else
	{
		while (isRunning)
        {
            string certSubjectName;
            if (argc > 1)
                certSubjectName = argv[1];
            // Create and start a new thread to wait for a connection
            AssocThread thread(certSubjectName);
            thread.start();
            
            // Create an event to know when a connection has been
            // established
            HANDLE hEvent = thread.createAndGetEvent();

            // Wait on the event, when a connection is
            // established we need to create another
            // thread and start waiting for another connection
            WaitForSingleObject(hEvent, INFINITE);
            CloseHandle(hEvent);
        }
	}

	return nRetCode;
}
