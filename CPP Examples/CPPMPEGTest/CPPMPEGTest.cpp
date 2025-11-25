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
/// \section CPPMPEGTest C++ Creating DICOM Video Example
/// C++ Creating DICOM Video Example
/// This example shows how to use the DCXOBJ (DicomObject) in the
/// rzdcx.dll to encapsulate MPEG video in DICOM file

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

#include <sys/stat.h>
#include <stdio.h>

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>

using namespace rzdcxLib;
using namespace std;

static unsigned long getFileSize(const char* path)
{
	int status;
	unsigned long size;
	struct stat im_stat;

	status = stat(path, &im_stat);
	if (status < 0) {
		size = 0;
		return 0;
	}
	else {
		return im_stat.st_size;
	}
}

void SendFile(const string& filename)
{
	IDCXREQPtr req(__uuidof(DCXREQ));

	BSTR succeededFilesList = NULL;
	BSTR failedFilesList = NULL;

	req->Send("RZDCX", "DICOMIZER", "localhost", 104, filename.c_str(), &succeededFilesList, &failedFilesList);
}

static void CreateVideo(string& filename, string& outfilename)
{
	/// Create a DCXOBJ
	IDCXOBJPtr obj(__uuidof(DCXOBJ));

	/// Create an element pointer to place in the object for every tag
	IDCXELMPtr el(__uuidof(DCXELM));

	IDCXUIDPtr id(__uuidof(DCXUID));

	rzdcxLib::ENCAPSULATED_VIDEO_PROPS videoProps;
	videoProps.width = 352;
	videoProps.Height = 288;
	videoProps.PixelAspectRatioX = 4;
	videoProps.PixelAspectRatioY = 3;
	videoProps.FrameDurationMiliSec; // 40 msec = 25 FPS
	videoProps.NumberOfFrames = 1600; // 1600 frames
	videoProps.VideoFormat = rzdcxLib::MPEG2_AT_MAIN_LEVEL;
	obj->SetVideoStream(filename.c_str(), videoProps);
	/// You don't have to create an element every time, 
	/// just initialize it.
	char pn[] = "John^Doe";
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
	el->Value = "1.2.840.10008.5.1.4.1.1.77.1.4.1"; // Video Photographic Image Storage
	obj->insertElement(el);

	el->Init(rzdcxLib::NumberOfFrames);
	el->Value = (short)1600;
	obj->insertElement(el);

	el->Init(rzdcxLib::FrameIncrementPointer);
	el->Value = rzdcxLib::FrameTime;
	obj->insertElement(el);

	obj->saveFile(outfilename.c_str());
}

bool FileExists(LPCWSTR filename)
{
	return (0xffffffff != GetFileAttributes(filename));
}

/// \example CPPMPEGTest.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// to encapsulate MPEG video in DICOM file.
int main(int argc, char* argv[])
{
	if (argc < 2)
		return -1;

    /// Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (!SUCCEEDED(hr)) {
        ::CoUninitialize();
        return 1;
    }

	{
		string infilename(argv[1]);
		string outfilename(argv[1]);
		outfilename += ".dcm";

		IDCXAPPPtr app(__uuidof(DCXAPP));
		app->LogLevel = LOG_LEVEL_DEBUG;
		app->StartLogging("rzdcx.log");

		CreateVideo(infilename, outfilename);

		SendFile(outfilename);

		app->StopLogging();
	}

	::CoUninitialize();

	return 0; 
}

