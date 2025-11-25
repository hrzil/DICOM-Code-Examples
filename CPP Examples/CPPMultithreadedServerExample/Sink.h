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

#pragma once

#include <string>

/// \page CPPTestApplications C++ Test Applications
/// \brief CSinkCommandTarget CSink Command Target
/// CSink Command Target
/// C++ Command Sink for connection points of the accepter class.
/// This class is provided as part of the C++ Storage SCP Demo Application.
/// Use this class to recieve callbacks of the Accepter class from C++ applications.
/// This class wraps the implementation of COM Connection points.
class CSink : public CCmdTarget
{
	DECLARE_DYNAMIC(CSink)

public:
	CSink();
	virtual ~CSink();

	/// Called when a Storage Commit result is recieved through a N-EVENT-REPORT command.
	HRESULT OnCommitResult(VARIANT_BOOL status, BSTR transactionUID, BSTR succeededInstances, BSTR failedInstances);

	/// Called for every incomming association request.
	/// Return true if you would like to accept the association or false to reject it.
	HRESULT OnConnection(BSTR calling, BSTR called, BSTR host, VARIANT_BOOL *accept_connection);

	/// Called when a C-STORE command starts. 
	/// filename is the filename of the stored object.
	HRESULT OnStoreSetup(BSTR *filename);
	
	/// Called when a C-STORE command ends. 
	/// filename is the filename of the stored object.
	HRESULT OnStoreDone(BSTR filename,
				VARIANT_BOOL storage_status,
				VARIANT_BOOL *accept_storage);

	/// Called when no commands has been recieved for a predefined period of time.
	HRESULT OnTimeout();

	virtual void OnFinalRelease();

    std::string mTID;
	std::string mSList; 
	std::string mFList; 

protected:
	DECLARE_MESSAGE_MAP()
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()

private:
    BSTR mFilename;
};


