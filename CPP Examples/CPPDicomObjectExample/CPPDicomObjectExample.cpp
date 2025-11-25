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
/// \section CPPDicomObjectDemo C++ Dicom Object Demo
/// C++ Dicom Object Demo
/// This example shows how to use the DCXOBJ (DicomObject) in the
/// rzdcx.dll. The demo app adds several tags to the DCXOBJ object.
/// Retrieves the tags from the object and prints them.
/// This example also shows how to create private tags using the dicom.dic file.

#include <stdio.h>
#include <time.h>
#include <string>
#include <list>

// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")
using namespace rzdcxLib;
using namespace std;

static char some_binary_blob_data[1024];

template <typename T>
void ReadArray1D(VARIANT& pVariant, T* arraySrc, long& length)
{
	if (pVariant.vt | VT_ARRAY)
	{
		T* dwArray = NULL;
		SafeArrayAccessData(pVariant.parray, (void**)&dwArray);

		for (int i = 0; i < length; i++)
		{
			arraySrc[i] = dwArray[i];
		}

		SafeArrayUnaccessData(pVariant.parray);
	}
}

// This function reads the file that was created in WriteTest and then deletes the file
void ReadTest()
{
    printf("Printing Dicom Object...\n");

    _variant_t ret;

    // Create a DCXOBJ
    IDCXOBJPtr obj(__uuidof(DCXOBJ));

    obj->openFile(".\\test.dcm");

    // You need to hold a IDCXELMPtr in order to get the elements
    IDCXELMPtr elem;
    elem = obj->getElementByTag(0x00100010);        //Patient's Name
    if (elem != NULL)
    {
        ret = elem->Value;
    }

    string name = static_cast<_bstr_t>(ret);
    printf("Patient name: %s\n", name.c_str());

    elem = obj->getElementByTag(0x00100020);        //Patient ID
    if (elem != NULL)
    {
        ret = elem->Value;
    }

    string id = static_cast<_bstr_t>(ret);
    printf("Patient ID: %s\n", id.c_str());

    elem = obj->getElementByTag(0x00200010);        //Study ID
    if (elem != NULL)
    {
        ret = elem->Value;
    }

    string sid = static_cast<_bstr_t>(ret);
    printf("Study ID: %s\n", sid.c_str());

    elem = obj->getElementByTag(0x00080020);        //Study Date
    if (elem != NULL)
    {
        ret = elem->Value;
    }

    string sDate = static_cast<_bstr_t>(ret);
    printf("Study Date: %s\n", sDate.c_str());

    elem = obj->getElementByTag(0x0020000e);        //Series Instance UID
    if (elem != NULL)
    {
        ret = elem->Value;
    }

    string siuid = static_cast<_bstr_t>(ret);
    printf("Series Instance UID: %s\n", siuid.c_str());

	// Read some binary blob data
	elem = obj->getElementByTag(0x00282000);        //ICC Profile
	if (elem != NULL)
	{
		ret = elem->Value;
		char* data_ptr = (char*)ret.ulVal;
		if (data_ptr) {
			long len = elem->Length; // Should be 1024
			int res = memcmp(data_ptr, some_binary_blob_data, len); // should be 0
			if (res == 0)
				printf("Binary data matches exactly!\n");
			else
				printf("Binary data doesn't match?\n");
		}
        else
            printf("Binary data doesn't exist!\n");
	}

	/// \page errorhandling Error Handling
	/// \section ierrorinfo Getting Detailed Error Information in C++
	/// The following code snippet uses three different emthods to handle errors.
	/// When using ATL Auto Pointers, any call with Failed HR throws exception that
	/// can be catches with the _com_error class.
	/// \verbatim
	
	// Read an element that doesn't exists - throws exception!
	try {
		elem = obj->getElementByTag(PatientsAge);
	} catch (const _com_error& err) {
		printf("PatientsAge: %s\n", (char*)err.Description());
	}

	// Read same element without throwing an exception
	HRESULT hr = obj->raw_getElementByTag(PatientsAge, &elem);
	// You can test like this
	if (elem == 0)
		printf("PatientsAge not found in object\n"); 
	// Or you can test like this and optionally get the detailed error information
	if ( FAILED(hr) ) 
	{ 
		IErrorInfoPtr eip(elem);
		hr = ::GetErrorInfo(0 /* reserved; must be zero */, &eip); 
		if ( hr == S_OK && eip.GetInterfacePtr() ) 
		{ 
			// Read the description
			_bstr_t desc;
			hr = eip->GetDescription(&desc.GetBSTR());
			printf("Error description: %s\n", (char*)desc);
		} 
	}

	/// \endverbatim

	unsigned short values[4];
	// Read an element with VM > 1
	elem = obj->GetElement(DICOM_TAGS_ENUM::AcquisitionMatrix);
	if (elem->ValueMultiplicity > 1)
	{
		long len = 4;
		ReadArray1D(elem->ValueArray, values, len);
	}
	else
		values[0] = elem->Value;

	obj->Dump(L".\\test.txt");
    DeleteFile(L".\\test.dcm");
}

// This method creates a DICOM Object in memory and saves it to file
void WriteTest()
{
    // Create a DCXOBJ
    IDCXOBJPtr obj(__uuidof(DCXOBJ));

    // Create an element pointer to place in the object for every tag
    IDCXELMPtr el(__uuidof(DCXELM));

    printf("Filling Dicom Object...\n");

    // You don't have to create an element every time, 
    // just initialize it.
    el->Init(rzdcxLib::PatientsName);   //Patient's Name
    el->Value = "John";
    // insert the element to the object
    obj->insertElement(el);

    el->Init(0x00100020);               //Patient ID
    el->Value = "123765";
    obj->insertElement(el);

    el->Init(0x00200010);               //Study ID
    el->Value = "234523";
    obj->insertElement(el);

    el->Init(0x00080020);               //Study Date
    el->Value = "20080103";
    obj->insertElement(el);

    el->Init(0x0020000e);               //Series Instance UID
    el->Value = "2346234512345245";
    obj->insertElement(el);

	// Set some binary blob data 
	el->Init(0x00282000);               // ICC Profile - VR is OB
	//el->Length = sizeof(some_binary_blob_data);
	//el->Value = /*(unsigned long)*/(byte)some_binary_blob_data;
    el->SetRawData((unsigned long)some_binary_blob_data, sizeof(some_binary_blob_data));
	obj->insertElement(el);

	el->Length = 0; // Bug until 2.0.7.4 including. Call to Init doesn't reset Length

	// Add private tags:
	el->Init(0x00270010);
	el->Value = "RZDCX";
	obj->insertElement(el);

	el->Init(0x00271001);
	el->Value = 1234;
	obj->insertElement(el);

	el->Init(0x00271002);
	el->Value = "CODE_STRING_1";
	obj->insertElement(el);

	el->Init(DICOM_TAGS_ENUM::AcquisitionMatrix);
	el->Value = "1\\4\\2\\3";
	obj->insertElement(el);

    // save it to disk
    obj->saveFile(".\\test.dcm");
}

#ifndef UNIT_TESTING

/// \example CPPDicomObjectExample.cpp
/// This is an example of how to use the IDCXELM interface and the IDCXOBJ interface to
/// load, save, create and modify DICOM Objects.
int main(int argc, char* argv[])
{

	// Initialize some binary blob data
	for (int i=0; i<sizeof(some_binary_blob_data); i++)
		some_binary_blob_data[i] = (char)(i);

    // Initialize the dll (has to be performed in every thread)
    HRESULT hr = ::CoInitialize(NULL); 

    if (!SUCCEEDED(hr)) {
        ::CoUninitialize();
        return 1;
    }

    WriteTest();
    
    ReadTest();

   	printf("Dicom Object example ended\n");

    ::CoUninitialize();
    return 0; 
}

#endif