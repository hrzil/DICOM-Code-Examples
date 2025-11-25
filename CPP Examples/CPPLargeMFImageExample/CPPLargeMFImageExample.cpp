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
/// \section CPPLargeMFImageExample C++ Demo creating a very long multi-frame image file on low memory computer
/// This example shows how to create a very big DICOM file holding 200 frames 1020 x 818 pixels each
/// while using only a fragment of memory. Later on the file is compressed using lossless jpeg compression
/// that works frame by frame without loading the complete sequence into memory

/// This example use the following methods:
/// - IDCXELM::SetValueFromfile to set the long pixels value from a file
/// - IDCXOBJ::SaveAs to compress a DICOM file using temporary files for each frame

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>
#include <fstream>

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

/// A simple digits to image tool
#include "DigiTools.h"

/// \example CPPLargeMFImageExample.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// load, save, create and modify DICOM Images.
int main(int argc, char* argv[])
{
	//char filename[] = /*"color.dcm"; */"..\\..\\..\\CPPLargeMFImageExample\\color.uncompressed.dcm";
	//if (argc == 2)
	//	strcpy(filename, argv[1]);
	// If any of the files of this example exists, delete it first
	if (true)
	{
		::DeleteFileA("pixel.data");
		::DeleteFileA("color.uncompressed.dcm");
		::DeleteFileA("Color.jpegLossless.dcm");
		::DeleteFileA("Color.jpeg.dcm");
		::DeleteFileA("extracted.data");
	}


	// Setting the image pixel group elements
	int ROWS = 1024;
	int COLUMNS = 1024;
	int NUMBER_OF_FRAMES = 1000;
	int BITS_ALLOCATED = 8;
	int BITS_STORED = 8;
	int RESCALE_INTERCEPT = 0;
	int SAMPLES_PER_PIXEL = 3;
	char* PHOTOMETRIC_INTERPRETATION = "RGB";
	int frameSize = ROWS*COLUMNS*SAMPLES_PER_PIXEL;

	// Create a dummy pixels frame

	// Write it to a file
	if (true) 
	{
		char *pixels = new char[frameSize];
		ofstream s("pixel.data", ios_base::binary);
		for (int i = 0; i < NUMBER_OF_FRAMES; i++)
		{
			number2image(i + 1);
			scaleImageTo(COLUMNS, ROWS, pixels);
			s.write(pixels, frameSize);
		}
		s.close();
		delete[] pixels;
	}


	if (true)
	{
		/// Initialize the dll (has to be performed in every thread)
		if (SUCCEEDED(::CoInitialize(NULL)))
		{
			/// Create a DICOM Object - DCXOBJ
			IDCXOBJPtr obj(__uuidof(DCXOBJ));

			///Create an element pointer to place in the object for every tag
			IDCXELMPtr el(__uuidof(DCXELM));

			IDCXUIDPtr id(__uuidof(DCXUID));

			// Set the basic elements - patient name and ID, study, series and instance UID's
			// You don't have to create an element every time, 
			// just initialize it.
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
			el->Value = "1.2.840.10008.5.1.4.1.1.7"; // Secondary Capture
			obj->insertElement(el);

			el->Init(rzdcxLib::Modality);
			el->Value = "OT";
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
			el->Value = BITS_STORED - 1;
			obj->insertElement(el);

			el->Init(rzdcxLib::PixelRepresentation);
			el->Value = 0;
			obj->insertElement(el);

			if (NUMBER_OF_FRAMES > 1)
			{
				el->Init(rzdcxLib::NumberOfFrames);
				el->Value = (short)NUMBER_OF_FRAMES;
				obj->insertElement(el);
			}

			el->Init(rzdcxLib::PixelData);
			el->ValueRepresentation = VR_CODE_OB;

			unsigned long pixelsLength = frameSize*NUMBER_OF_FRAMES;
			el->SetValueFromFile("pixel.data", 0, pixelsLength);
			obj->insertElement(el);

			// Save it as is
			obj->saveFile("color.uncompressed.dcm");
		}
		::CoUninitialize();
	}

	// Extract the pixels from the DICOM file

	/// Initialize the dll (has to be performed in every thread)
	if (true)
	{
		if (SUCCEEDED(::CoInitialize(NULL)))
		{
			IDCXOBJPtr obj(__uuidof(DCXOBJ));
			obj->openFile("color.uncompressed.dcm");
			// Compress it as JPEG 
			obj->SaveAs("Color.jpeg.dcm", 
				TS_JPEG, 80, ".\\workDir");

			// Compress it as JPEG Lossless
			obj->SaveAs("Color.jpegLossless.dcm", 
				TS_LOSSLESS_JPEG_DEFAULT, 100, ".\\workDir");

			if (false)
			{
				// Recover the pixel data
				char* lp = (char*)obj->GetPixelDataRef();
				ofstream s("extracted.data", ios_base::binary);
				for (int i = 0; i < NUMBER_OF_FRAMES; i++, lp += frameSize)
				{
					s.write(lp, frameSize);
				}
				s.close();
			}
		}
		::CoUninitialize();
	}

	return 0; 
}

