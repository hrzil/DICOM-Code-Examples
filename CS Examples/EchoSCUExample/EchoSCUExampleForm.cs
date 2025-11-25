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
/// \section EchoSCUDemo Verification SCU Demo
/// Echo SCU Demo
/// This example shows how to call echo to the called AE.
/// In this project we use a manifest file instead of COM registration 
/// for demonstaration of registry free COM.
/// This technique is very usefull for example in DICOM CD Viewer where
/// you can't register the COM object on the CD but you would like to
/// use it in your application.

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example EchoSCUExampleForm.cs
/// A C# DICOM Verification (C-ECHO) SCU
namespace EchoSCUExample
{
    public partial class EchoSCUExampleForm : Form
    {
        public EchoSCUExampleForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this method demonstrates how to configure a DCXSEC
        /// security context object
        /// </summary>
        /// <returns>A new DCXSEC object from the parameters set on the UI</returns>
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
            }

            return sec;
        }
        private void EchoBtn_Click(object sender, System.EventArgs e)
        {
            DCXREQ req = null;
            DCXSEC sec = this.GetDCXSEC();
            try
            {
                // Create the requester object
                req = new DCXREQ();
                // Add TLS security context
                if (sec != null)
                    req.SecurityContext = sec;
                // Call the echo method. If echo fails an exception is thrown
                req.Echo(LocalAEEdit.Text,
                         TargetAEEdit.Text,
                         HostEdit.Text,
                         ushort.Parse(PortEdit.Text));

                MessageBox.Show("Echo succeeded");
            }
            catch (System.Runtime.InteropServices.COMException com_e)
            {
                MessageBox.Show("Echo failed: " + com_e.Message);
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
    }
}