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
#include "CPPStorageSCPExample.h"
#include "AppEventsSink.h"


// CAppEventsSink

IMPLEMENT_DYNAMIC(CAppEventsSink, CCmdTarget)


CAppEventsSink::CAppEventsSink()
{
	EnableAutomation();
}

CAppEventsSink::~CAppEventsSink()
{
}

BEGIN_MESSAGE_MAP(CAppEventsSink, CCmdTarget)
END_MESSAGE_MAP()


BEGIN_DISPATCH_MAP(CAppEventsSink, CCmdTarget)
	DISP_FUNCTION(CAppEventsSink, "OnMaxLogCount", OnMaxLogCount, VT_I4, VTS_I4 VTS_BSTR VTS_PBSTR)
END_DISPATCH_MAP()

// Note: we add support for IID_ISink to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .IDL file.

// {3B73F109-11A0-4716-930C-8CACB90AAEE2}
static const IID IID_ISink =
{ 0x3B73F109, 0x11A0, 0x4716, { 0x93, 0x0C, 0x8C, 0xAC, 0xB9, 0x0A, 0xAE, 0xE2 } };

BEGIN_INTERFACE_MAP(CAppEventsSink, CCmdTarget)
	INTERFACE_PART(CAppEventsSink, __uuidof(_IDCXAPPEvents), Dispatch)
END_INTERFACE_MAP()


// CAppEventsSink message handlers
HRESULT CAppEventsSink::OnMaxLogCount(int count, BSTR closed_file_name, BSTR* new_file_name)
{
    static int filecount=0;
    char fname[100];
	sprintf(fname, "c:\\rzdcxlog_%08d.log", ++filecount);
	USES_CONVERSION;
    *new_file_name = A2BSTR(fname);
	return 0;
}
