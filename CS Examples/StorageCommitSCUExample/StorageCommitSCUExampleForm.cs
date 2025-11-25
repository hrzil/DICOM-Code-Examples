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
/// \section StorageCommitSCUDemo Storage Commit SCU Demo
/// Storage Commit SCU Demo
/// This example shows how to send a storage commit request to the
/// called ae.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example StorageCommitSCUExampleForm.cs
/// A C# DICOM Storage Commitment SCU Example
namespace StorageCommitSCUExample
{

    public partial class StorageCommitSCUExampleForm : Form
    {
        public StorageCommitSCUExampleForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CommitBtn_Click(object sender, EventArgs e)
        {
            DCXREQ req = null;
            try
            {
                req = new DCXREQ();

                // Send a commit command to the target ae with the files list (; separated)
                string res = req.CommitFiles(LocalAEEdit.Text,
                             TargetAEEdit.Text,
                             HostEdit.Text,
                             ushort.Parse(PortEdit.Text),
                             FilePathEdit.Text);

                MessageBox.Show(res);
            }
            catch (System.Runtime.InteropServices.COMException com_e)
            {
                MessageBox.Show(com_e.Message, "Commit files failed");
            }
            finally
            {
                ReleaseComObject(req);
            }
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    FilePathEdit.Text += openFileDialog.FileNames[i] + ";";
                }
            }
            else
                FilePathEdit.Text = "";
        }


        class AccepterHandler
        {
            bool _status;
            string _transaction_uid;
            string _succeeded_instances;
            string _failed_instances;

            public bool done = false;

            public void Accept()
            {
                DCXACC accepter = new DCXACC();
                accepter.OnCommitResult += new IDCXACCEvents_OnCommitResultEventHandler(accepter_OnCommitResult);

                while (!done)
                {
                    if (accepter.WaitForConnection("RZDCX", 104, 5))
                    {
                        while (accepter.WaitForCommand(30))
                            continue;
                    }
                }
            }

            void accepter_OnCommitResult(bool status, string transaction_uid, string succeeded_instances, string failed_instances)
            {
                MessageBox.Show(
                    "Status: " + status + "\n" +
                    "transaction UID: " + transaction_uid + "\n" +
                    "Succeeded: " + succeeded_instances + "\n" +
                    "Failed: " + failed_instances + "\n");
            }
        }

    }
}