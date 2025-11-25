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


/// \page CPPLogStreamExample C++ Test Applications
/// The C++ test applications can be downloaded from www.roniza.com/downloads
/// \section CPPLogStreamExample Log stream example
/// This demo shows how to use the log stream functionality

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>
#include "atlconv.h"
#include <shlwapi.h>

#import "..\..\MODALIZER-SDK\win32\rzdcx.dll"

using namespace rzdcxLib;
using namespace std;

class MyStream : public IStream
{

};

/// \example CPPLogStreamExample.cpp 
int main(int argc, char* argv[])
{
	HRESULT hr = ::CoInitialize(NULL); 

	if (SUCCEEDED(hr))
	{
		IDCXAPPPtr app(__uuidof(DCXAPP));
		app->LogLevel = LOG_LEVEL_DEBUG;
		app->MaxLogMessages = 250;
		app->StartLogging("modalizer-sdk.log");
		char buf[10];
		for (int i = 0; i < 49999; i++)
		{
			_bstr_t message("lOG MESSAGE #");
			itoa(i, buf, 10);
			message += buf;
			app->WriteToLog((LOG_LEVEL)(i%LOG_LEVEL_DEBUG+1), message);
		}
		app->StopLogging();
	}

	::CoUninitialize();

	return 0;
}

