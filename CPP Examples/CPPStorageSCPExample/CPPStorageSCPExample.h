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

#include "resource.h"

/// \brief The Console Event Manager captures console events by setting the appropriate hook for it.
/// It is used when the application is running not as a service to capture user inputs such as CTRL-C.
/// \ingroup DicomService
template <class T>
class ConsoleEventManager {
public:

	static HANDLE m_endEvent;

	ConsoleEventManager() {
		m_endEvent = CreateEvent( 
			NULL,		// default security attributes
			TRUE,		// manual-reset event
			FALSE,		// initial state is not signaled
			_T("EndEvent")	// object name
			); 

		if (m_endEvent == NULL) 
		{ 
			// Ok, Ok, Be Silent!
			//printf("CreateEvent failed (%d)\n", GetLastError());
			return;
		}
        SignalStarting();
		BOOL res = SetConsoleCtrlHandler(handler, TRUE);
		if (!res)
			printf("Failed to set console handler\n");
	}

	~ConsoleEventManager() {
        SignalStopping();
		BOOL res = SetConsoleCtrlHandler(handler, FALSE);
		if (!res)
			printf("Failed to unset console handler\n");
	}

	/// This function is called when console events are raised.
	/// In all cases but for logoff events, it stops the application.
	/// Since we don't know who logs off, this event is ignored.
	static BOOL WINAPI handler(DWORD control_code)
	{
		switch (control_code) 
		{
		case CTRL_C_EVENT:
		case CTRL_BREAK_EVENT:
		case CTRL_CLOSE_EVENT:
		case CTRL_SHUTDOWN_EVENT:
			T::Stop();
			printf("WaitForSingleObject returned %x\n", WaitForSingleObject(m_endEvent,30*1000));
			return TRUE;
		case CTRL_LOGOFF_EVENT:
			return FALSE;
		default:
			return FALSE;
		}
	}

	void SignalStopping() {
		BOOL res = SetEvent(m_endEvent);
	}
	void SignalStarting() {
		BOOL res = ResetEvent(m_endEvent);
	}

};


