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
/// \section CPPQueryRetrieveSCUDemo C++ Query/Retrieve SCU Demo
/// C++ Query Retrieve SCU Demo
/// This example shows how to call Query to the target AE
/// on patients and how to retrieve the data (Move)


#include "stdafx.h"

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>
#include <atlbase.h>
#include "atlconv.h"
#include "DcxreqSink.h"

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

/// \example CPPQueryRetrieveSCUExample.cpp
/// This example shows how to send a DICOM Query to the target AE
/// on patients and how to retrieve the data (Move)
/// 1) "Query" or "Move"
/// 2) LocalAE
/// 3) TargetAE
/// 4) Host
/// 5) Port
/// 6) AE to move to (only with Move) 
/// 7) Patient ID to move (only for Move)

int main(int argc, char* argv[])
{
    /// See if we have any command line args we recognise
    if (argc <= 1) 
		return 1;

    /// If query requested
    if (strcmp(argv[1], "Query") == 0) { 
		HRESULT hr = ::CoInitialize(NULL); 

		if (SUCCEEDED(hr))
		{
			// Start logging
			IDCXAPPPtr app(__uuidof(DCXAPP));
			app->LogLevel = LOG_LEVEL_DEBUG;
			app->StartLogging("CPPQueryRetrieveSCUExample.log");
            
			/// Create a requester object
			IDCXREQPtr req(__uuidof(DCXREQ));

			DcxreqSink sink;
			sink.Connect((IDCXREQ*)req);

			req->AssociationRequestTimeout = 5;
			req->DimseCommandTimeout = 30;

            /// Create a DCXOBJ to fill it with the query data
			IDCXOBJPtr obj(__uuidof(DCXOBJ));
			
			IDCXELMPtr el(__uuidof(DCXELM));
			
			char* error=0;
			try
			{
                /// Query type - Patient
				el->Init(QueryRetrieveLevel);
				el->Value = "PATIENT";
				obj->insertElement(el);

                /// All query names
				el->Init(PatientsName);
				el->Value = "*";
				obj->insertElement(el);

                /// All query IDs
				el->Init(patientID);
				el->Value = "*";
				obj->insertElement(el);

                /// Call query method
				IDCXOBJIteratorPtr it = req->Query(argv[2], 
					argv[3], 
					argv[4],
					atoi(argv[5]),
					"1.2.840.10008.5.1.4.1.2.2.1", /// Query sop class uid
					obj);

                /// Iterate over the query results
				for (; !it->AtEnd(); it->Next())
				{
					IDCXOBJPtr currObj = it->Get();

					_variant_t ret;
					IDCXELMPtr elem = currObj->getElementByTag(0x00100010);
					if (elem != NULL)
					{
						ret = elem->Value;
					}

					string name = static_cast<_bstr_t>(ret);
                    printf("Patient name: %s\n", name.c_str());

					elem = currObj->getElementByTag(0x00100020);
					if (elem != NULL)
					{
						ret = elem->Value;
					}

					string id = static_cast<_bstr_t>(ret);
                    printf("Patient ID: %s\n", id.c_str());
				}

				printf("Query succeeded\n");
			}
			catch (_com_error& err)
			{
				printf("COM Error: %s\n", (char*)err.Description());
			}
			catch (...)
			{
				printf("Query failed");
				::CoUninitialize();
				return 1;
			}

		}

		::CoUninitialize();
        return 0; 
        
    /// If move requested
    }
	else if (strcmp(argv[1], "Move") == 0) { 
		HRESULT hr = ::CoInitialize(NULL); 

		if (SUCCEEDED(hr)){
            /// Create a requester object
			IDCXREQPtr req(__uuidof(DCXREQ));

			DcxreqSink sink;
			sink.Connect((IDCXREQ*)req);
			
            /// Create an object to load the file and request
            /// move on it's instance
			IDCXOBJPtr obj(__uuidof(DCXOBJ));

			try{
				req->MovePatient(  argv[2], 
								   argv[3], 
								   argv[4],
								   atoi(argv[5]),
								   argv[6],
								   "*",
                                   argv[7]);

				printf("Move succeeded\n");
			}
			catch (_com_error& err)
			{
				printf("COM Error: %s\n", (char*)err.Description());
			}
			catch (...)
			{
				printf("Move failed");
				::CoUninitialize();
				return 1;
			}
		}

		::CoUninitialize();
        return 0; 
    }
}

