namespace SecureDICOMExampleUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSCU = new System.Windows.Forms.GroupBox();
            this.cbSCUTLS = new System.Windows.Forms.CheckBox();
            this.lbSCUPort = new System.Windows.Forms.Label();
            this.txtSCUPort = new System.Windows.Forms.TextBox();
            this.lblCallingAE = new System.Windows.Forms.Label();
            this.txtCallingAE = new System.Windows.Forms.TextBox();
            this.pnlSCUTLS = new System.Windows.Forms.Panel();
            this.btnSCUCert = new System.Windows.Forms.Button();
            this.lblSCUAccepted = new System.Windows.Forms.Label();
            this.txtSCUAccepted = new System.Windows.Forms.TextBox();
            this.lblSCUMethod = new System.Windows.Forms.Label();
            this.cbSCUMethod = new System.Windows.Forms.ComboBox();
            this.rbSCUThumbprint = new System.Windows.Forms.RadioButton();
            this.lblSCUStore = new System.Windows.Forms.Label();
            this.rbSCUSubject = new System.Windows.Forms.RadioButton();
            this.cbSCUStore = new System.Windows.Forms.ComboBox();
            this.txtSCUThumbprint = new System.Windows.Forms.TextBox();
            this.txtSCUSubjectName = new System.Windows.Forms.TextBox();
            this.gbSCP = new System.Windows.Forms.GroupBox();
            this.tabsSCP = new System.Windows.Forms.TabControl();
            this.tabSCPRemore = new System.Windows.Forms.TabPage();
            this.lblSCPHostRemote = new System.Windows.Forms.Label();
            this.txtSCPHostRemote = new System.Windows.Forms.TextBox();
            this.lblSCPPortRemote = new System.Windows.Forms.Label();
            this.txtSCPPortRemote = new System.Windows.Forms.TextBox();
            this.lblCalledAERemote = new System.Windows.Forms.Label();
            this.txtCalledAERemote = new System.Windows.Forms.TextBox();
            this.tabSCPLocal = new System.Windows.Forms.TabPage();
            this.cbSCPTLSLocal = new System.Windows.Forms.CheckBox();
            this.lblSCPPortLocal = new System.Windows.Forms.Label();
            this.lblCalledAELocal = new System.Windows.Forms.Label();
            this.pnlSCPTLSLocal = new System.Windows.Forms.Panel();
            this.cbMutAuth = new System.Windows.Forms.CheckBox();
            this.lblSCPAcceptedLocal = new System.Windows.Forms.Label();
            this.txtSCPAcceptedLocal = new System.Windows.Forms.TextBox();
            this.lblSCPMethodLocal = new System.Windows.Forms.Label();
            this.cbSCPMethodLocal = new System.Windows.Forms.ComboBox();
            this.btnSCPCertLocal = new System.Windows.Forms.Button();
            this.rbSCPThumbprint = new System.Windows.Forms.RadioButton();
            this.lblSCPStoreLocal = new System.Windows.Forms.Label();
            this.rbSCPSubject = new System.Windows.Forms.RadioButton();
            this.cbSCPStoreLocal = new System.Windows.Forms.ComboBox();
            this.txtSCPThumbprint = new System.Windows.Forms.TextBox();
            this.txtSCPSubject = new System.Windows.Forms.TextBox();
            this.txtSCPPortLocal = new System.Windows.Forms.TextBox();
            this.txtCalledAELocal = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnEcho = new System.Windows.Forms.Button();
            this.bgwListenerThread = new System.ComponentModel.BackgroundWorker();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.gbSCU.SuspendLayout();
            this.pnlSCUTLS.SuspendLayout();
            this.gbSCP.SuspendLayout();
            this.tabsSCP.SuspendLayout();
            this.tabSCPRemore.SuspendLayout();
            this.tabSCPLocal.SuspendLayout();
            this.pnlSCPTLSLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSCU
            // 
            this.gbSCU.Controls.Add(this.cbSCUTLS);
            this.gbSCU.Controls.Add(this.lbSCUPort);
            this.gbSCU.Controls.Add(this.txtSCUPort);
            this.gbSCU.Controls.Add(this.lblCallingAE);
            this.gbSCU.Controls.Add(this.txtCallingAE);
            this.gbSCU.Controls.Add(this.pnlSCUTLS);
            this.gbSCU.Location = new System.Drawing.Point(13, 12);
            this.gbSCU.Name = "gbSCU";
            this.gbSCU.Size = new System.Drawing.Size(775, 150);
            this.gbSCU.TabIndex = 0;
            this.gbSCU.TabStop = false;
            this.gbSCU.Text = "SCU Configuration";
            // 
            // cbSCUTLS
            // 
            this.cbSCUTLS.AutoSize = true;
            this.cbSCUTLS.Checked = global::SecureDICOMExample.Properties.Settings.Default.SCU_use_tls;
            this.cbSCUTLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSCUTLS.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SecureDICOMExample.Properties.Settings.Default, "SCU_use_tls", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSCUTLS.Location = new System.Drawing.Point(627, 22);
            this.cbSCUTLS.Name = "cbSCUTLS";
            this.cbSCUTLS.Size = new System.Drawing.Size(68, 17);
            this.cbSCUTLS.TabIndex = 4;
            this.cbSCUTLS.Text = "Use TLS";
            this.cbSCUTLS.UseVisualStyleBackColor = true;
            this.cbSCUTLS.CheckedChanged += new System.EventHandler(this.cbSCUTLS_CheckedChanged);
            // 
            // lbSCUPort
            // 
            this.lbSCUPort.AutoSize = true;
            this.lbSCUPort.Location = new System.Drawing.Point(282, 23);
            this.lbSCUPort.Name = "lbSCUPort";
            this.lbSCUPort.Size = new System.Drawing.Size(26, 13);
            this.lbSCUPort.TabIndex = 3;
            this.lbSCUPort.Text = "Port";
            // 
            // txtSCUPort
            // 
            this.txtSCUPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "SCU_port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCUPort.Location = new System.Drawing.Point(335, 20);
            this.txtSCUPort.Name = "txtSCUPort";
            this.txtSCUPort.Size = new System.Drawing.Size(56, 20);
            this.txtSCUPort.TabIndex = 2;
            this.txtSCUPort.Text = global::SecureDICOMExample.Properties.Settings.Default.SCU_port;
            // 
            // lblCallingAE
            // 
            this.lblCallingAE.AutoSize = true;
            this.lblCallingAE.Location = new System.Drawing.Point(6, 23);
            this.lblCallingAE.Name = "lblCallingAE";
            this.lblCallingAE.Size = new System.Drawing.Size(62, 13);
            this.lblCallingAE.TabIndex = 1;
            this.lblCallingAE.Text = "Calling AET";
            // 
            // txtCallingAE
            // 
            this.txtCallingAE.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "SCU_aet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCallingAE.Location = new System.Drawing.Point(116, 20);
            this.txtCallingAE.Name = "txtCallingAE";
            this.txtCallingAE.Size = new System.Drawing.Size(122, 20);
            this.txtCallingAE.TabIndex = 0;
            this.txtCallingAE.Text = global::SecureDICOMExample.Properties.Settings.Default.SCU_aet;
            // 
            // pnlSCUTLS
            // 
            this.pnlSCUTLS.Controls.Add(this.btnSCUCert);
            this.pnlSCUTLS.Controls.Add(this.lblSCUAccepted);
            this.pnlSCUTLS.Controls.Add(this.txtSCUAccepted);
            this.pnlSCUTLS.Controls.Add(this.lblSCUMethod);
            this.pnlSCUTLS.Controls.Add(this.cbSCUMethod);
            this.pnlSCUTLS.Controls.Add(this.rbSCUThumbprint);
            this.pnlSCUTLS.Controls.Add(this.lblSCUStore);
            this.pnlSCUTLS.Controls.Add(this.rbSCUSubject);
            this.pnlSCUTLS.Controls.Add(this.cbSCUStore);
            this.pnlSCUTLS.Controls.Add(this.txtSCUThumbprint);
            this.pnlSCUTLS.Controls.Add(this.txtSCUSubjectName);
            this.pnlSCUTLS.Location = new System.Drawing.Point(9, 49);
            this.pnlSCUTLS.Name = "pnlSCUTLS";
            this.pnlSCUTLS.Size = new System.Drawing.Size(760, 95);
            this.pnlSCUTLS.TabIndex = 7;
            // 
            // btnSCUCert
            // 
            this.btnSCUCert.Location = new System.Drawing.Point(693, 9);
            this.btnSCUCert.Name = "btnSCUCert";
            this.btnSCUCert.Size = new System.Drawing.Size(64, 41);
            this.btnSCUCert.TabIndex = 11;
            this.btnSCUCert.Text = "Browse";
            this.btnSCUCert.UseVisualStyleBackColor = true;
            this.btnSCUCert.Click += new System.EventHandler(this.btnSCUCert_Click);
            // 
            // lblSCUAccepted
            // 
            this.lblSCUAccepted.AutoSize = true;
            this.lblSCUAccepted.Location = new System.Drawing.Point(268, 73);
            this.lblSCUAccepted.Name = "lblSCUAccepted";
            this.lblSCUAccepted.Size = new System.Drawing.Size(114, 13);
            this.lblSCUAccepted.TabIndex = 10;
            this.lblSCUAccepted.Text = "Accepted Thumbprints";
            // 
            // txtSCUAccepted
            // 
            this.txtSCUAccepted.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "SCU_accepted", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCUAccepted.Enabled = false;
            this.txtSCUAccepted.Location = new System.Drawing.Point(386, 70);
            this.txtSCUAccepted.Name = "txtSCUAccepted";
            this.txtSCUAccepted.Size = new System.Drawing.Size(300, 20);
            this.txtSCUAccepted.TabIndex = 9;
            this.txtSCUAccepted.Text = global::SecureDICOMExample.Properties.Settings.Default.SCU_accepted;
            // 
            // lblSCUMethod
            // 
            this.lblSCUMethod.AutoSize = true;
            this.lblSCUMethod.Location = new System.Drawing.Point(4, 73);
            this.lblSCUMethod.Name = "lblSCUMethod";
            this.lblSCUMethod.Size = new System.Drawing.Size(98, 13);
            this.lblSCUMethod.TabIndex = 8;
            this.lblSCUMethod.Text = "Verification Method";
            // 
            // cbSCUMethod
            // 
            this.cbSCUMethod.FormattingEnabled = true;
            this.cbSCUMethod.Items.AddRange(new object[] {
            "None",
            "Trust Chain",
            "Thumbprints"});
            this.cbSCUMethod.Location = new System.Drawing.Point(108, 70);
            this.cbSCUMethod.Name = "cbSCUMethod";
            this.cbSCUMethod.Size = new System.Drawing.Size(121, 21);
            this.cbSCUMethod.TabIndex = 7;
            this.cbSCUMethod.SelectedIndexChanged += new System.EventHandler(this.cbSCUMethod_SelectedIndexChanged);
            // 
            // rbSCUThumbprint
            // 
            this.rbSCUThumbprint.AutoSize = true;
            this.rbSCUThumbprint.Location = new System.Drawing.Point(271, 32);
            this.rbSCUThumbprint.Name = "rbSCUThumbprint";
            this.rbSCUThumbprint.Size = new System.Drawing.Size(79, 17);
            this.rbSCUThumbprint.TabIndex = 6;
            this.rbSCUThumbprint.TabStop = true;
            this.rbSCUThumbprint.Text = "ThumbPrint";
            this.rbSCUThumbprint.UseVisualStyleBackColor = true;
            this.rbSCUThumbprint.CheckedChanged += new System.EventHandler(this.rbSCUThumbprint_CheckedChanged);
            // 
            // lblSCUStore
            // 
            this.lblSCUStore.AutoSize = true;
            this.lblSCUStore.Location = new System.Drawing.Point(4, 12);
            this.lblSCUStore.Name = "lblSCUStore";
            this.lblSCUStore.Size = new System.Drawing.Size(54, 13);
            this.lblSCUStore.TabIndex = 3;
            this.lblSCUStore.Text = "Cert Store";
            // 
            // rbSCUSubject
            // 
            this.rbSCUSubject.AutoSize = true;
            this.rbSCUSubject.Checked = true;
            this.rbSCUSubject.Location = new System.Drawing.Point(271, 10);
            this.rbSCUSubject.Name = "rbSCUSubject";
            this.rbSCUSubject.Size = new System.Drawing.Size(92, 17);
            this.rbSCUSubject.TabIndex = 5;
            this.rbSCUSubject.TabStop = true;
            this.rbSCUSubject.Text = "Subject Name";
            this.rbSCUSubject.UseVisualStyleBackColor = true;
            // 
            // cbSCUStore
            // 
            this.cbSCUStore.FormattingEnabled = true;
            this.cbSCUStore.Items.AddRange(new object[] {
            "Local Machine",
            "Current User"});
            this.cbSCUStore.Location = new System.Drawing.Point(108, 9);
            this.cbSCUStore.Name = "cbSCUStore";
            this.cbSCUStore.Size = new System.Drawing.Size(121, 21);
            this.cbSCUStore.TabIndex = 2;
            // 
            // txtSCUThumbprint
            // 
            this.txtSCUThumbprint.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "SCU_thumbprint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCUThumbprint.Enabled = false;
            this.txtSCUThumbprint.Location = new System.Drawing.Point(386, 31);
            this.txtSCUThumbprint.Name = "txtSCUThumbprint";
            this.txtSCUThumbprint.Size = new System.Drawing.Size(300, 20);
            this.txtSCUThumbprint.TabIndex = 1;
            this.txtSCUThumbprint.Text = global::SecureDICOMExample.Properties.Settings.Default.SCU_thumbprint;
            // 
            // txtSCUSubjectName
            // 
            this.txtSCUSubjectName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "SCU_cert_cn", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCUSubjectName.Location = new System.Drawing.Point(386, 9);
            this.txtSCUSubjectName.Name = "txtSCUSubjectName";
            this.txtSCUSubjectName.Size = new System.Drawing.Size(300, 20);
            this.txtSCUSubjectName.TabIndex = 0;
            this.txtSCUSubjectName.Text = global::SecureDICOMExample.Properties.Settings.Default.SCU_cert_cn;
            // 
            // gbSCP
            // 
            this.gbSCP.Controls.Add(this.tabsSCP);
            this.gbSCP.Location = new System.Drawing.Point(13, 168);
            this.gbSCP.Name = "gbSCP";
            this.gbSCP.Size = new System.Drawing.Size(775, 215);
            this.gbSCP.TabIndex = 1;
            this.gbSCP.TabStop = false;
            this.gbSCP.Text = "SCP Configuration";
            // 
            // tabsSCP
            // 
            this.tabsSCP.Controls.Add(this.tabSCPRemore);
            this.tabsSCP.Controls.Add(this.tabSCPLocal);
            this.tabsSCP.Location = new System.Drawing.Point(9, 20);
            this.tabsSCP.Name = "tabsSCP";
            this.tabsSCP.SelectedIndex = 0;
            this.tabsSCP.Size = new System.Drawing.Size(757, 190);
            this.tabsSCP.TabIndex = 0;
            this.tabsSCP.SelectedIndexChanged += new System.EventHandler(this.tabsSCP_SelectedIndexChanged);
            // 
            // tabSCPRemore
            // 
            this.tabSCPRemore.Controls.Add(this.lblSCPHostRemote);
            this.tabSCPRemore.Controls.Add(this.txtSCPHostRemote);
            this.tabSCPRemore.Controls.Add(this.lblSCPPortRemote);
            this.tabSCPRemore.Controls.Add(this.txtSCPPortRemote);
            this.tabSCPRemore.Controls.Add(this.lblCalledAERemote);
            this.tabSCPRemore.Controls.Add(this.txtCalledAERemote);
            this.tabSCPRemore.Location = new System.Drawing.Point(4, 22);
            this.tabSCPRemore.Name = "tabSCPRemore";
            this.tabSCPRemore.Padding = new System.Windows.Forms.Padding(3);
            this.tabSCPRemore.Size = new System.Drawing.Size(749, 164);
            this.tabSCPRemore.TabIndex = 0;
            this.tabSCPRemore.Text = "Remote SCP";
            // 
            // lblSCPHostRemote
            // 
            this.lblSCPHostRemote.AutoSize = true;
            this.lblSCPHostRemote.Location = new System.Drawing.Point(213, 8);
            this.lblSCPHostRemote.Name = "lblSCPHostRemote";
            this.lblSCPHostRemote.Size = new System.Drawing.Size(29, 13);
            this.lblSCPHostRemote.TabIndex = 9;
            this.lblSCPHostRemote.Text = "Host";
            // 
            // txtSCPHostRemote
            // 
            this.txtSCPHostRemote.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "RSCP_host", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCPHostRemote.Location = new System.Drawing.Point(245, 5);
            this.txtSCPHostRemote.Name = "txtSCPHostRemote";
            this.txtSCPHostRemote.Size = new System.Drawing.Size(122, 20);
            this.txtSCPHostRemote.TabIndex = 8;
            this.txtSCPHostRemote.Text = global::SecureDICOMExample.Properties.Settings.Default.RSCP_host;
            // 
            // lblSCPPortRemote
            // 
            this.lblSCPPortRemote.AutoSize = true;
            this.lblSCPPortRemote.Location = new System.Drawing.Point(380, 8);
            this.lblSCPPortRemote.Name = "lblSCPPortRemote";
            this.lblSCPPortRemote.Size = new System.Drawing.Size(26, 13);
            this.lblSCPPortRemote.TabIndex = 7;
            this.lblSCPPortRemote.Text = "Port";
            // 
            // txtSCPPortRemote
            // 
            this.txtSCPPortRemote.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "RSCP_port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCPPortRemote.Location = new System.Drawing.Point(413, 5);
            this.txtSCPPortRemote.Name = "txtSCPPortRemote";
            this.txtSCPPortRemote.Size = new System.Drawing.Size(122, 20);
            this.txtSCPPortRemote.TabIndex = 6;
            this.txtSCPPortRemote.Text = global::SecureDICOMExample.Properties.Settings.Default.RSCP_port;
            // 
            // lblCalledAERemote
            // 
            this.lblCalledAERemote.AutoSize = true;
            this.lblCalledAERemote.Location = new System.Drawing.Point(3, 8);
            this.lblCalledAERemote.Name = "lblCalledAERemote";
            this.lblCalledAERemote.Size = new System.Drawing.Size(60, 13);
            this.lblCalledAERemote.TabIndex = 5;
            this.lblCalledAERemote.Text = "Called AET";
            // 
            // txtCalledAERemote
            // 
            this.txtCalledAERemote.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "RSCP_aet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCalledAERemote.Location = new System.Drawing.Point(71, 5);
            this.txtCalledAERemote.Name = "txtCalledAERemote";
            this.txtCalledAERemote.Size = new System.Drawing.Size(122, 20);
            this.txtCalledAERemote.TabIndex = 4;
            this.txtCalledAERemote.Text = global::SecureDICOMExample.Properties.Settings.Default.RSCP_aet;
            // 
            // tabSCPLocal
            // 
            this.tabSCPLocal.Controls.Add(this.cbSCPTLSLocal);
            this.tabSCPLocal.Controls.Add(this.lblSCPPortLocal);
            this.tabSCPLocal.Controls.Add(this.lblCalledAELocal);
            this.tabSCPLocal.Controls.Add(this.pnlSCPTLSLocal);
            this.tabSCPLocal.Controls.Add(this.txtSCPPortLocal);
            this.tabSCPLocal.Controls.Add(this.txtCalledAELocal);
            this.tabSCPLocal.Location = new System.Drawing.Point(4, 22);
            this.tabSCPLocal.Name = "tabSCPLocal";
            this.tabSCPLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tabSCPLocal.Size = new System.Drawing.Size(749, 164);
            this.tabSCPLocal.TabIndex = 1;
            this.tabSCPLocal.Text = "Local SCP";
            // 
            // cbSCPTLSLocal
            // 
            this.cbSCPTLSLocal.AutoSize = true;
            this.cbSCPTLSLocal.Checked = global::SecureDICOMExample.Properties.Settings.Default.LSCP_use_tls;
            this.cbSCPTLSLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSCPTLSLocal.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_use_tls", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSCPTLSLocal.Location = new System.Drawing.Point(614, 11);
            this.cbSCPTLSLocal.Name = "cbSCPTLSLocal";
            this.cbSCPTLSLocal.Size = new System.Drawing.Size(68, 17);
            this.cbSCPTLSLocal.TabIndex = 12;
            this.cbSCPTLSLocal.Text = "Use TLS";
            this.cbSCPTLSLocal.UseVisualStyleBackColor = true;
            this.cbSCPTLSLocal.CheckedChanged += new System.EventHandler(this.cbSCPTLSLocal_CheckedChanged);
            // 
            // lblSCPPortLocal
            // 
            this.lblSCPPortLocal.AutoSize = true;
            this.lblSCPPortLocal.Location = new System.Drawing.Point(253, 12);
            this.lblSCPPortLocal.Name = "lblSCPPortLocal";
            this.lblSCPPortLocal.Size = new System.Drawing.Size(26, 13);
            this.lblSCPPortLocal.TabIndex = 11;
            this.lblSCPPortLocal.Text = "Port";
            // 
            // lblCalledAELocal
            // 
            this.lblCalledAELocal.AutoSize = true;
            this.lblCalledAELocal.Location = new System.Drawing.Point(4, 12);
            this.lblCalledAELocal.Name = "lblCalledAELocal";
            this.lblCalledAELocal.Size = new System.Drawing.Size(60, 13);
            this.lblCalledAELocal.TabIndex = 9;
            this.lblCalledAELocal.Text = "Called AET";
            // 
            // pnlSCPTLSLocal
            // 
            this.pnlSCPTLSLocal.Controls.Add(this.cbMutAuth);
            this.pnlSCPTLSLocal.Controls.Add(this.lblSCPAcceptedLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.txtSCPAcceptedLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.lblSCPMethodLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.cbSCPMethodLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.btnSCPCertLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.rbSCPThumbprint);
            this.pnlSCPTLSLocal.Controls.Add(this.lblSCPStoreLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.rbSCPSubject);
            this.pnlSCPTLSLocal.Controls.Add(this.cbSCPStoreLocal);
            this.pnlSCPTLSLocal.Controls.Add(this.txtSCPThumbprint);
            this.pnlSCPTLSLocal.Controls.Add(this.txtSCPSubject);
            this.pnlSCPTLSLocal.Location = new System.Drawing.Point(6, 38);
            this.pnlSCPTLSLocal.Name = "pnlSCPTLSLocal";
            this.pnlSCPTLSLocal.Size = new System.Drawing.Size(737, 119);
            this.pnlSCPTLSLocal.TabIndex = 13;
            // 
            // cbMutAuth
            // 
            this.cbMutAuth.AutoSize = true;
            this.cbMutAuth.Checked = global::SecureDICOMExample.Properties.Settings.Default.LSCP_mutual_auth;
            this.cbMutAuth.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_mutual_auth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbMutAuth.Location = new System.Drawing.Point(98, 72);
            this.cbMutAuth.Name = "cbMutAuth";
            this.cbMutAuth.Size = new System.Drawing.Size(129, 17);
            this.cbMutAuth.TabIndex = 17;
            this.cbMutAuth.Text = "Mutual Authentication";
            this.cbMutAuth.UseVisualStyleBackColor = true;
            // 
            // lblSCPAcceptedLocal
            // 
            this.lblSCPAcceptedLocal.AutoSize = true;
            this.lblSCPAcceptedLocal.Location = new System.Drawing.Point(258, 98);
            this.lblSCPAcceptedLocal.Name = "lblSCPAcceptedLocal";
            this.lblSCPAcceptedLocal.Size = new System.Drawing.Size(114, 13);
            this.lblSCPAcceptedLocal.TabIndex = 16;
            this.lblSCPAcceptedLocal.Text = "Accepted Thumbprints";
            // 
            // txtSCPAcceptedLocal
            // 
            this.txtSCPAcceptedLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_accepted", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCPAcceptedLocal.Enabled = false;
            this.txtSCPAcceptedLocal.Location = new System.Drawing.Point(376, 95);
            this.txtSCPAcceptedLocal.Name = "txtSCPAcceptedLocal";
            this.txtSCPAcceptedLocal.Size = new System.Drawing.Size(300, 20);
            this.txtSCPAcceptedLocal.TabIndex = 15;
            this.txtSCPAcceptedLocal.Text = global::SecureDICOMExample.Properties.Settings.Default.LSCP_accepted;
            // 
            // lblSCPMethodLocal
            // 
            this.lblSCPMethodLocal.AutoSize = true;
            this.lblSCPMethodLocal.Location = new System.Drawing.Point(0, 98);
            this.lblSCPMethodLocal.Name = "lblSCPMethodLocal";
            this.lblSCPMethodLocal.Size = new System.Drawing.Size(98, 13);
            this.lblSCPMethodLocal.TabIndex = 14;
            this.lblSCPMethodLocal.Text = "Verification Method";
            // 
            // cbSCPMethodLocal
            // 
            this.cbSCPMethodLocal.FormattingEnabled = true;
            this.cbSCPMethodLocal.Items.AddRange(new object[] {
            "None",
            "Trust Chain",
            "Thumbprints"});
            this.cbSCPMethodLocal.Location = new System.Drawing.Point(98, 95);
            this.cbSCPMethodLocal.Name = "cbSCPMethodLocal";
            this.cbSCPMethodLocal.Size = new System.Drawing.Size(121, 21);
            this.cbSCPMethodLocal.TabIndex = 13;
            this.cbSCPMethodLocal.SelectedIndexChanged += new System.EventHandler(this.cbSCPMethodLocal_SelectedIndexChanged);
            // 
            // btnSCPCertLocal
            // 
            this.btnSCPCertLocal.Location = new System.Drawing.Point(682, 9);
            this.btnSCPCertLocal.Name = "btnSCPCertLocal";
            this.btnSCPCertLocal.Size = new System.Drawing.Size(52, 41);
            this.btnSCPCertLocal.TabIndex = 12;
            this.btnSCPCertLocal.Text = "Browse";
            this.btnSCPCertLocal.UseVisualStyleBackColor = true;
            this.btnSCPCertLocal.Click += new System.EventHandler(this.btnSCPCertLocal_Click);
            // 
            // rbSCPThumbprint
            // 
            this.rbSCPThumbprint.AutoSize = true;
            this.rbSCPThumbprint.Location = new System.Drawing.Point(250, 33);
            this.rbSCPThumbprint.Name = "rbSCPThumbprint";
            this.rbSCPThumbprint.Size = new System.Drawing.Size(79, 17);
            this.rbSCPThumbprint.TabIndex = 6;
            this.rbSCPThumbprint.TabStop = true;
            this.rbSCPThumbprint.Text = "ThumbPrint";
            this.rbSCPThumbprint.UseVisualStyleBackColor = true;
            this.rbSCPThumbprint.CheckedChanged += new System.EventHandler(this.rbSCPThumbprint_CheckedChanged);
            // 
            // lblSCPStoreLocal
            // 
            this.lblSCPStoreLocal.AutoSize = true;
            this.lblSCPStoreLocal.Location = new System.Drawing.Point(4, 12);
            this.lblSCPStoreLocal.Name = "lblSCPStoreLocal";
            this.lblSCPStoreLocal.Size = new System.Drawing.Size(54, 13);
            this.lblSCPStoreLocal.TabIndex = 3;
            this.lblSCPStoreLocal.Text = "Cert Store";
            // 
            // rbSCPSubject
            // 
            this.rbSCPSubject.AutoSize = true;
            this.rbSCPSubject.Checked = true;
            this.rbSCPSubject.Location = new System.Drawing.Point(250, 10);
            this.rbSCPSubject.Name = "rbSCPSubject";
            this.rbSCPSubject.Size = new System.Drawing.Size(92, 17);
            this.rbSCPSubject.TabIndex = 5;
            this.rbSCPSubject.TabStop = true;
            this.rbSCPSubject.Text = "Subject Name";
            this.rbSCPSubject.UseVisualStyleBackColor = true;
            // 
            // cbSCPStoreLocal
            // 
            this.cbSCPStoreLocal.FormattingEnabled = true;
            this.cbSCPStoreLocal.Items.AddRange(new object[] {
            "Local Machine",
            "Current User"});
            this.cbSCPStoreLocal.Location = new System.Drawing.Point(97, 9);
            this.cbSCPStoreLocal.Name = "cbSCPStoreLocal";
            this.cbSCPStoreLocal.Size = new System.Drawing.Size(121, 21);
            this.cbSCPStoreLocal.TabIndex = 2;
            // 
            // txtSCPThumbprint
            // 
            this.txtSCPThumbprint.Enabled = false;
            this.txtSCPThumbprint.Location = new System.Drawing.Point(376, 31);
            this.txtSCPThumbprint.Name = "txtSCPThumbprint";
            this.txtSCPThumbprint.Size = new System.Drawing.Size(300, 20);
            this.txtSCPThumbprint.TabIndex = 1;
            this.txtSCPThumbprint.Text = global::SecureDICOMExample.Properties.Settings.Default.LSCP_tumbprint;
            // 
            // txtSCPSubject
            // 
            this.txtSCPSubject.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_cert_subj", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCPSubject.Location = new System.Drawing.Point(376, 9);
            this.txtSCPSubject.Name = "txtSCPSubject";
            this.txtSCPSubject.Size = new System.Drawing.Size(300, 20);
            this.txtSCPSubject.TabIndex = 0;
            this.txtSCPSubject.Text = global::SecureDICOMExample.Properties.Settings.Default.LSCP_cert_subj;
            // 
            // txtSCPPortLocal
            // 
            this.txtSCPPortLocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSCPPortLocal.Location = new System.Drawing.Point(322, 9);
            this.txtSCPPortLocal.Name = "txtSCPPortLocal";
            this.txtSCPPortLocal.Size = new System.Drawing.Size(56, 20);
            this.txtSCPPortLocal.TabIndex = 10;
            this.txtSCPPortLocal.Text = global::SecureDICOMExample.Properties.Settings.Default.LSCP_port;
            // 
            // txtCalledAELocal
            // 
            this.txtCalledAELocal.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SecureDICOMExample.Properties.Settings.Default, "LSCP_aet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCalledAELocal.Location = new System.Drawing.Point(103, 9);
            this.txtCalledAELocal.Name = "txtCalledAELocal";
            this.txtCalledAELocal.Size = new System.Drawing.Size(121, 20);
            this.txtCalledAELocal.TabIndex = 8;
            this.txtCalledAELocal.Text = global::SecureDICOMExample.Properties.Settings.Default.LSCP_aet;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Enabled = false;
            this.btnStartStop.Location = new System.Drawing.Point(29, 389);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(230, 23);
            this.btnStartStop.TabIndex = 14;
            this.btnStartStop.Text = "Start Local Listener";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnEcho
            // 
            this.btnEcho.Location = new System.Drawing.Point(265, 389);
            this.btnEcho.Name = "btnEcho";
            this.btnEcho.Size = new System.Drawing.Size(230, 23);
            this.btnEcho.TabIndex = 2;
            this.btnEcho.Text = "Send Echo";
            this.btnEcho.UseVisualStyleBackColor = true;
            this.btnEcho.Click += new System.EventHandler(this.btnEcho_Click);
            // 
            // bgwListenerThread
            // 
            this.bgwListenerThread.WorkerSupportsCancellation = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(501, 389);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(230, 23);
            this.btnSaveSettings.TabIndex = 15;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnEcho);
            this.Controls.Add(this.gbSCP);
            this.Controls.Add(this.gbSCU);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.Text = "HRZ SDK TLS Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbSCU.ResumeLayout(false);
            this.gbSCU.PerformLayout();
            this.pnlSCUTLS.ResumeLayout(false);
            this.pnlSCUTLS.PerformLayout();
            this.gbSCP.ResumeLayout(false);
            this.tabsSCP.ResumeLayout(false);
            this.tabSCPRemore.ResumeLayout(false);
            this.tabSCPRemore.PerformLayout();
            this.tabSCPLocal.ResumeLayout(false);
            this.tabSCPLocal.PerformLayout();
            this.pnlSCPTLSLocal.ResumeLayout(false);
            this.pnlSCPTLSLocal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSCU;
        private System.Windows.Forms.GroupBox gbSCP;
        private System.Windows.Forms.Label lbSCUPort;
        private System.Windows.Forms.TextBox txtSCUPort;
        private System.Windows.Forms.Label lblCallingAE;
        private System.Windows.Forms.TextBox txtCallingAE;
        private System.Windows.Forms.RadioButton rbSCUThumbprint;
        private System.Windows.Forms.RadioButton rbSCUSubject;
        private System.Windows.Forms.CheckBox cbSCUTLS;
        private System.Windows.Forms.Panel pnlSCUTLS;
        private System.Windows.Forms.TextBox txtSCUThumbprint;
        private System.Windows.Forms.TextBox txtSCUSubjectName;
        private System.Windows.Forms.Label lblSCUStore;
        private System.Windows.Forms.ComboBox cbSCUStore;
        private System.Windows.Forms.TabControl tabsSCP;
        private System.Windows.Forms.TabPage tabSCPRemore;
        private System.Windows.Forms.TabPage tabSCPLocal;
        private System.Windows.Forms.Label lblSCPPortRemote;
        private System.Windows.Forms.TextBox txtSCPPortRemote;
        private System.Windows.Forms.Label lblCalledAERemote;
        private System.Windows.Forms.TextBox txtCalledAERemote;
        private System.Windows.Forms.Label lblSCPHostRemote;
        private System.Windows.Forms.TextBox txtSCPHostRemote;
        private System.Windows.Forms.CheckBox cbSCPTLSLocal;
        private System.Windows.Forms.Label lblSCPPortLocal;
        private System.Windows.Forms.TextBox txtSCPPortLocal;
        private System.Windows.Forms.Label lblCalledAELocal;
        private System.Windows.Forms.Panel pnlSCPTLSLocal;
        private System.Windows.Forms.RadioButton rbSCPThumbprint;
        private System.Windows.Forms.Label lblSCPStoreLocal;
        private System.Windows.Forms.RadioButton rbSCPSubject;
        private System.Windows.Forms.ComboBox cbSCPStoreLocal;
        private System.Windows.Forms.TextBox txtSCPThumbprint;
        private System.Windows.Forms.TextBox txtSCPSubject;
        private System.Windows.Forms.Label lblSCUMethod;
        private System.Windows.Forms.ComboBox cbSCUMethod;
        private System.Windows.Forms.Label lblSCUAccepted;
        private System.Windows.Forms.TextBox txtSCUAccepted;
        private System.Windows.Forms.Button btnSCUCert;
        private System.Windows.Forms.Button btnSCPCertLocal;
        private System.Windows.Forms.Label lblSCPAcceptedLocal;
        private System.Windows.Forms.TextBox txtSCPAcceptedLocal;
        private System.Windows.Forms.Label lblSCPMethodLocal;
        private System.Windows.Forms.ComboBox cbSCPMethodLocal;
        private System.Windows.Forms.CheckBox cbMutAuth;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnEcho;
        private System.ComponentModel.BackgroundWorker bgwListenerThread;
        private System.Windows.Forms.TextBox txtCalledAELocal;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}

