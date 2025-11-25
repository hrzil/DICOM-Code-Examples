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
/// \section CPPMPPSSCUExample C++ Modality Performed Procedure Step (MPPS) SCU Example
/// This example shows how to create a Modality Performed Procedure Step client (SCU)

/// Dll import from the current directory
#import "../../MODALIZER-SDK/win32/rzdcx.dll" rename_namespace ("rzdcxLib")

using namespace rzdcxLib;

/// \example CPPMPPSSCUExample.cpp
/// This is an example of how to to create a Modality Performed Procedure Step client (SCU).
int main(int argc, char* argv[])
{
	CoInitialize(NULL);

	_bstr_t sopInstanceUID;

	{

		IDCXOBJPtr ssas(__uuidof(DCXOBJ));
		IDCXELMPtr e(_uuidof(DCXELM));

		///
		/// Scheduled Step Attributes Sequence
		/// 

		e->Init((int)studyInstanceUID);
		e->Value = "2.16.124.113543.6021.1.1.1513159247.8668.1560837709.1"; /// Type 1 (Mandatory)
		//e->Value = "1.2.3.4.5"; /// Type 1 (Mandatory)
		ssas->insertElement(e); 

		e->Init((int)ReferencedStudySequence);
		ssas->insertElement(e); /// Type 2 (0 length is OK)
		IDCXOBJPtr obj(__uuidof(DCXOBJ));

		e->Init((int)AccessionNumber);
		ssas->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)RequestedProcedureID);
		ssas->insertElement(e); /// Type 2 (0 length is OK)
		/// 
		e->Init((int)RequestedProcedureDescription);
		ssas->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)ScheduledProcedureStepID);
		e->Value = 1;			/// HRZ-ZiKiT Worklist Manager identifies according to ScheduledProcedureStepID
		ssas->insertElement(e); /// Type 2 (0 length is OK)


		e->Init((int)ScheduledProcedureStepDescription);
		ssas->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)ScheduledProtocolCodeSequence);
		ssas->insertElement(e); /// Type 2 (0 length is OK)

		IDCXOBJIteratorPtr sq(__uuidof(DCXOBJIterator));
		sq->Insert(ssas);

		e->Init((int)ScheduledStepAttributesSequence);
		e->PutValue( &_variant_t( (IUnknown*)sq, false ) );

		///
		/// Performed Procedure Step
		/// 

		IDCXOBJPtr pps(__uuidof(DCXOBJ));
		pps->insertElement(e);

		e->Init((int)PatientsName);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)patientID);
		pps->insertElement(e); /// Type 2 (0 length is OK)
		/// 
		e->Init((int)PatientsBirthDate);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PatientsSex);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)ReferencedPatientSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProcedureStepID);
		e->Value = 123;
		pps->insertElement(e);

		e->Init((int)PerformedStationAETitle);
		e->Value = "RZDCX";
		pps->insertElement(e);

		e->Init((int)PerformedStationName);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedLocation);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProcedureStepStartDate);
		e->Value = "20090101";
		pps->insertElement(e);

		e->Init((int)PerformedProcedureStepStartTime);
		e->Value = "10:10:10";
		pps->insertElement(e);

		e->Init((int)PerformedProcedureStepStatus);
		e->Value = "IN PROGRESS";
		pps->insertElement(e);

		e->Init((int)PerformedProcedureStepDescription);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProcedureTypeDescription);
		pps->insertElement(e); /// Type 2 (0 length is OK) 

		e->Init((int)ProcedureCodeSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProcedureStepEndDate);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProcedureStepEndTime);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)Modality);
		e->Value = "US";
		pps->insertElement(e);

		e->Init((int)StudyID);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedProtocolCodeSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init((int)PerformedSeriesSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		///
		/// Send the N-CREATE
		/// 

		IDCXREQPtr req(__uuidof(DCXREQ));
		sopInstanceUID = req->MPPS_Create("RZDCX", "DSRSVC", "localhost", 104, pps);

	}

	{
		IDCXOBJPtr ssas(__uuidof(DCXOBJ));
		IDCXELMPtr e(__uuidof(DCXELM));

		///
		/// Performed Procedure Step
		/// 

		IDCXOBJPtr pps(__uuidof(DCXOBJ));
		e->Init(PerformedProcedureStepEndDate);
		e->Value = "20090102";
		pps->insertElement(e);

		e->Init(PerformedProcedureStepEndTime);
		e->Value = "101012";
		pps->insertElement(e);

		e->Init(PerformedProcedureStepStatus);
		e->Value = "COMPLETED";
		pps->insertElement(e);

		e->Init(PerformedProcedureStepDescription);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init(PerformedProcedureTypeDescription);
		pps->insertElement(e); /// Type 2 (0 length is OK) 

		e->Init(ProcedureCodeSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		e->Init(PerformedProtocolCodeSequence);
		pps->insertElement(e); /// Type 2 (0 length is OK)

		/// 
		/// Series Sequence
		/// 
		IDCXOBJPtr series_item(__uuidof(DCXOBJ));
		e->Init(PerformingPhysiciansName);
		series_item->insertElement(e);

		e->Init(ProtocolName);
		e->Value = "SOME PROTOCOL";
		series_item->insertElement(e);

		e->Init(OperatorsName);
		series_item->insertElement(e);

		e->Init(seriesInstanceUID);
		e->Value = "1.2.3.4.5.6";
		series_item->insertElement(e);

		e->Init(SeriesDescription);
		series_item->insertElement(e);

		e->Init(RetrieveAETitle);
		series_item->insertElement(e);

		e->Init(ReferencedImageSequence);
		series_item->insertElement(e);

		e->Init(ReferencedNonImageCompositeSOPInstanceSequence);
		series_item->insertElement(e);

		IDCXOBJIteratorPtr series_sq(__uuidof(DCXOBJIterator));
		series_sq->Insert(series_item);
		e->Init(PerformedSeriesSequence);
		e->PutValue( &_variant_t( (IUnknown*)series_sq, false ) );
		pps->insertElement(e);

		///
		/// Send the N-SET
		/// 

		IDCXREQPtr req(__uuidof(DCXREQ));
		req->MPPS_Set("RZDCX", "DSRSVC", "localhost", 104, pps, sopInstanceUID);
	}

	CoUninitialize();
}

