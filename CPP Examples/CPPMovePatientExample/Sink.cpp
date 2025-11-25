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


// CSink

IMPLEMENT_DYNAMIC(CSink, CCmdTarget)


CSink::CSink()
{
	EnableAutomation();
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
	DISP_FUNCTION(CSink, "Dummy", OnMoveResponseRecievedEx, VT_I4, VTS_NONE)
	DISP_FUNCTION(CSink, "Dummy", OnMoveResponseRecievedEx, VT_I4, VTS_NONE)
	DISP_FUNCTION(CSink, "Dummy", OnMoveResponseRecievedEx, VT_I4, VTS_NONE)
	DISP_FUNCTION(CSink, "OnMoveResponseRecievedEx", OnMoveResponseRecievedEx, VT_I4, VTS_UI2 VTS_UI2 VTS_UI2 VTS_UI2 VTS_UI2)
END_DISPATCH_MAP()

// Note: we add support for IID_ISink to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .IDL file.

// {4AF0BAFE-5575-4381-9CC9-59A0156D65B2}
static const IID IID_ISink =
{ 0x4AF0BAFE, 0x5575, 0x4381, { 0x9C, 0xC9, 0x59, 0xA0, 0x15, 0x6D, 0x65, 0xB2 } };

BEGIN_INTERFACE_MAP(CSink, CCmdTarget)
	INTERFACE_PART(CSink, __uuidof(IDCXREQEvents), Dispatch)
END_INTERFACE_MAP()



HRESULT CSink::OnMoveResponseRecievedEx(unsigned short,unsigned short,unsigned short,unsigned short,unsigned short)
{
	return E_ABORT;
}