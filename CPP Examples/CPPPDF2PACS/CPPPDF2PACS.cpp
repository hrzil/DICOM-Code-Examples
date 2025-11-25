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
/// \section CPPPDF2PACS C++ Demo Converting PDF to DICOM and send to PACS
/// This example shows how to create DICOM file incapsulating PDF and send it to PACS

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
#include <sys/stat.h>
#include <stdio.h>

using namespace rzdcxLib;

static unsigned long getFileSize(const char* path)
{
  int status;
  unsigned long size;
  struct stat im_stat;

  status = stat(path, &im_stat);
  if (status < 0) {
    size = 0;
    return 0;
  } else {
    return im_stat.st_size;
  }
}

static void pdf2dcm(const char* pdfFile, 
					const char* dicomFile,
					const char *patientsName,
					const char *patientId,
					const char *patientsBirthdate,
					const char *patientsSex)
{
    // insert empty type 2 attributes
	IDCXOBJPtr o(__uuidof(DCXOBJ));
	IDCXELMPtr e(__uuidof(DCXELM));
	IDCXUIDPtr u(__uuidof(DCXUID));

	e->Init(StudyDate				); o->insertElement(e);
	e->Init(StudyTime				); o->insertElement(e);
	e->Init(AccessionNumber		); o->insertElement(e);
	e->Init(Manufacturer			); o->insertElement(e);
	e->Init(ReferringPhysicianName	); o->insertElement(e);
	e->Init(StudyID				); o->insertElement(e);
	e->Init(ContentDate			); o->insertElement(e);
	e->Init(ContentTime			); o->insertElement(e);
	e->Init(AcquisitionDatetime	); o->insertElement(e);
	
	e->Init(ConceptNameCodeSequence); o->insertElement(e);

    // insert const value attributes
	e->Init(SpecificCharacterSet			); e->Value =  "ISO_IR 100";					o->insertElement(e);
	e->Init(sopClassUid	); e->Value =  "1.2.840.10008.5.1.4.1.1.104.1"; o->insertElement(e); // Encapsulated PDF
	e->Init(Modality						); e->Value =  "OT";							o->insertElement(e);
	e->Init(ConversionType					); e->Value =  "WSD";							o->insertElement(e);
	e->Init(MIMETypeOfEncapsulatedDocument	); e->Value =  "application/pdf";				o->insertElement(e);
	e->Init(BurnedInAnnotation				); e->Value =  "YES";							o->insertElement(e);
	e->Init(SeriesNumber					); e->Value =  "1";								o->insertElement(e);
	e->Init(InstanceNumber					); e->Value =  1;								o->insertElement(e);	
	e->Init(DocumentTitle					); e->Value =  "Example PDF Report";			o->insertElement(e);
	
	// Insert parameters
	e->Init(PatientsName      ); if (patientName) e->Value =  patientsName;				o->insertElement(e);
	e->Init(patientID         ); if (patientId)   e->Value =  patientId;				o->insertElement(e);
	e->Init(PatientBirthDate  ); if (patientsBirthdate) e->Value = patientsBirthdate;	o->insertElement(e);
	e->Init(PatientSex        ); if (patientsSex) e->Value =  patientsSex;				o->insertElement(e);

	// Create new UID's for the object
	e->Init(studyInstanceUID); e->Value = u->CreateUID(UID_TYPE_STUDY);o->insertElement(e);
	e->Init(seriesInstanceUID); e->Value = u->CreateUID(UID_TYPE_SERIES);o->insertElement(e);
	e->Init(sopInstanceUID); e->Value = u->CreateUID(UID_TYPE_INSTANCE);o->insertElement(e);
	
	SYSTEMTIME t;
	GetSystemTime(&t);
	double vt;
	SystemTimeToVariantTime(&t, &vt);
	e->Init(InstanceCreationDate); e->Value = _variant_t(vt, VT_DATE); o->insertElement(e);
	e->Init(InstanceCreationTime); e->Value = _variant_t(vt, VT_DATE); o->insertElement(e);

	unsigned long fsize = getFileSize(pdfFile);
	//if (fsize % 2)	// size must be even!
	//	fsize--;
	e->Init(EncapsulatedDocument); 	e->SetValueFromFile(pdfFile, 0, fsize); o->insertElement(e);
	o->saveFile(dicomFile);
}

/// \example CPPPDF2PACS.cpp
/// This example shows how to create DICOM file incapsulating PDF
/// Arguments list:
/// 1) PDF File name to read
/// 2) DICOM File to save
int main(int argc, char* argv[])
{
	HRESULT hr = CoInitialize(NULL);

	if (SUCCEEDED(hr))
	{
		// Convert 2 DICOM
		pdf2dcm(argv[1], argv[2], "Doe^John", "999999999", "", "O" /* or "F" or "M" */);
		
		// Send 2 PACS
		IDCXREQPtr req(__uuidof(DCXREQ));
		BSTR succeeded = NULL;// L"";
		BSTR failed = NULL;//L"";
		req->Send(/*"RZDCXTEST"*/"BARIIRZDCXCPPEXAMPLE", "DSRSVC", "localhost", 104, argv[2], &succeeded, &failed);
		SysFreeString(succeeded);
		SysFreeString(failed);
	}
	CoUninitialize();
}

