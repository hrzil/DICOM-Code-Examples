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

/// \page CSTestApplications C# Test Applications
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// \section MPPSSCUExample Modality Performed Procedure Step Example
/// Modality Performed Procedure Step Example
/// This demo shows how to send MPPS command to a server using RZDCX objects

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;

/// \example MPPSSCUExample.cs
/// A C# MPPS sending Example
namespace MPPSSCUExample
{
    public partial class MPPSSCUExample : Form
    {
        public MPPSSCUExample()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs ea)
        {
            textSentUID.Text = "";
            txtInstanceUID.Text = "";
            DCXOBJ ssas = new DCXOBJ();
            DCXELM e = new DCXELM();
            
            ///
            /// Scheduled Step Attributes Sequence
            /// 

            e.Init((int)DICOM_TAGS_ENUM.studyInstanceUID);
            e.Value = txtStudyUID.Text; /// Type 1 (Mandatory)
            ssas.insertElement(e); 

            e.Init((int)DICOM_TAGS_ENUM.ReferencedStudySequence);
            ssas.insertElement(e); /// Type 2 (0 length is OK)
            DCXOBJ obj = new DCXOBJ();

            e.Init((int)DICOM_TAGS_ENUM.AccessionNumber);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.RequestedProcedureID);
            ssas.insertElement(e); /// Type 2 (0 length is OK)
                                   /// 
            e.Init((int)DICOM_TAGS_ENUM.RequestedProcedureDescription);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepID);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepDescription);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.ScheduledProtocolCodeSequence);
            ssas.insertElement(e); /// Type 2 (0 length is OK)

            DCXOBJIterator sq = new DCXOBJIterator();
            sq.Insert(ssas);

            e.Init((int)DICOM_TAGS_ENUM.ScheduledStepAttributesSequence);
            e.Value = sq;
   
            ///
            /// Performed Procedure Step
            /// 

            DCXOBJ pps = new DCXOBJ();
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PatientsName);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.patientID);
            pps.insertElement(e); /// Type 2 (0 length is OK)
                                   /// 
            e.Init((int)DICOM_TAGS_ENUM.PatientsBirthDate);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PatientsSex);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.ReferencedPatientSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepID);
            e.Value = txtPPSID.Text;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedStationAETitle);
            e.Value = txtAETitle.Text;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedStationName);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedLocation);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStartDate);
            e.Value = dateStart.Value;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStartTime);
            e.Value = dateStart.Value;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStatus);
            e.Value = "IN PROGRESS";
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureTypeDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK) 

            e.Init((int)DICOM_TAGS_ENUM.ProcedureCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndDate);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndTime);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.Modality);
            if (cbModality.Text != null)
                e.Value = cbModality.Text;
            else 
                e.Value = "OT";
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.StudyID);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProtocolCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedSeriesSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            ///
            /// Send the N-CREATE
            /// 
            string sopInstanceUID;
            try
            {
                if (sender == btnCreate)
                {
                    DCXREQ req = new DCXREQ();
                    sopInstanceUID = req.MPPS_Create(txtAETitle.Text, txtRemoteAE.Text,
                        txtRemoteHost.Text, UInt16.Parse(txtRemotePort.Text), pps);
                }
                else if (sender == btnCreate2)
                {
                    DCXUID uid = new DCXUID();
                    string sopInstanceUID1 = uid.CreateUID(UID_TYPE.UID_TYPE_MPPS);
                    textSentUID.Text = sopInstanceUID1;
                    DCXREQ req = new DCXREQ();
                    sopInstanceUID = req.N_Create(txtAETitle.Text, txtRemoteAE.Text,
                        txtRemoteHost.Text, UInt16.Parse(txtRemotePort.Text), pps, "1.2.840.10008.3.1.2.3.3", sopInstanceUID1);
                }
                else
                    sopInstanceUID = "";

                txtInstanceUID.Text = sopInstanceUID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSet_Click(object sender, EventArgs ea)
        {
            DCXOBJ ssas = new DCXOBJ();
            DCXELM e = new DCXELM();

            ///
            /// Performed Procedure Step
            /// 

            DCXOBJ pps = new DCXOBJ();
            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndDate);
            e.Value = dateEnd.Value;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepEndTime);
            e.Value = dateEnd.Value;
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepStatus);
            e.Value = "COMPLETED";
            pps.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureStepDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProcedureTypeDescription);
            pps.insertElement(e); /// Type 2 (0 length is OK) 

            e.Init((int)DICOM_TAGS_ENUM.ProcedureCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            e.Init((int)DICOM_TAGS_ENUM.PerformedProtocolCodeSequence);
            pps.insertElement(e); /// Type 2 (0 length is OK)

            /// 
            /// Series Sequence
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

            ///
            /// Send the N-SET
            /// 

            try
            {
                DCXREQ req = new DCXREQ();
                req.MPPS_Set(txtAETitle.Text, txtRemoteAE.Text,
                    txtRemoteHost.Text, UInt16.Parse(txtRemotePort.Text), pps, txtInstanceUID.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateStudyUID_Click(object sender, EventArgs e)
        {
            txtStudyUID.Text = (new DCXUID()).CreateUID(UID_TYPE.UID_TYPE_STUDY);
        }
    }
}
