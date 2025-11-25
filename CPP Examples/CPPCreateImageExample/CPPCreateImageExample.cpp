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
/// \section CPPCreateImageExample C++ Creating DICOM Image Example
/// C++ Creating DICOM Image Example
/// This example shows how to use the DCXOBJ (DicomObject) in the
/// rzdcx.dll to create a DICOM Image with Pixel data

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

static int ROWS=512;
static int COLUMNS=512;
static int BITS_ALLOCATED=12;
static int BITS_STORED=16;
static int RESCALE_INTERCEPT=0;
static int SAMPLES_PER_PIXEL=1;
static int NUMBER_OF_FRAMES=1;
static char* PHOTOMETRIC_INTERPRETATION="MONOCHROME2";

static void compress(char* filename, IDCXOBJPtr obj)
{
	string BEEfname(filename);
	BEEfname += ".bee.dcm";
	obj->TransferSyntax = TS_BEE;
	obj->saveFile(BEEfname.c_str());

	string LEEfname(filename);
	obj->TransferSyntax = TS_LEE;
	LEEfname += ".lee.dcm";
	obj->saveFile(LEEfname.c_str());

	string LEIfname(filename);
	obj->TransferSyntax = TS_LEI;
	LEIfname += ".lei.dcm";
	obj->saveFile(LEIfname.c_str());

	/// Encode as JPEG lossless
	obj->EncodeLosslessJpeg();
	string jfname1(filename);
	jfname1 += ".jpeglossless.dcm";
	obj->saveFile(jfname1.c_str());

	/// Encode as JPEG lossy 0 quality
	obj->EncodeJpeg(0);
	string jfname4(filename);
	jfname4 += ".jpeglossy_0.dcm";
	obj->saveFile(jfname4.c_str());

	/// Encode as JPEG lossy 100 (best) quality
	obj->EncodeJpeg(100);
	string jfname2(filename);
	jfname2 += ".jpeglossy_100.dcm";
	obj->saveFile(jfname2.c_str());

	/// Encode as JPEG lossy 50 quality
	obj->EncodeJpeg(50);
	string jfname3(filename);
	jfname3 += ".jpeglossy_50.dcm";
	obj->saveFile(jfname3.c_str());

	/// Encode as JPEG2000 lossless
	obj->EncodeJpeg2000(1);
	string j2kfname1(filename);
	j2kfname1 += ".jpeg2000_lossless.dcm";
	obj->saveFile(j2kfname1.c_str());

	/// Encode as JPEG2000 lossy
	obj->EncodeJpeg2000(1000);
	string j2kfname2(filename);
	j2kfname2 += ".jpeg2000_lossy.dcm";
	obj->saveFile(j2kfname2.c_str());

	if (string("RGB") == PHOTOMETRIC_INTERPRETATION)
	{
		obj->openFile(filename);
		obj->MakePaletteColor();
		string pal(filename);
		pal += ".palette.dcm";
		obj->saveFile(pal.c_str());
	}
}

static void LoadDicomImage(char* filename)
{
	/// Create a DCXOBJ
	IDCXOBJPtr obj(__uuidof(DCXOBJ));

	/// Open the file
	obj->openFile(filename);

	/// compress the image and save
	compress(filename, obj);
}
template<typename T>
static void pixels2safeArray(/*[In]*/T* pArr, /*[Out]*/SAFEARRAY*& sArr, /*[In]*/ULONG size) {
	HRESULT hr = S_OK;
	UINT limit = size;
	if (sizeof(T) == 1 || sizeof(T) == 3) {
		sArr = SafeArrayCreateVector(VT_UI1, 0, size);
		limit = size / sizeof(T);
	}
	else
		sArr = SafeArrayCreateVector(VT_UI2, 0, size);
	T* pdata;
	hr = SafeArrayAccessData(sArr, (void**)&pdata);
	if (SUCCEEDED(hr)) {
		for (UINT i = 0; i < limit; i++) {
			pdata[i] = pArr[i];
		}
	}
	hr = SafeArrayUnaccessData(sArr);
}

template<typename T>
static void CreateImage(char* filename)
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
    el->PutCStringPtr((__int3264)pn);
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

	int len = ROWS * COLUMNS * sizeof(T) * NUMBER_OF_FRAMES;
	el->Init(rzdcxLib::PixelData);
	el->Length = len;
	T* pixels=new T[ROWS*COLUMNS*NUMBER_OF_FRAMES];
	for (int n=0; n<NUMBER_OF_FRAMES; n++) {
		for (int y=0; y<ROWS;y++){
			for (int x=0;x<COLUMNS;x++){
				int i=x+COLUMNS*y*n;
				//Insert arbitary values
				pixels[i]=((i)%(1<<BITS_STORED))-RESCALE_INTERCEPT;
			}
		}
	}

	//VARIANT newVar;
	//VariantInit(&newVar);

	if (sizeof(T) == 1 || sizeof(T) == 3) //{
		el->ValueRepresentation = VR_CODE_OB;
/*		newVar.vt = VT_UI1 | VT_ARRAY;
	}
	else
		newVar.vt = VT_UI2 | VT_ARRAY;

	SAFEARRAY* newArr;
	pixels2safeArray(pixels, newArr, len);
	
	newVar.parray = newArr;
	el->Value = newVar;
	*/
	el->SetRawData((int)pixels, len);
	obj->insertElement(el);

	/// Save it as is
	obj->saveFile(filename);

	delete[] pixels;

	/// compress the image and save
	compress(filename, obj);
}

struct ColorPixel {
	char r;
	char g;
	char b;
	const ColorPixel& operator=(int value) {
		r = ((value+0)%3)*127;
		g = ((value+1)%3)*127;
		b = ((value+2)%3)*127;
		return *this;
	}
};

/// \example CPPCreateImageExample.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// load, save, create and modify DICOM Images.
int main(int argc, char* argv[])
{
    /// Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (!SUCCEEDED(hr)) {
        ::CoUninitialize();
        return 1;
    }

	if (argc == 2) // load the image, if a path is given
	{
		LoadDicomImage(argv[1]);
	}
	else
	{ // if a path is not given, create the images manually
		/// RGB Color Image
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 8;
		BITS_STORED = 8;
		RESCALE_INTERCEPT = 0;
		SAMPLES_PER_PIXEL = 3;
		PHOTOMETRIC_INTERPRETATION = "RGB";
		CreateImage<ColorPixel>("color.dcm");

		/// Monochrome 16 bit image 512x512 pixesl
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 16;
		BITS_STORED = 16;
		RESCALE_INTERCEPT = 0;
		SAMPLES_PER_PIXEL = 1;
		PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";
		CreateImage<unsigned short>("gray16.dcm");

		/// Monochrome 16 bit image with rescale intercept
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 16;
		BITS_STORED = 16;
		RESCALE_INTERCEPT = -1024;
		SAMPLES_PER_PIXEL = 1;
		PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";
		CreateImage<unsigned short>("gray16r-1024.dcm");

		/// Monochrome 12 bit image
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 16;
		BITS_STORED = 12;
		RESCALE_INTERCEPT = 0;
		SAMPLES_PER_PIXEL = 1;
		PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";
		CreateImage<unsigned short>("gray12.dcm");

		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 16;
		BITS_STORED = 12;
		RESCALE_INTERCEPT = -1024;
		SAMPLES_PER_PIXEL = 1;
		PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";
		CreateImage<unsigned short>("gray12r-1024.dcm");

		/// Monochrome 8 bit image
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 8;
		BITS_STORED = 8;
		RESCALE_INTERCEPT = 0;
		SAMPLES_PER_PIXEL = 1;
		PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";
		CreateImage<unsigned char>("gray8.dcm");

		/// RGB Color multi-frame image
		ROWS = 512;
		COLUMNS = 512;
		BITS_ALLOCATED = 8;
		BITS_STORED = 8;
		RESCALE_INTERCEPT = 0;
		SAMPLES_PER_PIXEL = 3;
		PHOTOMETRIC_INTERPRETATION = "RGB";
		NUMBER_OF_FRAMES = 30;
		CreateImage<ColorPixel>("color_30_frames.dcm");
	}

	::CoUninitialize();

	return 0; 
}

