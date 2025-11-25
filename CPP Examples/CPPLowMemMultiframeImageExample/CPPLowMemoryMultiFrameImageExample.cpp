/*
 * Copyright (C) 2005, Roni Zaharia
 */

/// \page CPPTestApplications C++ Test Applications
/// The C++ test applications can be downloaded from www.roniza.com/downloads
/// \section CPPLowMemoryMultiFrameImageExample C++ Low Memory Multiframe Image Creation Example
/// This example shows how to create a large DICOM object that holds many frames (multi-frame).
/// The original frames are simple pixel data files.
/// The toolkit is used to compress them as JPEG frames and then pack them into a single DICOM object.
/// When running this example, open the performance monitor or task manager and observe that the
/// memory consumption stays constant and is proportional to the size of a single frame.

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
static const char* PHOTOMETRIC_INTERPRETATION="MONOCHROME2";
static const char* PIXELSFILE="pixels.dat";
template<typename T>
static void CreateImage(const char* filename, bool useTempFile=false)
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

	el->Init(rzdcxLib::PixelData);
	el->Length = ROWS*COLUMNS*SAMPLES_PER_PIXEL*NUMBER_OF_FRAMES;
	T* pixels=new T[ROWS*COLUMNS* ( useTempFile ? 1 : NUMBER_OF_FRAMES ) ];
	
	if (sizeof(T)==1 || sizeof(T) == 3)
		el->ValueRepresentation = VR_CODE_OB;

	if (useTempFile)
	{
		FILE* f(0);
		fopen_s(&f,PIXELSFILE, "wb");
		for (int n=0; n<NUMBER_OF_FRAMES; n++) {
			for (int y=0; y<ROWS;y++){
				for (int x=0;x<COLUMNS;x++){
					int i=x+COLUMNS*y;
					pixels[i]=((i)%(1<<BITS_STORED))-RESCALE_INTERCEPT;
				}
			}
			fwrite(pixels, sizeof(T), ROWS*COLUMNS, f);
		}
		fclose(f);
		el->SetValueFromFile(PIXELSFILE, 0, ROWS*COLUMNS*NUMBER_OF_FRAMES*sizeof(T));
	}
	else
	{
		for (int n=0; n<NUMBER_OF_FRAMES; n++) {
			for (int y=0; y<ROWS;y++){
				for (int x=0;x<COLUMNS;x++){
					int i=x+COLUMNS*y*n;
					pixels[i]=((i)%(1<<BITS_STORED))-RESCALE_INTERCEPT;
				}
			}
		}
		el->Value = (unsigned long)pixels;
	}

	obj->insertElement(el);

	delete[] pixels;

	IDCXAPPPtr dcxapp(__uuidof(DCXAPP));

	obj->SaveAs(filename, rzdcxLib::TS_JPEG, 50, ".\\workDir");
	
	if (useTempFile)
		DeleteFileA(PIXELSFILE);

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

/// \example CPPLowMemoryMultiFrameImageExample.cpp
int main(int argc, char* argv[])
{
    /// Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (!SUCCEEDED(hr)) {
        ::CoUninitialize();
        return 1;
    }

	/// RGB Color multi-frame image
	ROWS=1020;
	COLUMNS=818;
	BITS_ALLOCATED=8;
	BITS_STORED=8;
	RESCALE_INTERCEPT=0;
	SAMPLES_PER_PIXEL=3;
	PHOTOMETRIC_INTERPRETATION="RGB";
	NUMBER_OF_FRAMES = 5;

	{
		IDCXAPPPtr app(__uuidof(DCXAPP));
		app->NewInstanceUIDPolicy = CNU_NEVER;
		CreateImage<ColorPixel>("low_mem_jpeg.dcm", true);
		app->NewInstanceUIDPolicy = CNU_ALWAYS;
		IDCXOBJPtr obj(__uuidof(DCXOBJ));
		obj->openFile("low_mem_jpeg.dcm");
		obj->Decode();
		obj->saveFile("low_mem_decoded.dcm");
	}

	::CoUninitialize();

	return 0; 
}

