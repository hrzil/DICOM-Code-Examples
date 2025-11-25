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
/// \section CPPLowMemCompressionExample C++ Creating DICOM Image Example
/// C++ Creating DICOM Image Example
/// This example shows how to use the DCXOBJ (DicomObject) in the
/// rzdcx.dll to create a DICOM Image with Pixel data

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>
#include <fstream>

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

/// \example CPPLowMemCompressionExample.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// load, save, create and modify DICOM Images.
int main(int argc, char* argv[])
{
    /// Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (SUCCEEDED(hr)) 
	{
		int NUMBER_OF_FRAMES = 200;

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

		el->Init(rzdcxLib::sopClassUid);
		el->Value = "1.2.840.10008.5.1.4.1.1.7"; // Secondary Capture
		obj->insertElement(el);

		string filenames("");
		for (int n=0; n<NUMBER_OF_FRAMES; n++)
			filenames+="gray.bmp;";

		obj->SetBMPFrames(filenames.c_str());

		obj->SaveAs("multiframe_lossless_jpeg_200_frames.dcm", TS_LOSSLESS_JPEG_DEFAULT, 100, ".\\workDir");
	}

	::CoUninitialize();

	return 0; 
}

