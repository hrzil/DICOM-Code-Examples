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
/// The application listens on a port
/// The Sink class is the way to implement the event callbacks from
/// the dll.
/// This application serves as Modality Worklist SCP as well. 

#include "stdafx.h"
#include "CPPStorageSCPExample.h"
#include <stdio.h>
#include <time.h>
#include <afx.h>
#include <string>
#include "Sink.h"
#include "AppEventsSink.h"

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

/// Enhanced Logging 
/// Here we initialize the enhanced logging option
class AppInterfaceHolder 
{
	IDCXAPPPtr app;
	DWORD dwCookie;
	CAppEventsSink *pSink;
	IConnectionPointContainerPtr ptrConPntCnt;
	IConnectionPointPtr ptrCon;
	IUnknown* pHandler;

public:
	AppInterfaceHolder() 
		: app(__uuidof(DCXAPP))
		, pHandler(NULL)
	{
		Setup();
	}
	~AppInterfaceHolder()
	{
		Cleanup();
	}
	void Setup()
	{
		if (app)
		{
			ptrConPntCnt = (IDispatch*)app;
			if(ptrConPntCnt != NULL)
			{
				/// Connect the acceptor events to the Sink class that handles
				/// them.
				ptrConPntCnt->FindConnectionPoint(__uuidof(_IDCXAPPEvents),&ptrCon);
				pSink = new CAppEventsSink();
				pHandler = pSink->GetInterface(&IID_IUnknown);
				ptrCon->Advise(pHandler,&dwCookie);
			}
			/// set it to a very small value
			app->MaxLogMessages = 10;
			app->LogLevel = LOG_LEVEL_DEBUG;
			app->StartLogging("c:\\rzdcx_first_log_file.log");
		}
	}
	void Cleanup()
	{
		if (app) {
			if (ptrCon)
			{
				/// Unattach the event callbacks.
				ptrCon->Unadvise(dwCookie);
				ptrCon.Release();
				if (ptrConPntCnt) {
					ptrConPntCnt.Release();
				}
			}
			delete pSink;
		}
	}
};


/// \example CPPStorageSCPExample.cpp
/// Single Threaded DICOM Storage SCP Example
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
		HRESULT hr = ::CoInitialize(NULL); 

		if (SUCCEEDED(hr)){

			/// Create the application holder inside the scope of COM CoInitialize
			AppInterfaceHolder appHolder;

            /// Create the acceptor object for waiting for
            /// connections and commands
			IDCXSECPtr sec(__uuidof(DCXSEC));


			DWORD NameLength = 0;
			wstring compname;
			if (ERROR_SUCCESS == ::GetComputerNameExW(ComputerNameDnsFullyQualified, NULL, &NameLength))
			{
				compname.resize(NameLength);
				if (1 == ::GetComputerNameExW(ComputerNameDnsFullyQualified, &compname[0], &NameLength))
				{
					sec->CertSubjectName = compname.c_str();
				}
			}

			printf("CertSubjectName: %S\n", (LPCWSTR)(sec->CertSubjectName));

			IDCXACCPtr acc(__uuidof(DCXACC));
			if (argc>1)
				acc->SecurityContext = sec;

			if(acc)
			{
				IConnectionPointContainerPtr ptrConPntCnt = (IDispatch*)acc;
				IConnectionPointPtr ptrCon;
				IUnknown* pHandler = NULL;
				DWORD dwCookie;

				if(ptrConPntCnt != NULL)
				{
                    /// Connect the acceptor events to the Sink class that handles
                    /// them.
					ptrConPntCnt->FindConnectionPoint(__uuidof(IDCXACCEvents),&ptrCon);
			                    
					CSink *pSink = new CSink();

					pHandler = pSink->GetInterface(&IID_IUnknown);
					ptrCon->Advise(pHandler,&dwCookie);
			              
					try{
                        /// This is the Local AE
						string ae = "BARIIRZDCXCPPEXAMPLE";
						USES_CONVERSION;
						BSTR ae_title = A2BSTR(ae.c_str());

                        /// This is the way to use the acceptor to
                        /// run and wait for connections and commands.
						while (isRunning)
						{
							try
							{
                                /// Wait for the connection on port: 104,
                                /// timeout: 30 secs
								if (acc->WaitForConnection(ae_title, 1104, 5))
								{
									/// If you want you an go to a new thread here
									bool res;
									do {
                                        /// Wait for a command from the client
										if (acc->WaitForCommand(30) == VARIANT_TRUE)
										{
											printf("%s %s %s", pSink->mTID.c_str(), pSink->mSList.c_str(), pSink->mFList.c_str());
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
                                printf("store failed");
							}
                            /// If you want to sleep in the middle:
							/// Sleep(1000);
						}
						printf("store succeeded\n");
					}catch (...){
						printf("store failed");
					}

                    /// Unattach the event callbacks.
					ptrCon->Unadvise(dwCookie);

					ptrCon.Release();
					ptrConPntCnt.Release();
					delete pSink;

				}
			}
		}
		
		::CoUninitialize();
	}

	return nRetCode;
}
