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
/// \section CPPStorageCommitmentExample C++ Complete storage commit request/result example
/// Because the commit result is sent by the SCP on a separate association, we have to 
/// set up a listener to recieve this N-EVENT-REPORT on a separate thread.

#include "stdafx.h"
#include <stdio.h>
#include <time.h>
#include <afx.h>
#include <string>
#include "Sink.h"
#include "AssocThread.h"

using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

/****************************************************************
 * Utility function to create images for sending and commiting.
 ****************************************************************/

static int ROWS=512;
static int COLUMNS=512;
static int BITS_ALLOCATED=12;
static int BITS_STORED=16;
static int RESCALE_INTERCEPT=0;
static int SAMPLES_PER_PIXEL=1;
static int NUMBER_OF_FRAMES=1;
static char* PHOTOMETRIC_INTERPRETATION="MONOCHROME2";

static void pixels2safeArray(/*[In]*/unsigned char* pArr, /*[Out]*/SAFEARRAY*& sArr, /*[In]*/ULONG size) {
	HRESULT hr = S_OK;
	sArr = SafeArrayCreateVector(VT_UI1, 0, size);
	char* pdata;
	hr = SafeArrayAccessData(sArr, (void**)&pdata);
	for (UINT i = 0; i < size; i++) {
		pdata[i] = pArr[i];
	}
	hr = SafeArrayUnaccessData(sArr);
}

template<typename T>
static void CreateImage(char* filename, string& sopClassUID, string& sopInstanceUID)
{
    /// Create a DCXOBJ
    IDCXOBJPtr obj(__uuidof(DCXOBJ));

    /// Create an element pointer to place in the object for every tag
    IDCXELMPtr el(__uuidof(DCXELM));

	IDCXUIDPtr id(__uuidof(DCXUID));

	/// Set Hebrew Character Set
	el->Init(rzdcxLib::SpecificCharacterSet);
	el->Value = "ISO_IR 192";
    /// insert the element to the object
    obj->insertElement(el);

    /// You don't have to create an element every time, 
    /// just initialize it.
	char pn[]="John^Doe";
    el->Init(rzdcxLib::PatientsName);
    el->PutCStringPtr((int)pn);
    obj->insertElement(el);

	el->Init(rzdcxLib::patientID);
    el->Value = "123765";
    obj->insertElement(el);

	el->Init(rzdcxLib::studyInstanceUID);
	el->Value = id->CreateUID(UID_TYPE_STUDY);
	obj->insertElement(el);

	el->Init(rzdcxLib::seriesInstanceUID);
	el->Value = id->CreateUID(UID_TYPE_SERIES);
	obj->insertElement(el);
	
	el->Init(rzdcxLib::sopInstanceUID);
	el->Value = id->CreateUID(UID_TYPE_INSTANCE);
	obj->insertElement(el);
	sopInstanceUID = (char*)_bstr_t(el->Value);

	el->Init(rzdcxLib::sopClassUid);
	el->Value = "1.2.840.10008.5.1.4.1.1.7"; // Secondary Capture
	obj->insertElement(el);
	sopClassUID = (char*)_bstr_t(el->Value);

	el->Init(rzdcxLib::Rows);
	el->Value = ROWS;
	obj->insertElement(el);

	el->Init(rzdcxLib::Columns);
	el->Value = COLUMNS;
	obj->insertElement(el);

	el->Init(rzdcxLib::SamplesPerPixel);
	el->Value = SAMPLES_PER_PIXEL;
	obj->insertElement(el);

	el->Init(rzdcxLib::PhotometricInterpretation);
	el->Value = PHOTOMETRIC_INTERPRETATION;
	obj->insertElement(el);

	if (string("RGB") == PHOTOMETRIC_INTERPRETATION)
	{
		el->Init(rzdcxLib::PlanarConfiguration);
		el->Value = 0;
		obj->insertElement(el);
	}

	el->Init(rzdcxLib::BitsAllocated);
	el->Value = BITS_ALLOCATED;
	obj->insertElement(el);

	el->Init(rzdcxLib::BitsStored);
	el->Value = BITS_STORED;
	obj->insertElement(el);

	el->Init(rzdcxLib::HighBit);
	el->Value = BITS_STORED-1;
	obj->insertElement(el);

	el->Init(rzdcxLib::PixelRepresentation);
	el->Value = 0;
	obj->insertElement(el);

	el->Init(rzdcxLib::WindowCenter);
	el->Value = (int)(1<<(BITS_STORED-1));
	obj->insertElement(el);
	
	el->Init(rzdcxLib::WindowWidth);
	el->Value = (int)(1<<BITS_STORED);
	obj->insertElement(el);

	el->Init(rzdcxLib::RescaleIntercept);
	el->Value = (short)RESCALE_INTERCEPT;
	obj->insertElement(el);

	el->Init(rzdcxLib::RescaleSlope);
	el->Value = (short)1;
	obj->insertElement(el);

	if (NUMBER_OF_FRAMES > 1)
	{
		el->Init(rzdcxLib::NumberOfFrames);
		el->Value = (short)NUMBER_OF_FRAMES;
		obj->insertElement(el);
	}

	el->Init(rzdcxLib::GraphicData);
	el->Value = "456\\8934\\39843\\223\\332\\231\\100\\200\\300\\400";
	obj->insertElement(el);

	el->Init(rzdcxLib::PixelData);
	ULONG len = ROWS * COLUMNS * SAMPLES_PER_PIXEL * NUMBER_OF_FRAMES;
		el->Length = len;
	T* pixels=new T[ROWS*COLUMNS*NUMBER_OF_FRAMES];
	for (int n=0; n<NUMBER_OF_FRAMES; n++) {
		for (int y=0; y<ROWS;y++){
			for (int x=0;x<COLUMNS;x++){
				int i=x+COLUMNS*y*n;
				pixels[i]=((i)%(1<<BITS_STORED))-RESCALE_INTERCEPT;
			}
		}
	}
	if (sizeof(T)==1 || sizeof(T) == 3)
		el->ValueRepresentation = VR_CODE_OB;
	SAFEARRAY* newArr;
	pixels2safeArray(pixels, newArr, len);
	variant_t newVar(newArr);
	VariantInit(&newVar);
	newVar.vt = VT_UI1 | VT_ARRAY;
	newVar.parray = newArr;
	el->Value = newVar;
	//el->Value = (unsigned long)pixels;
	obj->insertElement(el);

	/// Save it as is
	obj->saveFile(filename);

	delete[] pixels;

}


string SendFilesAndCommitRequest() 
{
	_bstr_t bstrTransactionUID;
	CoInitialize(NULL);
	try {
		string sopClass1;
		string sopInst1;
		string sopClass2;
		string sopInst2;

		CreateImage<unsigned char>("IMG1", sopClass1, sopInst1);
		CreateImage<unsigned char>("IMG2", sopClass2, sopInst2);
		IDCXREQPtr req(__uuidof(DCXREQ));
		BSTR succceeded_instances(0);
		BSTR failed_instances(0);
		req->Send("BARIIRZDCXCPPEXAMPLE", "DSRSVC", "localhost", 104, "IMG1;IMG2", &succceeded_instances, &failed_instances);
		SysFreeString(succceeded_instances);
		SysFreeString(failed_instances);
		bstrTransactionUID = req->CommitFiles("BARIIRZDCXCPPEXAMPLE", "DSRSVC", "localhost", 104, "IMG1;IMG2");
	} catch (_com_error& e) {
		printf("%s\n", _bstr_t(e.Description()));
	} catch (...) {
		printf("Unknown Error\n");
	}
	CoUninitialize();
	return (char*)bstrTransactionUID;
}


/// \example CPPStorageCommitmentExample.cpp C++ Storage Commit Test Application.
/// This application creates test images and then send them and commits.
/// It has 2 cases:
/// 1) send two files and try to commit them
/// 2) Try to commit two files that were sent in 1 and one instance that wasn't sent.
///    This should result in error.
int main(int argc, char* argv[])
{
	// Start a thread to handle the result of the Commit.
	// This result comes as N-EVENT-REPORT on an association started by the peer.
	AssocThread thread;
    HANDLE hEvent = thread.createAndGetEvent();
	thread.start();
	SendFilesAndCommitRequest();
    WaitForSingleObject(hEvent, INFINITE);
	/// Let the association end nicely
	Sleep(1000);
    CloseHandle(hEvent);
    
	return 0;
}
