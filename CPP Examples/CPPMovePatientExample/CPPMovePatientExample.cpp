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
/// \section CPPMoveSeriesExample C++ Demonstrating how to use MoveSeries command
/// C++ Demonstrating how to use MoveSeries command
/// This example shows how to use the DCXREQ Object in the
/// rzdcx.dll to move files of one Patient
/// IMPORTANT: The Target AE Title is not implemented in this example.
/// To run this you should a Move SCP to serve the MOVE and a STORE SCP to receive the files.

#include "stdafx.h"
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
#include "Sink.h"

using namespace rzdcxLib;
using namespace std;

/// \example CPPMoveSeriesExample.cpp 
/// Using MoveSeries command example
int main(int argc, char* argv[])
{
	CoInitialize(NULL);


	{
		IDCXREQPtr req(__uuidof(DCXREQ));

		IConnectionPointContainerPtr ptrConPntCnt = (IDispatch*)req;
		IConnectionPointPtr ptrCon;
		IUnknown* pHandler = NULL;
		DWORD dwCookie;

		if(ptrConPntCnt != NULL)
		{
			ptrConPntCnt->FindConnectionPoint(__uuidof(IDCXREQEvents),&ptrCon);

			CSink *pSink = new CSink();

			pHandler = pSink->GetInterface(&IID_IUnknown);
			ptrCon->Advise(pHandler,&dwCookie);

			try {
				req->MovePatient("RZDCXCPPEXAMPLE", "DSRSVC", "localhost", 104, "BARMPLUS", "*", "*");
				req.Release();
			} catch (_com_error& e) {
				printf("%s", _bstr_t(e.Description()));
			}

			ptrCon->Unadvise(dwCookie);
			ptrCon.Release();
			ptrConPntCnt.Release();
			delete pSink;
		}
	}

	CoUninitialize();
	return 0;
}

