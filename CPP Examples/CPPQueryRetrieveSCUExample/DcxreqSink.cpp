/*
 * 
 * Copyright (c) 2015-2017, H.R.Z. SOftware Services LTD
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


// DcxreqSink.cpp : implementation file
//

#include "stdafx.h"
#include "DcxreqSink.h"

using namespace rzdcxLib;
using namespace std;


// DcxreqSink

IMPLEMENT_DYNAMIC(DcxreqSink, CCmdTarget)


DcxreqSink::DcxreqSink()
	: pHandler(0)
	, dwCookie(0)
{
	EnableAutomation();
}

void DcxreqSink::Connect(IDCXREQ* req)
{
	ptrConPntCnt = (IDispatch*)req;
	pHandler = NULL;
	if (ptrConPntCnt != NULL)
	{
		HRESULT hr = ptrConPntCnt->FindConnectionPoint(__uuidof(IDCXREQEvents), &ptrCon);
		if (SUCCEEDED(hr))
			pHandler = /*this->*/GetInterface(&IID_IUnknown);
		else
			throw _com_error(hr);
		if (pHandler)
		{
			hr = ptrCon->Advise(pHandler, &dwCookie);
			if (FAILED(hr))
				throw _com_error(hr);
		}
		else
			throw exception("Advise returned a NULL interface pointer"); // probably will never happen
	}
}

void DcxreqSink::Disconnect()
{
	if (ptrCon)
	{
		HRESULT hr = ptrCon->Unadvise(dwCookie);
		ASSERT(SUCCEEDED(hr)); // In release mode we don't want this to stop us but when debugging it better alert
		ptrCon.Release();
		ptrConPntCnt.Release();
		pHandler = 0;
		dwCookie = 0;
	}
}

DcxreqSink::~DcxreqSink()
{
	Disconnect();
}


void DcxreqSink::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

	CCmdTarget::OnFinalRelease();
}


BEGIN_MESSAGE_MAP(DcxreqSink, CCmdTarget)
END_MESSAGE_MAP()


BEGIN_DISPATCH_MAP(DcxreqSink, CCmdTarget)	
	DISP_FUNCTION(DcxreqSink, "OnFileSent", OnFileSent, VT_I4, VTS_BOOL VTS_BSTR)
	DISP_FUNCTION(DcxreqSink, "OnQueryResponseRecieved", OnQueryResponseRecieved, VT_I4, VTS_UNKNOWN)
	DISP_FUNCTION(DcxreqSink, "OnMoveResponseRecieved", OnMoveResponseRecieved, VT_I4, VTS_BOOL)
	DISP_FUNCTION(DcxreqSink, "OnMoveResponseRecievedEx", OnMoveResponseRecievedEx, VT_I4, VTS_UI2 VTS_UI2 VTS_UI2 VTS_UI2 VTS_UI2)
END_DISPATCH_MAP()

// Note: we add support for IID_ISink to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .IDL file.

// {4AF0BAFE-5575-4381-9CC9-59A0156D65B2}
static const IID IID_ISink =
{ 0x4AF0BAFE, 0x5575, 0x4381, { 0x9C, 0xC9, 0x59, 0xA0, 0x15, 0x6D, 0x65, 0xB2 } };

BEGIN_INTERFACE_MAP(DcxreqSink, CCmdTarget)
	INTERFACE_PART(DcxreqSink, __uuidof(IDCXREQEvents), Dispatch)
END_INTERFACE_MAP()

HRESULT DcxreqSink::OnFileSent(VARIANT_BOOL succeeded, BSTR filename)
{
	printf("OnFileSent\n");
	return S_OK;
}
HRESULT DcxreqSink::OnQueryResponseRecieved(IDCXOBJ *obj)
{
	printf("OnQueryResponseRecieved\n");
	printf("Result: %ws\n", obj->GetElement(patientName)->Value.bstrVal);
	return S_OK;
}

HRESULT DcxreqSink::OnMoveResponseRecieved(VARIANT_BOOL succeeded)
{
	printf("OnMoveResponseRecieved\n");
	return S_OK;
}


HRESULT DcxreqSink::OnMoveResponseRecievedEx(
	unsigned short status, ///< The status from the response. 0x0000 is success, 0xFF00 and 0xFF01 are pending, 0x0001 and 0xBxxx are warning, 0xAxxx and 0xCxxx are Failure, 0xFE00 is cancel
	unsigned short remaining, ///< Remaining sub operations, relevant only when status is pending
	unsigned short completed, ///< completed sub operations, relevant only when status is pending
	unsigned short failed, ///< Failed sub operations, relevant only when status is pending
	unsigned short warning ///< Warning sub operations, relevant only when status is pending
)
{
	printf("OnMoveResponseRecievedEx\n");
	return S_OK;
}