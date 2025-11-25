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
/// \section CPPStorageSCPDemo C++ Storage SCP Demo
/// C++ Storage SCP Demo
/// This is a test application for the association accepter class.
/// The application listens on a port and stores DICOM objects

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

//**************************************************
//*** REPLACE THE PATH HERE TO WHERE RZDCX.DLL IS
//**************************************************
//#import "../../../BUILD/Win32/Debug/bin/rzdcx.dll"
#include <iostream>

using namespace rzdcxLib;
using namespace std;

using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


/// \example CPPInMemoryStorageSCPExample.cpp
/// Single Threaded DICOM Storage SCP Example with in memory handling
void main()
{
 	HRESULT hr = ::CoInitialize(NULL); 
	if (SUCCEEDED(hr))
	{
		try
		{
			IDCXAPPPtr app(__uuidof(DCXAPP));
			app->put_EnableInMemoryStorageSCP(VARIANT_TRUE);
			bool isRunning(true);
			while (isRunning)
			{
				IDCXACCPtr acc(__uuidof(DCXACC));
				
				try
				{
					/// Wait for the connection on port: 1104,
					/// timeout: 30 secs
					if (acc->WaitForConnection(L"RZDCXEXAMPLE", 8104, 5))
					{
						/// If you want you can go to a new thread here
						bool res;
						do {
							/// Wait for a command from the client
							if (acc->WaitForCommand(30) == VARIANT_TRUE)
							{
								IDCXOBJPtr storedObject = acc->TakeStoredObject();
								if (storedObject)
								{
									IDCXELMPtr e = storedObject->GetElement(sopInstanceUID);
									if (e)
									{
										wstring fname(e->GetValue().bstrVal);
										wprintf(L"Instance Stored: %s and is in memory\n", e->GetValue().bstrVal);
										// Now we can for example take the bitmap and save a thumbnail bitmap
										IDCXIMGPtr image = storedObject->GetImage();
										IDCXIMGPtr thumbnail = image->ScaledImage(150.*image->width/image->Height,150);
										fname+=L".bmp";
										thumbnail->SaveBitmap(0, _bstr_t(fname.c_str()));
									}
								}
								res = true;
							}
							else
							{
								res = false;
							}
						} while (res);
					}
				}
				catch (...)
				{
					wprintf(L"store failed");
				}
			}
			wprintf(L"store succeeded\n");
		}
		catch (...)
		{
			wprintf(L"store failed");
		}
		::CoUninitialize();
	}
}
