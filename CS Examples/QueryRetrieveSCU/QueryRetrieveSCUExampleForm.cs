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
/// \section QRSCU Query/Retrieve SCU Demo
/// Query/Retrieve SCU Demo
/// This example shows how to execute a query to the called ae
/// and how to retrieve dicom objects from the called ae to the 
/// target ae

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example QueryRetrieveSCUExampleForm.cs
/// A C# DICOM Query/Retrieve SCU Example
namespace QueryRetrieveSCU
{
    public partial class QueryRetrieveSCUExampleForm : Form
    {
        public QueryRetrieveSCUExampleForm()
        {
            InitializeComponent();
        }

        /* Callback when a response from the called ae for the 
           query command is recieved */
        public void OnQueryResponseRecievedAction(DCXOBJ obj)
        {
            //throw new ApplicationException("Cancel!");
            DCXELM e = obj.getElementByTag((int)DICOM_TAGS_ENUM.patientID);
            string pid = e.Value.ToString();
            e = obj.getElementByTag((int)DICOM_TAGS_ENUM.PatientsBirthDate);
            string bdate;
            if (e != null && e.Value != null)
                bdate = e.Value.ToString();

            // Do something
            ReleaseComObject(obj);
        }

        private DCXOBJ CreateQuery()
        {
            // Fill the query object
            DCXOBJ obj = new DCXOBJ();
            DCXELM el = new DCXELM();

            el.Init((int)DICOM_TAGS_ENUM.QueryRetrieveLevel);
            el.Value = "PATIENT";
            obj.insertElement(el);

            el.Init(0x00100010);
            el.Value = PatientNameEdit.Text;
            obj.insertElement(el);

            el.Init(0x00100020);
            el.Value = PatientIDEdit.Text;
            obj.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.PatientsSex);
            obj.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.PatientsBirthDate);
            obj.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.PregnancyStatus);
            el.Value = 4;
            obj.insertElement(el);
            return obj;
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();

            try
            {
                DCXOBJ obj = CreateQuery();
                // Create the requester object and connect it's callback to our method
                DCXREQ req = new DCXREQClass();
                req.OnQueryResponseRecieved += new IDCXREQEvents_OnQueryResponseRecievedEventHandler(OnQueryResponseRecievedAction);

                // send the query command
                DCXOBJIterator it = req.Query(LocalAEEdit.Text, 
                               TargetAEEdit.Text, 
                               HostEdit.Text, 
                               ushort.Parse(PortEdit.Text), 
                               "1.2.840.10008.5.1.4.1.2.1.1", 
                               obj);

                DCXOBJ currObj = null;

                // Iterate over the query results
                for (; !it.AtEnd(); it.Next())
                {
                    currObj = it.Get();
                    string infoLine = "";
                    DCXELM currElem = currObj.GetElement(0x00100020);
                    if (currElem != null)
                        infoLine += "" + currElem.Value;

                    currElem = currObj.GetElement(0x00100010);
                    if (currElem != null)
                        infoLine += " " + currElem.Value;

                    currElem = currObj.GetElement((int)DICOM_TAGS_ENUM.PregnancyStatus);
                    if (currElem != null)
                        infoLine += " " + currElem.Value;

                    checkedListBox1.Items.Add(infoLine, false);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query failed: " + ex.Message);
            }
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create the requester object and connect it's event to our callback method
                DCXAPP app = new DCXAPP();
                app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
                app.StartLogging(textBoxSaveDir.Text + "\\MoveAndStoreExample.log");
                DCXREQ req = new DCXREQ();
                req.OnMoveResponseRecievedEx += new IDCXREQEvents_OnMoveResponseRecievedExEventHandler(req_OnMoveResponseRecievedEx);
                req.OnGetResponseRecieved += Req_OnGetResponseRecieved;
                string patientName = "";
                string patientID = "";
                char[] delimiter = {' '};
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    string temp = (string)checkedListBox1.CheckedItems[i];
                    string[] array = temp.Split(delimiter);

                    patientID = array[0];
                    patientName = array[1];

                    DCXOBJ identifier = new DCXOBJ();
                    DCXELM ePatName = identifier.createElement((int)DICOM_TAGS_ENUM.patientName);
                    ePatName.Value = patientName;
                    identifier.insertElement(ePatName);

                    DCXELM ePatId = identifier.createElement((int)DICOM_TAGS_ENUM.patientID);
                    ePatId.Value = patientName;
                    identifier.insertElement(ePatId);

                    DCXELM qrLevel = identifier.createElement((int)DICOM_TAGS_ENUM.QueryRetrieveLevel);
                    qrLevel.Value = "PATIENT";
                    identifier.insertElement(qrLevel);

                    if (radioMoveAndStore.Checked)
                    {
                        
                        DCXACC acc = new DCXACC();
                        acc.StoreDirectory = textBoxSaveDir.Text;
                        req.MoveAndStore(LocalAEEdit.Text, TargetAEEdit.Text, HostEdit.Text,ushort.Parse( PortEdit.Text), LocalAEEdit.Text, identifier, ushort.Parse( textListenPort.Text), acc);
                    }
                    else if (radioOtherApp.Checked)
                    {
                        // Move the patient to the target ae
                        req.MovePatient(LocalAEEdit.Text,
                                       TargetAEEdit.Text,
                                       HostEdit.Text,
                                       ushort.Parse(PortEdit.Text),
                                       LocalAEEdit.Text,
                                       patientName,
                                       patientID);
                    }
                    else if (radioCget.Checked)
                    {
                        req.Get(LocalAEEdit.Text, TargetAEEdit.Text, HostEdit.Text, ushort.Parse(PortEdit.Text), "1.2.840.10008.5.1.4.1.2.2.3", identifier);
                    }

                    
                }

                MessageBox.Show("Move succeeded");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Move failed: " + ex.Message);
            }
        }

        private void Req_OnGetResponseRecieved(ushort status, ushort remaining, ushort completed, ushort failed, ushort warning)
        {
            throw new NotImplementedException();
        }

        void req_OnMoveResponseRecievedEx(ushort status, ushort remaining, ushort completed, ushort failed, ushort warning)
        {
            // do nothing
        }

        private void radioMoveAndStore_CheckedChanged(object sender, EventArgs e)
        {
            if(radioMoveAndStore.Checked)
            {
                textListenPort.Enabled = true;
                textBoxSaveDir.Enabled = true;
            }
        }

        private void radioOtherApp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOtherApp.Checked)
            {
                textListenPort.Enabled = false;
                textBoxSaveDir.Enabled = false;
            }
        }

        private void radioCget_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCget.Checked)
            {
                textListenPort.Enabled = false;
                textBoxSaveDir.Enabled = true;
            }

        }
    }
}