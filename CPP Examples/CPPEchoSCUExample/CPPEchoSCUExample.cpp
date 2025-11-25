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

/// \page CPPTestApplications C++ Test Applications
/// The C++ test applications can be downloaded from www.roniza.com/downloads
/// \section CPPEchoDemo C++ Echo Demo
/// C++ Echo Demo
/// This demo shows how to verify comunication with another Application Entity using the C-ECHO command

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>
#include "atlconv.h"

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

using namespace rzdcxLib;
using namespace std;

/// \example CPPEchoSCUExample.cpp 
/// ECHO SCU Example
/// Command line arguments are:
/// 1) Local AE
/// 2) Remote AE
/// 3) Host (of target AE)
/// 4) Port (of target AE)
/// 5) OPTIONAL! secure network (any value)
int main(int argc, char* argv[])
{
    /// See if we have all the params
    if (argc <= 4) 
		return 1;

	const char
		* localAE(argv[1]),
		* remoteAE(argv[2]),
		* host(argv[3]),
		* port(argv[4]);

	bool secure(argc > 5);

	HRESULT hr = ::CoInitialize(NULL); 

	_bstr_t e;
	bool failed(false);
	if (SUCCEEDED(hr))\
    {
		IDCXAPPPtr app(__uuidof(DCXAPP));
		app->LogLevel = LOG_LEVEL_DEBUG; // Debug
		app->StartLogging("CPPEchoSCUExample.log");

        /// Create a requester object to call the echo method
		IDCXREQPtr req(__uuidof(DCXREQ));

		IDCXSECPtr sec(__uuidof(DCXSEC));
		if (secure)
			req->SecurityContext = sec;

		try
        {
            /// Call echo
			req->Echo(
				localAE, 
				remoteAE, 
				host,
				atoi(port));
			printf("Echo succeeded\n");
		}
        catch (_com_error& err)
        {
			failed=true;
			e = err.Description();
            /// If we have an exceptions, the Echo failed
			printf("Echo failed\n");
        }
        catch (...)
        {
            /// If we have an exceptions, the Echo failed
			printf("Unknown Error!\nEcho failed\n");
		}

		app->StopLogging();

	}

	if (failed) {
		USES_CONVERSION;
		printf("Message: %s\n", W2A(e));
	}

	::CoUninitialize();

	return 0;
}

