using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace CertControl
{
    public enum VerificationMethod
    {
        None = 0,         // Don't verify at all
        TrustChain = 1,   // Check if the cert issuer is trusted
        Thumbprints = 2   // Compare other side cert to a list of allowed certs
    }
    public partial class CertControl: UserControl
    {
        public StoreLocation certStore
        {
            get
            {
                switch (this.cbSCPStoreLocal.SelectedItem.ToString().ToUpper())
                {
                    case "LOCAL MACHINE":
                        return StoreLocation.LocalMachine;
                    case "CURRENT USER":
                    default:
                        return StoreLocation.CurrentUser;
                }
            }
        }
        public string certSubjectName
        {
            get
            {
                return this.txtSCPSubject.Text;
            }
        }
        public string certThumbprint
        {
            get
            {
                return this.txtSCPThumbprint.Text;
            }
        }
        public VerificationMethod verificationMethod
        {
            get
            {
                switch(this.cbSCPMethodLocal.SelectedItem.ToString().ToUpper())
                {
                    case "TRUST":
                        return VerificationMethod.TrustChain;
                    case "THUMBPRINTS":
                        return VerificationMethod.Thumbprints;
                    case "NONE":
                    default:
                        return VerificationMethod.None;
                }
            }
        }
        public string acceptedThumbprints
        {
            get { return this.txtSCPAcceptedLocal.Text; }
        }
        bool _use_mutualAuth = false;
        public bool use_mutualAuth
        {
            get
            {
                return this._use_mutualAuth;
            }
            set 
            { 
                this.cbMutAuth.Visible = value;
                this._use_mutualAuth = value;
            }
        }
        public bool mutualAuth
        {
            get
            {
                return this.cbMutAuth.Checked;
            }
        }
        public bool useTLS
        {
            get { return this.cbUseTLS.Checked;}
        }

        public CertControl()
        {
            InitializeComponent();
            this.cbSCPMethodLocal.SelectedIndex = 0;
            this.cbSCPStoreLocal.SelectedIndex = 0;
        }

        private void cbUseTLS_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlSCPTLSLocal.Enabled = cbUseTLS.Checked;
        }

        private void cbSCPMethodLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cbSCPMethodLocal.SelectedItem != null )
            {
                this.txtSCPAcceptedLocal.Enabled =
                    this.cbSCPMethodLocal.SelectedItem.ToString().ToUpper() == "THUMBPRINTS";
                
            }
        }

        private void btnSCPCertLocal_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(StoreName.My, this.certStore);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

            X509Certificate2Collection selected = X509Certificate2UI.SelectFromCollection(
                store.Certificates,
                "Select a certificate",
                "Select a certificate",
                X509SelectionFlag.SingleSelection);

            store.Close();

            if (selected.Count > 0)
            {
                X509Certificate2 cert = selected[0];

                string subject = cert.SubjectName.Name;

                subject = subject.Substring(subject.IndexOf("CN="));

                if(subject.Contains(','))
                    subject = subject.Substring(0, subject.IndexOf(','));

                this.txtSCPSubject.Text = subject;
                this.txtSCPThumbprint.Text = cert.Thumbprint;
            }
        }

        private void cbSCPStoreLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void rbSCPSubject_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSCPSubject.Enabled = this.rbSCPSubject.Checked;
            this.txtSCPThumbprint.Enabled = !this.rbSCPSubject.Checked;
        }
    }
}
