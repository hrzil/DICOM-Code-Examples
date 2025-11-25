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
/// \section printscudemo C++ DICOM Print SCU Demo
/// This is a test application for the DICOM Print SCU implementation of rzdcx.
/// The application can print a test pattern or a input DICOM file.

#include <stdio.h>
#include <list>
#include <string>
#include <stdlib.h>

// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

static const int rows = 512;
static const int columns = 512;
static unsigned short imagebuf[rows*columns];
static int n=1,m=1;
static bool testImage = false;
static const int Max_Text_Len=100; 
static char annotationText[Max_Text_Len]="";
static char annotationFormat[Max_Text_Len]="";
static bool printAnnotation=false;
static bool printColor=false;
static int buffPtr;

/*! \example CPPPrintSCU.cpp C++ DICOM Print SCU
This application demonstrate using RZDCX to send DICOM Images to a DICOM Printer
Using the DICOM Print Managment Service.
*/
static void usageError()
{
    char msg[] = 
		"Usage: PrintClient "
		"-a <calling-ae-title> "
		"-c <called-ae-title> "
		"-h <host> "
		"-p <port>] "
		"[-l MxN]"
		"[-t] "		
		"[-n  annotationFormatText annotationText]"
		"[-b <color>]"
		"[-o P|L]"
		"filename [filename [...]]"
		"\n\n"
		"-l\tLayout M - rows, N columns\n"
		"-t\tPrint a test 8 bit grayscale image\n"
		"-o\t P[ortrait] or L[andscape]"
		"\n";

    fprintf(stderr, msg);
    exit(1);
}

// Create a simple image pattern to be printed
IDCXPrintImageBoxPtr initImage()
{
	IDCXPrintImageBoxPtr image(__uuidof(DCXPrintImageBox));
	
	// Set a simple diagonal stripes pattern on the image
	unsigned short *p=&imagebuf[rows*columns];
	unsigned short val=0;
	while (p-- > imagebuf) *p=(val++%2) ? 0 : 4095;

	
	image->Rows = rows;
	image->Columns = columns;
	image->BitsAllocated = IBA_16;
	image->BitsStored = IBS_12;
	image->HighBit = IHB_11;
	image->PhotometricInterpretation = IPI_MONOCHROME2;
	image->PixelRepresentation = IPR_UNSIGNED;
	image->SamplesPerPixel = ISPP_1;
	image->ImagePosition = 1;
	image->PixelDataPtr = (int)buffPtr;

	return image;
}

// This is a simple structure
struct DicomConnectionInfo {
	std::string		title;
	string		host;
	unsigned short	port;
	string		myAETitle;
};

static bool parseCmdLine(DicomConnectionInfo &info, list<string> &files, int argc, char *argv[])
{
    while (--argc > 0) {
		if (*(++argv)[0] == '-') {
			switch ((*argv)[1]) {
			case 'a':
				argc--;
				argv++;
				if (!argc)
					usageError();
				info.myAETitle = *argv;
				break;
			case 'c':
				argc--;
				argv++;
				if (!argc)
					usageError();
				info.title = *argv;
				break;
			case 'h':		//* set the forgive option */
				argc--;
				argv++;
				if (!argc)
					usageError();
				info.host = *argv;
				break;
			case 'p':		//* use node as name rather than hostname */
				argc--;
				argv++;
				if (!argc)
					usageError();
				info.port = atoi(*argv);
				break;
			case 'l':
				argc--;
				argv++;
				if (!argc)
					usageError();
				sscanf(*argv,"%dx%d",&m,&n);
				break;
			case 't':
				testImage=true;
				break;
			case 'n':
				argc--;
				argv++;
				if (!argc)
					usageError();
				strcpy(annotationFormat,*argv);
				argc--;
				argv++;
				if (!argc)
					usageError();

				strcpy(annotationText,*argv);
				printAnnotation=true;
				break;
			case 'b':
				printColor=true;
				break;
			default:
				argc--;
				argv++;
				break; // Ignore it
			}
		} else
			files.push_back(*argv);
	}
	return true;
}

int main(int argc, char *argv[])
{
	try {

		// Initialize the default connection info
		DicomConnectionInfo cinfo;
		cinfo.myAETitle = "PRINT-SCU";
		cinfo.title = "PRINTSCP";
		cinfo.host = "localhost";
		cinfo.port = 10006;

		list<string> files;

		// Parse command line arguments
		parseCmdLine(cinfo,files,argc,argv);

		HRESULT hr = ::CoInitialize(NULL);
		if (FAILED(hr))
			return 1;

		IDCXOBJPtr o(__uuidof(DCXOBJ));

        o->openFile(_bstr_t(files.front().c_str()));
        
		buffPtr = o->GetPixelDataRef();

		// Start a session.
		// Based on command line argument (-b) start either color or grayscalse session.
		IDCXREQPtr requester(__uuidof(DCXREQ));

		IDCXOBJPtr printerStatus = requester->GetPrinterInfo(_bstr_t(cinfo.myAETitle.c_str()), _bstr_t(cinfo.title.c_str()), _bstr_t(cinfo.host.c_str()), cinfo.port);
		IDCXELMIteratorPtr it = printerStatus->iterator();
		for (; !it->AtEnd(); it->Next())
		{
			IDCXELMPtr e = it->Get();
			string val = static_cast<_bstr_t>(e->Value);
			printf("Tag: %08x\t", e->Tag);
			printf("Value: %s\n", val.c_str());		
		}

		IDCXPrintSessionPtr session;
		if(printColor)
			session = requester->StartColorPrintSession(_bstr_t(cinfo.myAETitle.c_str()), _bstr_t(cinfo.title.c_str()), _bstr_t(cinfo.host.c_str()), cinfo.port);
		else
			session = requester->StartGrayscalePrintSession(_bstr_t(cinfo.myAETitle.c_str()), _bstr_t(cinfo.title.c_str()), _bstr_t(cinfo.host.c_str()), cinfo.port);

		// Try C-ECHO 

		// Define a Page (Film Box)
		IDCXPrintPagePtr page(__uuidof(DCXPrintPage));

		// Set protrait layout
		page->Orientation = PPO_PORTRAIT;

		// Set page layout
		char s[60];

		sprintf(s, "STANDARD\\%d,%d", m, n);
		page->StandardLayout = _bstr_t(s);

		// Use object to set any additional attribues (or override)
		IDCXOBJPtr page_attributes(__uuidof(DCXOBJ));
		if(printAnnotation)
		{
			IDCXELMPtr element(page_attributes->createElement(0x20100030)); /// Annotations Display Format ID
			element->Value = annotationFormat;
			page_attributes->insertElement(element);
		}

		// Create the page.
		session->CreatePage(page);
		//or session->CreatePageEx(page, page_attributes);

		// Case I: Send test image (8 bit)
		IDCXPrintImageBoxPtr image = initImage();

		for (int i = 1; i <=n*m; i++) {
			page->SetImageBox(i,image);
			// or page->SetImageFromObject(i,object)
			// or page->SetImageFromFile(i,object)
		}

		page->Print();
		session->DeletePage(page);
		requester->EndPrintSession();

		::CoUninitialize;

	} catch (_com_error& err) {
		printf("%s", (char*)(err.Description()));
	}
	return 0;
}

