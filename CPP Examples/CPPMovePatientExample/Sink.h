/*
 * Copyright (C) 2005 - 2011, Roni Zaharia
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

	HRESULT OnMoveResponseRecievedEx(unsigned short,unsigned short,unsigned short,unsigned short,unsigned short);

	virtual void OnFinalRelease();

protected:
	DECLARE_MESSAGE_MAP()
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};


