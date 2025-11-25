/*
 * Copyright (c) 2017 - HRZ Software Services LTD
 */
#pragma once

// A COM Connection Point Client class for receiving DCXRREQ Events using MFC Command Target OLE Automation Class
class DcxreqSink : public CCmdTarget
{
	DECLARE_DYNAMIC(DcxreqSink)

	IConnectionPointContainerPtr ptrConPntCnt; // COM Auto Pointers don't require initialization
	IConnectionPointPtr ptrCon; // COM Auto Pointers don't require initialization
	IUnknown* pHandler;
	DWORD dwCookie;


public:
	DcxreqSink();
	virtual ~DcxreqSink();

	// Connect to a DCXREQ instance events
	// Get the 
	void Connect(IDCXREQ*);

	//  
	void Disconnect();

	HRESULT OnFileSent(VARIANT_BOOL succeeded, BSTR filename);
	HRESULT OnQueryResponseRecieved(IDCXOBJ *obj);
	HRESULT OnMoveResponseRecieved(VARIANT_BOOL succeeded);
	HRESULT OnMoveResponseRecievedEx(unsigned short,unsigned short,unsigned short,unsigned short,unsigned short);

	virtual void OnFinalRelease();

protected:
	DECLARE_MESSAGE_MAP()
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};

