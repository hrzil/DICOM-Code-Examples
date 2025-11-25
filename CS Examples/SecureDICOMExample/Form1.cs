using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using SecureDICOMExample;
using SecureDICOMExample.Properties;

namespace SecureDICOMExampleUI
{
    public partial class Form1 : Form
    {
        SecureServer localSCP;
        private SecurityContext serverSecurity;
        rzdcxLib.DCXAPP app = new rzdcxLib.DCXAPP();

        private StoreLocation SCUStore 
        { 
            get 
            {
                return this.cbSCUStore.SelectedItem.ToString().ToLower().StartsWith("local") ?
                    StoreLocation.LocalMachine : StoreLocation.CurrentUser;
            } 
        }
        private StoreLocation SCPStore
        {
            get
            {
                return this.cbSCPStoreLocal.SelectedItem.ToString().ToLower().StartsWith("local") ?
                    StoreLocation.LocalMachine : StoreLocation.CurrentUser;
            }
        }

        System.Threading.Thread tListener = null;

        private bool isListening 
        { 
            get 
            {
                return this.tListener.IsAlive;
            } 
        }

        public Form1()
        {
            InitializeComponent();
            this.app.LogLevel = rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG;
            this.app.StartLogging(
                System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "SecureDicom.log"));
            this.RefreshListener();

            //this.bgwListenerThread.DoWork += this.RunLocalListener;
        }

        private void RefreshListener()
        {
            this.tListener = new System.Threading.Thread(
                            new System.Threading.ThreadStart(this.RunLocalListener));
            this.tListener.SetApartmentState(System.Threading.ApartmentState.STA);
        }

        private void cbSCUTLS_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlSCUTLS.Enabled = this.cbSCUTLS.Checked;
        }

        private void cbSCPTLSLocal_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlSCPTLSLocal.Enabled = this.cbSCPTLSLocal.Checked;

        }

        private void rbSCUThumbprint_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSCUThumbprint.Enabled = this.rbSCUThumbprint.Checked;
            this.txtSCUSubjectName.Enabled = !this.rbSCUThumbprint.Checked;
        }

        private void rbSCPThumbprint_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSCPThumbprint.Enabled = this.rbSCPThumbprint.Checked;
            this.txtSCPSubject.Enabled = !this.rbSCPThumbprint.Checked;
        }

        private void cbSCUMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSCUAccepted.Enabled =
                this.cbSCUMethod.SelectedItem.ToString().Trim().ToUpper() == "THUMBPRINTS";
        }

        private void cbSCPMethodLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtSCPAcceptedLocal.Enabled = 
                this.cbSCPMethodLocal.SelectedItem.ToString().Trim().ToUpper() == "THUMBPRINTS";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Settings s = Settings.Default;

            this.cbSCUStore.SelectedIndex = s.SCU_cert_store;
            this.cbSCUMethod.SelectedIndex = s.SCU_method;

            this.rbSCUSubject.Checked = s.SCU_use_cn;
            this.rbSCUThumbprint.Checked = !s.SCU_use_cn;

            this.cbSCPStoreLocal.SelectedIndex = s.LSCP_cert_store;
            this.cbSCPMethodLocal.SelectedIndex = s.LSCP_method;

            this.rbSCPSubject.Checked = s.LSCP_use_cn;
            this.rbSCPThumbprint.Checked = !s.LSCP_use_cn;
        }

        private void btnSCUCert_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(System.Security.Cryptography.X509Certificates.StoreName.My, this.SCUStore);
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

                if (this.rbSCUSubject.Checked)
                {
                    string subject = cert.SubjectName.Name;

                    subject = subject.Substring(subject.IndexOf("CN="));
                    subject = subject.Substring(0, subject.IndexOf(','));

                    this.txtSCUSubjectName.Text = subject;
                }
                else
                {
                    this.txtSCUThumbprint.Text = cert.Thumbprint;
                }
            }
        }

        private void btnSCPCertLocal_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(System.Security.Cryptography.X509Certificates.StoreName.My, this.SCUStore);
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

                if (this.rbSCPSubject.Checked)
                {
                    string subject = cert.SubjectName.Name;

                    subject = subject.Substring(subject.IndexOf("CN="));
                    subject = subject.Substring(0, subject.IndexOf(','));

                    this.txtSCPSubject.Text = subject;
                }
                else
                {
                    this.txtSCPThumbprint.Text = cert.Thumbprint;
                }
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!this.isListening)
            {
                ushort port;
                //verify local inputs
                if (this.txtCalledAELocal.Text.Trim() == "")
                    MessageBox.Show(
                        "Please fill a local listener AE title",
                        "Missing Input",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                else if (this.txtSCPPortLocal.Text.Trim() == "" && UInt16.TryParse(this.txtSCPPortLocal.Text, out port))
                    MessageBox.Show(
                        "Please fill a valid local listener port",
                        "Missing Input",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                else
                {
                    this.serverSecurity = this.GetServerSecurity();
                    //this.bgwListenerThread.RunWorkerAsync();
                    //this.isListening = this.bgwListenerThread.IsBusy;

                    this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Starting Local Listener");
                    this.tListener.Start();
                    this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Local Listener Started");
                }
            }
            else
            {
                //this.bgwListenerThread.CancelAsync();
                //this.isListening = false;
                this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Stopping Local Listener");
                this.localSCP.Stop();
                this.tListener.Join();
                //this.tListener.Abort();
                this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Local Listener Stopped");
                this.RefreshListener();
            }

            this.btnEcho.Enabled = this.isListening;
            this.gbSCP.Enabled = !this.isListening;
            this.btnStartStop.Text = this.isListening ? "Stop Local Listener" : "Start Local Listener";
        }

        private void RunLocalListener()//object sender, DoWorkEventArgs args)
        {
            this.localSCP = new SecureServer(
                this.txtCalledAELocal.Text,
                Convert.ToUInt16(this.txtSCPPortLocal.Text),
                this.serverSecurity,//this.GetServerSecurity()
                this.app
                );
            this.localSCP.Listen();
        }

        private void btnEcho_Click(object sender, EventArgs e)
        {
            ushort port;
            //verify client inputs
            if (this.txtCallingAE.Text.Trim() == "")
                MessageBox.Show(
                    "Please fill a client AE title", 
                    "Missing Input",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
            else if (this.txtSCUPort.Text.Trim() == "" && UInt16.TryParse(this.txtSCUPort.Text, out port))
                MessageBox.Show(
                    "Please fill a valid client port",
                    "Missing Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            else
            {
                // do work according to the selected tab
                if (this.tabsSCP.SelectedIndex == 0)
                {
                    //verify remote inputs
                    if (this.txtCalledAERemote.Text.Trim() == "")
                        MessageBox.Show(
                            "Please fill a remote AE title",
                            "Missing Input",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    else if (this.txtSCPHostRemote.Text.Trim() == "")
                        MessageBox.Show(
                            "Please fill a remote host",
                            "Missing Input",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    else if (this.txtSCPPortRemote.Text.Trim() == "" && UInt16.TryParse(this.txtSCPPortRemote.Text, out port))
                        MessageBox.Show(
                            "Please fill a valid remote port",
                            "Missing Input",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    else
                    {
                        //send echo
                        this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Sending C-Echo to remote server");
                        this.SendEcho(
                            this.txtCallingAE.Text,
                            this.txtCalledAERemote.Text,
                            this.txtSCPHostRemote.Text,
                            Convert.ToUInt16(this.txtSCPPortRemote.Text));
                    }
                }
                else
                {
                    //send echo
                    this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Sending C-Echo to local listener");
                    this.SendEcho(
                            this.txtCallingAE.Text,
                            this.txtCalledAELocal.Text,
                            "localhost",
                            Convert.ToUInt16(this.txtSCPPortLocal.Text));
                }
            }
        }

        private void SendEcho(string callingAE, string calledAE, string host, ushort port)
        {
            SecureDICOMExample.SecurityContext clientSec = this.GetClientSecurity();

            var client = new SecureDICOMExample.SecureClient();
            string message = client.SendEcho(callingAE, calledAE, host, port, clientSec);
            if (message.Length>0)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "C-Echo failed: "+ message);
            }
            else
            { 
                MessageBox.Show("Succes!");
                this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "C-Echo succeeded");
            }

        }

        private SecureDICOMExample.SecurityContext GetClientSecurity()
        {
            SecureDICOMExample.SecurityContext security = null;

            if (this.cbSCUTLS.Checked)
            {
                this.app.WriteToLog(
                    rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG,
                    "Loading client certificate.");

                security = new SecureDICOMExample.SecurityContext();

                if (this.cbSCUStore.SelectedItem.ToString().ToLower().StartsWith("local"))
                    security.Store = StoreType.LocalMachine;
                else
                    security.Store = StoreType.CurrentUser;

                if (this.cbSCUMethod.SelectedItem.ToString().ToLower().StartsWith("trust"))
                    security.VerificationMethod = VerificationMethod.TrustChain;
                else if (this.cbSCUMethod.SelectedItem.ToString().ToLower().StartsWith("thumb"))
                    security.VerificationMethod = VerificationMethod.Thumbprints;
                else
                    security.VerificationMethod = VerificationMethod.None;

                if (this.txtSCUSubjectName.Text != null && this.txtSCUSubjectName.Text.Trim().Length > 0)
                    security.SubjectName = this.txtSCUSubjectName.Text;
                else
                    security.Thumbprint = this.txtSCUThumbprint.Text;

                security.AcceptedThumbprints = this.txtSCUAccepted.Text;

                this.app.WriteToLog(
                    rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG,
                    $"Certificate loaded: STORE='{security.Store}' | CN='{security.SubjectName}' | " +
                    $"TP='{security.Thumbprint}' | MA='{security.MutualAuthentication}' | " +
                    $"VM='{security.VerificationMethod}' | ATs='{security.AcceptedThumbprints}'");
            }

            return security;
        }

        private SecureDICOMExample.SecurityContext GetServerSecurity()
        {
            SecureDICOMExample.SecurityContext security = null;

            if (this.cbSCPTLSLocal.Checked)
            {
                this.app.WriteToLog(
                    rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG,
                    "Loading server certificate.");

                security = new SecureDICOMExample.SecurityContext();

                security.MutualAuthentication = this.cbMutAuth.Checked;

                if (this.cbSCPStoreLocal.SelectedItem.ToString().ToLower().StartsWith("local"))
                    security.Store = StoreType.LocalMachine;
                else
                    security.Store = StoreType.CurrentUser;

                if (this.cbSCPMethodLocal.SelectedItem.ToString().ToLower().StartsWith("trust"))
                    security.VerificationMethod = VerificationMethod.TrustChain;
                else if (this.cbSCPMethodLocal.SelectedItem.ToString().ToLower().StartsWith("thumb"))
                    security.VerificationMethod = VerificationMethod.Thumbprints;
                else
                    security.VerificationMethod = VerificationMethod.None;

                if (this.txtSCPSubject.Text != null && this.txtSCPSubject.Text.Trim().Length > 0)
                    security.SubjectName = this.txtSCPSubject.Text;
                else
                    security.Thumbprint = this.txtSCPThumbprint.Text;

                security.AcceptedThumbprints = this.txtSCPAcceptedLocal.Text;

                this.app.WriteToLog(
                    rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG,
                    $"Certificate loaded: STORE='{security.Store}' | CN='{security.SubjectName}' | " +
                    $"TP='{security.Thumbprint}' | MA='{security.MutualAuthentication}' | " +
                    $"VM='{security.VerificationMethod}' | ATs='{security.AcceptedThumbprints}'");
            }

            return security;
        }

        private void tabsSCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabsSCP.SelectedIndex == 0)
            {
                this.btnStartStop.Enabled = false;
                this.btnEcho.Enabled = true;
            }
            else
            {
                this.btnStartStop.Enabled = true;
                this.btnEcho.Enabled = this.isListening;
            }
        }
        
        private void SaveAllSettings()
        {
            this.SaveClientSettings();
            this.SaveRemoteServerSettings();
            this.SaveLocalServerSettings();
        }
        private void SaveClientSettings()
        {
            Settings s = Settings.Default;

            s.SCU_aet = this.txtCallingAE.Text.Trim();
            s.SCU_port = this.txtSCUPort.Text.Trim();

            if (this.cbSCUTLS.Checked)
            {
                s.SCU_accepted = this.txtSCUAccepted.Text.Trim();
                s.SCU_cert_cn = this.txtSCUSubjectName.Text.Trim();
                s.SCU_thumbprint = this.txtSCUThumbprint.Text.Trim();

                s.SCU_use_cn = this.rbSCUSubject.Checked;

                s.SCU_cert_store = this.cbSCUStore.SelectedIndex;
                s.SCU_method = this.cbSCUMethod.SelectedIndex;
            }
            s.SCU_use_tls = this.cbSCUTLS.Checked;

            s.Save();
        }
        private void SaveRemoteServerSettings()
        {
            Settings s = Settings.Default;

            s.RSCP_aet = this.txtCalledAERemote.Text.Trim();
            s.RSCP_host = this.txtSCPHostRemote.Text.Trim();
            s.RSCP_port = this.txtSCPPortRemote.Text.Trim();

            s.Save();
        }
        private void SaveLocalServerSettings()
        {
            Settings s = Settings.Default;

            s.LSCP_aet = this.txtCalledAELocal.Text.Trim();
            s.LSCP_port = this.txtSCPPortLocal.Text.Trim();

            if(this.cbSCPTLSLocal.Checked)
            {
                s.LSCP_accepted = this.txtSCPAcceptedLocal.Text.Trim();
                s.LSCP_cert_subj = this.txtSCPSubject.Text.Trim();
                s.LSCP_tumbprint = this.txtSCPThumbprint.Text.Trim();

                s.LSCP_mutual_auth = this.cbMutAuth.Checked;
                s.LSCP_use_cn = this.rbSCPSubject.Checked;

                s.LSCP_cert_store = this.cbSCPStoreLocal.SelectedIndex;
                s.LSCP_method = this.cbSCPMethodLocal.SelectedIndex;
            }
            s.LSCP_use_tls = this.cbSCPTLSLocal.Checked;

            s.Save();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            this.SaveAllSettings();
            this.app.WriteToLog(rzdcxLib.LOG_LEVEL.LOG_LEVEL_DEBUG, "Application settings saved");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tListener != null && this.tListener.IsAlive)
                this.tListener.Abort();

            this.app.FlushLog();
            this.app.StopLogging();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(this.app);
        }
    }
}
