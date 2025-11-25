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

#include "StdAfx.h"
#include "AssocThread.h"
#include <string>
#include "Sink.h"

using namespace std;

// In this accepter we don't loop endlessly.
// We only expect one N-EVENT-REPORT to arrive so after the first association ends, we close
void AssocThread::run()
{
    // Call this in every thread to initialize the STL for the COM
    HRESULT hr = ::CoInitialize(NULL); 

		if (SUCCEEDED(hr)){
            /// Create the acceptor object for waiting for
            /// connections and commands
			IDCXACCPtr acc(__uuidof(DCXACC));

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
			                    
					CSink *pSink = new CSink(this);

					pHandler = pSink->GetInterface(&IID_IUnknown);
					ptrCon->Advise(pHandler,&dwCookie);
			              
                    /// \code

					try{
                        /// This is the Local AE
						string ae = "BARIIRZDCXCPPEXAMPLE";
						USES_CONVERSION;
						BSTR ae_title = A2BSTR(ae.c_str());

                        /// This is the way to use the acceptor to
                        /// run and wait for connections and commands.
                        bool recievedConnection = false;

						while (!recievedConnection)
						{
							try
							{
                                /// Wait for the connection on port: 1104,
                                /// timeout: 30 secs
								if (acc->WaitForConnection(ae_title, 1104, 30))
								{
                                    recievedConnection = true;

									/// If you want you an go to a new thread here
									bool res;
									do {
                                        /// Wait for a command from the client
										if (acc->WaitForCommand(300) == VARIANT_TRUE)
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
							catch (...)
							{
                                printf("Unknown Error");
							}
						}
					}catch (...) {
						printf("Unknown Error");
					}

                    /// \endcode

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

HRESULT AssocThread::OnCommitResult(bool result,
							   string& transactionID, 
							   string& succeededInstances, 
							   string& failedInstances)
{
	/// Check this transaction in some database and mark it according to status
	/// For example: SELECT * FROM COMMIT_REQUESTS WHERE TRANSACTION_ID = $(transactionID)
	/// Also compate succeeded and failed instances with the commit request
	printf("%s\n%s\n%s\n\n", transactionID.c_str(), 
							 succeededInstances.c_str(),
							 failedInstances.c_str());

	SetEvent();
	/// We can also delete here the two files we've created
	DeleteFile("IMG1");
	DeleteFile("IMG2");
	return NOERROR;
}

void AssocThread::SetEvent()
{
	::SetEvent(_hEvent);
}