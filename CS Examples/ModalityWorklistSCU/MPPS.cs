using System;
using System.Collections.Generic;
using System.Text;
using rzdcxLib;
using System.Runtime.InteropServices;

namespace ModalityWorklistSCU
{
    class MPPS
    {
        public DCXOBJ rp { set; get; }
        public DCXOBJ sps { set; get; }
        public string SOPInstanceUID { set;  get; }
        public string aeTitle { set; get; }
        public string modality { set; get; }
        public enum PPSState
        {
            IN_PROGRESS,
            COMPLETED,
            DICOUNTINUED
        }
        public PPSState State;
        public MPPS(DCXOBJ inRP, DCXOBJ inSPS)
        {
            rp = inRP;
            sps = inSPS;
            modality = "OT";
            aeTitle = "RZDCX";
        }

        static DCXELM TryGetElement(DCXOBJ obj, DICOM_TAGS_ENUM tag)
        {
            try
            {
                DCXELM e = obj.getElementByTag((int)tag);
                return e;
            }
            catch (COMException)
            {
                return null;
            }

        }

        private DCXOBJ BuildNCreateObject()
        {
            DCXOBJ ssas = new DCXOBJ();
            DCXELM e = new DCXELM();
            DCXUID uid = new DCXUID();

            // Scheduled Step Attributes Sequence
            // This element hold the list of ID's that identify the SPS and RP that we 
            // created this MPPS for

            // Get the STUDT INSTANCE UID from the RP we got from the MWL 
            // or create a new one if not found
            e = TryGetElement(rp, DICOM_TAGS_ENUM.studyInstanceUID);
            if (e != null)
                ssas.insertElement(e);
            else
            {
                // Create a new UID
                e.Init((int)DICOM_TAGS_ENUM.studyInstanceUID);
                e.Value = uid.CreateUID(UID_TYPE.UID_TYPE_STUDY);
                ssas.insertElement(e);
            }

            e.Init((int)DICOM_TAGS_ENUM.ReferencedStudySequence);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            // Get the accession number from the RP. It should always be there
            e = TryGetElement(rp, DICOM_TAGS_ENUM.AccessionNumber);
            if (e != null)
                ssas.insertElement(e);

            // Get the RP ID and add it
            e = TryGetElement(rp, DICOM_TAGS_ENUM.RequestedProcedureID);
            if (e != null)
                ssas.insertElement(e);

            // Get the RP description
            e = TryGetElement(rp, DICOM_TAGS_ENUM.RequestedProcedureDescription);
            if (e != null)
                ssas.insertElement(e);

            // Get the SPS ID from the SPS object we got from the MWL
            e = TryGetElement(sps, DICOM_TAGS_ENUM.ScheduledProcedureStepID);
            if (e != null)
                ssas.insertElement(e);

            // SPS description
            e = TryGetElement(sps, DICOM_TAGS_ENUM.ScheduledProcedureStepDescription);
            if (e != null)
                ssas.insertElement(e);

            // If we have codes, not only text description
            e.Init((int)DICOM_TAGS_ENUM.ScheduledProtocolCodeSequence);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            // Add the Scheduled Step item to a sequence
            DCXOBJIterator sq = new DCXOBJIterator();
            sq.Insert(ssas);

            e.Init((int)DICOM_TAGS_ENUM.ScheduledStepAttributesSequence);
            e.Value = sq;

            ///
            /// Performed Procedure Step Object
            /// 

            DCXOBJ pps = new DCXOBJ();

            // Add the Scheduled Step sequence to the MPPS object
            pps.insertElement(e);

            // Add Patient name
            e = TryGetElement(rp, DICOM_TAGS_ENUM.PatientsName);
            if (e != null)
                pps.insertElement(e);

            // Add Patient ID
            e = TryGetElement(rp, DICOM_TAGS_ENUM.patientID);
            if (e != null)
                pps.insertElement(e);

            // Add birth date null 
            e.Init((int)DICOM_TAGS_ENUM.PatientsBirthDate);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // Add sex null
            e.Init((int)DICOM_TAGS_ENUM.PatientsSex);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // Referenced Patient Seq.
            e.Init((int)DICOM_TAGS_ENUM.ReferencedPatientSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // MPPS ID has not logic on it. It can be anything
            // The SCU have to create it but it doesn't have to be unique and the SCP 
            // should not relay on its uniqueness
            // Here we use a timestamp
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepID);
            e.Value = DateTime.Now.ToString("yyyymmddhhmmssttt"); 
            pps.insertElement(e);

            // Performed station AE title - this identify the modality that
            // did the work
            e.Init((int)DICOM_TAGS_ENUM.PerformedStationAETitle);
            e.Value = aeTitle;
            pps.insertElement(e);

            // A logical name of the station
            e.Init((int)DICOM_TAGS_ENUM.PerformedStationName);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // The location
            e.Init((int)DICOM_TAGS_ENUM.PerformedLocation);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // Start date and time - let's use 'Now'
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStartDate);
            e.Value = DateTime.Now;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStartTime);
            e.Value = DateTime.Now;
            pps.insertElement(e);

            // This is important! The initial state is "IN PROGRESS"
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStatus);
            e.Value = "IN PROGRESS";
            pps.insertElement(e);
            
            // Description, we can set it later as well in the N-SET
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // Some more type 2 elements ...

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureTypeDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK) 

            e.Init((int)DICOM_TAGS_ENUM.ProcedureCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndDate);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndTime);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            // Modality - it's a type 1
            e.Init((int)DICOM_TAGS_ENUM.Modality);
            if (modality != null && modality.Length > 0)
                e.Value = modality;
            else
                e.Value = "OT";
            pps.insertElement(e);

            // More type 2 elements
            e.Init((int)DICOM_TAGS_ENUM.StudyID);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProtocolCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedSeriesSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            return pps;
        }

        public void Create(NetConnectionInfo connInfo)
        {
            DCXOBJ createObj = BuildNCreateObject();
            ///
            /// Send the N-CREATE command
            /// 
            DCXREQ req = new DCXREQ();
            this.SOPInstanceUID =
                req.MPPS_Create(
                connInfo.CallingAETitle,
                connInfo.CalledETitle,
                connInfo.Host,
                connInfo.Port,
                createObj);
        }

        private DCXOBJ BuildNSetObject(bool completed)
        {
            DCXOBJ pps = new DCXOBJ();
            DCXELM e = new DCXELM();

            ///
            /// Performed Procedure Step
            /// 

            // These are the elements we can update in the N-SET

            // Set the status to "COMPLETED" or "DISCONTINUED"
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStatus);
            e.Value = completed ? "COMPLETED" : "DISCONTINUED";
            pps.insertElement(e);

            // End date and time
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndDate);
            e.Value = DateTime.Now;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndTime);
            e.Value = DateTime.Now;
            pps.insertElement(e);

            // More type 2 elements that we are allowed to change in the N-SET
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureTypeDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK) 

            e.Init((int)DICOM_TAGS_ENUM.ProcedureCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProtocolCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            /// 
            /// Performed Series Sequence - Must have at least one item even if discontinued!
            /// 
            DCXOBJ series_item = new DCXOBJ();
            e.Init((int)DICOM_TAGS_ENUM.PerformingPhysiciansName);
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.ProtocolName);
            e.Value = "SOME PROTOCOL";
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.OperatorsName);
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.seriesInstanceUID);
            e.Value = "1.2.3.4.5.6";
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.SeriesDescription);
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.RetrieveAETitle);
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.ReferencedImageSequence);
            series_item.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.ReferencedNonImageCompositeSOPInstanceSequence);
            series_item.insertElement(e);

            DCXOBJIterator series_sq = new DCXOBJIterator();
            series_sq.Insert(series_item);
            e.Init((int)DICOM_TAGS_ENUM.PerformedSeriesSequence);
            e.Value = series_sq;
            pps.insertElement(e);

            return pps;
        }

        public void Set(bool completed, NetConnectionInfo connInfo)
        {
            DCXOBJ ppsSet = BuildNSetObject(completed);
            ///
            /// Send the N-SET
            /// 
            DCXREQ req = new DCXREQ();
            req.MPPS_Set(
                connInfo.CallingAETitle,
                connInfo.CalledETitle,
                connInfo.Host,
                connInfo.Port,
                ppsSet, 
                SOPInstanceUID);
            State = completed ? PPSState.COMPLETED : PPSState.DICOUNTINUED;
        }
    }
}
