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

// Sink.cpp : implementation file
//

#include "stdafx.h"
#include "Sink.h"
#include "AssocThread.h"

// CSink

IMPLEMENT_DYNAMIC(CSink, CCmdTarget)


CSink::CSink(AssocThread* inAssocThread)
{
	EnableAutomation();
	_assocThread = inAssocThread;
}

CSink::~CSink()
{
}


void CSink::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

	CCmdTarget::OnFinalRelease();
}


BEGIN_MESSAGE_MAP(CSink, CCmdTarget)
END_MESSAGE_MAP()


BEGIN_DISPATCH_MAP(CSink, CCmdTarget)
	DISP_FUNCTION(CSink, "OnCommitResult", OnCommitResult, VT_I4, VTS_BOOL VTS_BSTR VTS_BSTR VTS_BSTR)
    DISP_FUNCTION(CSink, "OnConnection", OnConnection, VT_I4, VTS_BSTR VTS_BSTR VTS_BSTR VTS_PBOOL)
	DISP_FUNCTION(CSink, "OnStoreSetup", OnStoreSetup, VT_I4, VTS_PBSTR)
	DISP_FUNCTION(CSink, "OnStoreDone", OnStoreDone, VT_I4, VTS_BSTR VTS_BOOL VTS_PBOOL)
	DISP_FUNCTION(CSink, "OnTimeout", OnTimeout, VT_I4, VTS_NONE)
END_DISPATCH_MAP()

// Note: we add support for IID_ISink to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .IDL file.

// {4AF0BAFE-5575-4381-9CC9-59A0156D65B2}
static const IID IID_ISink =
{ 0x4AF0BAFE, 0x5575, 0x4381, { 0x9C, 0xC9, 0x59, 0xA0, 0x15, 0x6D, 0x65, 0xB2 } };

BEGIN_INTERFACE_MAP(CSink, CCmdTarget)
	INTERFACE_PART(CSink, __uuidof(IDCXACCEvents), Dispatch)
END_INTERFACE_MAP()


// CSink message handlers
HRESULT CSink::OnCommitResult(VARIANT_BOOL status, BSTR transactionUID, BSTR succeededInstances, BSTR failedInstances)
{
	USES_CONVERSION;
	std::string tid   = T2A((LPTSTR)transactionUID);
	std::string slist = T2A((LPTSTR)succeededInstances);
	std::string flist = T2A((LPTSTR)failedInstances);
	return _assocThread->OnCommitResult((status == VARIANT_TRUE), tid, slist, flist);
}

HRESULT CSink::OnConnection(BSTR calling, BSTR called, BSTR host, VARIANT_BOOL *accept_connection)
{
	VARIANT_BOOL response = -1;
	USES_CONVERSION;
	char* callingAE = T2A((LPTSTR)calling);
	char* calledAE = T2A((LPTSTR)called);
	char* h = T2A((LPTSTR)host);

	printf("%s, %s, %s", callingAE, calledAE, h);

	*accept_connection = response;
	return 0;
}

HRESULT CSink::OnStoreSetup(BSTR *out)
{
    static int filecount=0;
    char fname[20];
    sprintf_s<20>(fname, ".\\dcm%04d.dcm", ++filecount);
	USES_CONVERSION;
    *out = A2BSTR(fname);
	return 0;
}

HRESULT CSink::OnStoreDone(BSTR filename,
				VARIANT_BOOL storage_status,
				VARIANT_BOOL *accept_storage)
{
	USES_CONVERSION;
	char* fname = T2A((LPTSTR)filename);
	
	VARIANT_BOOL b = -1;
	accept_storage = &b;
	printf("%s", fname);
	return 0;
}

HRESULT CSink::OnTimeout()
{	
	return 0;
}

IDCXOBJIterator* CSink::OnFind(IDCXOBJ* query)
{
	query->Dump("find.txt");
	IDCXOBJIteratorPtr i(__uuidof(DCXOBJIterator));
	IDCXELMPtr e(__uuidof(DCXELM));
	e->Init(0x100010); // Patien Name
	e->Value = "Patient A";
	query->insertElement(e);
	e->Init(patientID);
	e->Value = "PID1";
	query->insertElement(e);
	i->Insert(query);

	/*e->Init(0x100010); // Patien Name
	e->Value = "Patient B";
	query->insertElement(e);
	e->Init(patientID);
	e->Value = "PID2";
	query->insertElement(e);
	i->Insert(query);

	e->Init(0x100010); // Patien Name
	e->Value = "Patient C";
	query->insertElement(e);
	e->Init(patientID);
	e->Value = "PID3";
	query->insertElement(e);
	i->Insert(query);*/
	return i.Detach();
}

