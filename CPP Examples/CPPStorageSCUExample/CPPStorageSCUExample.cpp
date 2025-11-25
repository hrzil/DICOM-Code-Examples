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
/// \section CPPStorageSCUDemo C++ Storage SCU Demo
/// C++ Storage SCU Demo
/// This example shows how to send DICOM files.
/// The application accept the list of file as command line arguments separated by a ; (smicolon)
/// To run the application simply call 
/// CPPStorageSCPExample local-ar remote-ae remote-host remote-port file1[;file2[;file3] ...]

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>

/// Please put MODALIZER-SDK distribution from downloads.roniza.com/MODALIZER-SDK in a folder next to the examples folder
/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

/// \example CPPStorageSCUExample.cpp
/// This example shows how to send DICOM files.
/// 1) LocalAE
/// 2) TargetAE
/// 3) Host
/// 4) Port
/// 5) ";" separated files list
int main(int argc, char* argv[])
{
    /// See if we have all the params
    if (argc <= 5) return 4;

    HRESULT hr = ::CoInitialize(NULL); 

	if (SUCCEEDED(hr)){ 

		IDCXAPPPtr app(__uuidof(DCXAPP));
		app->StartLogging("c:\\rzdcx.log");
		app->LogLevel = LOG_LEVEL_DEBUG;

        /// Create the requester object to call send
		IDCXREQPtr req(__uuidof(DCXREQ));

        /// File lists of succeeded and failed files
		BSTR succeededFilesList = NULL;
		BSTR failedFilesList =  NULL;
        try{
            /// Call the send method and send the files
			req->Send(argv[1],
					  argv[2],
					  argv[3],
					  atoi(argv[4]),
					  argv[5],
					  &succeededFilesList,
					  &failedFilesList);

			printf("Send succeeded\n");
			printf("Succedded files: %S\n", succeededFilesList);
			printf("Failed files: %S\n", failedFilesList);
			/// Don't forget to free the memory of out parameters!!!
			SysFreeString(succeededFilesList);
			SysFreeString(failedFilesList);
		}
		catch (_com_error& err)
		{
			printf("Send failed: %s\n", (char*)err.Description());
		}
		catch (...)
		{
			printf("Send failed");
			::CoUninitialize();

			return 1;
		}
	}

	::CoUninitialize();

	return 0;
}

