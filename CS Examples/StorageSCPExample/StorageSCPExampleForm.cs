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
/// \section StorageSCPDemo Storage SCP Demo
/// Storage SCP Demo
/// This demo shows how to use the acceptor class to wait for incomming
/// commands and process them

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example StorageSCPExampleForm.cs
/// A C# Single-Threaded DICOM Storage SCP Example
namespace StorageSCPExample
{
    public partial class StorageSCPExampleForm : Form
    {
        static private int _filecount = 0;

        public StorageSCPExampleForm()
        {
            InitializeComponent();
            this.certControl1.use_mutualAuth = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /* Callback method when a connection is recieved
           Here you can check the calling ae and accept or 
           reject the association*/
        private void OnConnectionEventHandler(string callingTitle, 
                                       string calledTitle, 
                                       string callingHost,
                                       ref bool acceptConnection)
        {
            acceptConnection = true;
        }

        /* When a store command is recieved, you can provide the
           filename to store to*/
        private void OnStoreSetupEventHandler(ref string filename)
        {
            filename = ".\\dcm" + (++_filecount) + ".dcm";

            MessageBox.Show("Store setup on file: " + filename);
        }

        /* Handle the on timeout event here */
        private void OnTimeoutEventHandler()
        {
            MessageBox.Show("A timeout occurred");
        }

        /* When store is done, handle the file here and respond if storage went well */
        private void OnStoreDoneEventHandler(string filename, bool status, ref bool accept)
        {
            MessageBox.Show("Store done, file: " + filename + " status: " + status);

            string sop_instance_uid = null;
            DCXOBJ o = new DCXOBJ();
            o.openFile(filename);
            DCXELM e = o.getElementByTag((int)DICOM_TAGS_ENUM.sopInstanceUID);
            if (e != null)
            {
                sop_instance_uid = e.Value.ToString();
            }
            ReleaseComObject(e);
            ReleaseComObject(o);
            if (sop_instance_uid != null)
                System.IO.File.Move(filename, ".\\" + sop_instance_uid + ".dcm");

            accept = true;
        }

        /* When a result for the storage commit is recieved 
           this event occurs. Handle the succeeded and failed instances here*/
        private void OnCommitResultEventHandler(bool status, 
                                                string transactionUID, 
                                                string succeededInstances,
                                                string failedInstances)
        {
            MessageBox.Show("Commit result: Status: " + status + "\n" +
                            "TransactionUID: " + transactionUID + "\n" +
                            "SucceededInstances: " + succeededInstances + "\n" +
                            "FailedInstances: " + failedInstances);
        }

        private void ListenBtn_Click(object sender, EventArgs e)
        {

            DCXAPP app = new DCXAPP();
            app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
            app.StartLogging("c:\\rzdcxLog.txt");

            // Create the acceptor object here
            DCXACC acc = new DCXACC();
            // Add TLS security context
            acc.SecurityContext = this.GetDCXSEC();
            // Connect all the events to the callback methods
            acc.OnConnection += new IDCXACCEvents_OnConnectionEventHandler(OnConnectionEventHandler);
            acc.OnStoreSetup += new IDCXACCEvents_OnStoreSetupEventHandler(OnStoreSetupEventHandler);
            acc.OnTimeout += new IDCXACCEvents_OnTimeoutEventHandler(OnTimeoutEventHandler);
            acc.OnStoreDone += new IDCXACCEvents_OnStoreDoneEventHandler(OnStoreDoneEventHandler);
            acc.OnCommitResult += new IDCXACCEvents_OnCommitResultEventHandler(OnCommitResultEventHandler);

            // This is the standard loop to wait for connections and commands
            while (true)
            {
                if (acc.WaitForConnection(LocalAEEdit.Text, ushort.Parse(PortEdit.Text), 30))
                {
                    // Can go to new thread
                    bool res;
                    do
                    {
                        if (acc.WaitForCommand(30))
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    } while (res);
                }
            }

            ReleaseComObject(acc);

            app.StopLogging();

            ReleaseComObject(app);
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }
        private DCXSEC GetDCXSEC()
        {
            // a null DCXSEC means not to use TLS
            DCXSEC sec = null;
            if (this.certControl1.useTLS)
            {
                // create a new DCXSEC object
                sec = new DCXSEC();

                // select a certificate store
                switch (this.certControl1.certStore)
                {
                    // Local Machine certificate store
                    case System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine:
                        sec.CertStore = DCXSEC_CERT_STORE.DCXSEC_CERT_STORE_LOCAL_COMPUTER;
                        break;
                    // Current User certificate store
                    case System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser:
                    default:
                        sec.CertStore = DCXSEC_CERT_STORE.DCXSEC_CERT_STORE_CURRENT_USER;
                        break;
                }

                // select a certificate either by the subject's name (CN)
                // or by the cert's thumbprint (aka signature)
                // after the cert is loaded both the subject name and the
                // thumbprint are available for read from the DCXSEC(get)
                if (this.certControl1.certThumbprint != null && this.certControl1.certThumbprint.Trim() != "")
                    // set by thumbprint
                    sec.CertThumbprint = this.certControl1.certThumbprint;
                else
                    // set by subject name
                    sec.CertSubjectName = this.certControl1.certSubjectName;

                // select a verification method
                switch (this.certControl1.verificationMethod)
                {
                    // select trust chain to allow connections only if the other side got a certificate
                    // that it's issuer is trusted by the user\computer (according to the selected store)
                    case CertControl.VerificationMethod.TrustChain:
                        sec.VerificationMethod = DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_TRUST_CHAIN;
                        break;
                    // select thumbprints to allow connections only if the other side got a certificate
                    // that it's thumbprint is one of those supplied to DCXSEC::AcceptedThumbprints
                    case CertControl.VerificationMethod.Thumbprints:
                        sec.VerificationMethod = DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_THUMBPRINT;
                        // Accepted thumbprints should be a semicolon (;) seperated list
                        sec.AcceptedThumbprints = this.certControl1.acceptedThumbprints;
                        break;
                    // select none to allow connections from any certificate
                    case CertControl.VerificationMethod.None:
                    default:
                        sec.VerificationMethod = DCXSEC_VERIFICATION_METHOD.DCXSEC_VERIFICATION_METHOD_NONE;
                        break;
                }

                // set to true for the server to try verifying the client
                // (does not affect client side)
                sec.MutualAuthentication = this.certControl1.mutualAuth;

                sec.DebugLog = true;
            }

            return sec;
        }
    }
}