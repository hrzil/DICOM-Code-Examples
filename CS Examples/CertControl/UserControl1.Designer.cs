namespace CertControl
{
    partial class CertControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            this.cbUseTLS = new System.Windows.Forms.CheckBox();
            this.pnlSCPTLSLocal.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlSCPTLSLocal.Location = new System.Drawing.Point(4, 27);
            this.pnlSCPTLSLocal.Name = "pnlSCPTLSLocal";
            this.pnlSCPTLSLocal.Size = new System.Drawing.Size(413, 131);
            this.pnlSCPTLSLocal.TabIndex = 14;
            // 
            // cbMutAuth
            // 
            this.cbMutAuth.AutoSize = true;
            this.cbMutAuth.Location = new System.Drawing.Point(277, 82);
            this.cbMutAuth.Name = "cbMutAuth";
            this.cbMutAuth.Size = new System.Drawing.Size(129, 17);
            this.cbMutAuth.TabIndex = 17;
            this.cbMutAuth.Text = "Mutual Authentication";
            this.cbMutAuth.UseVisualStyleBackColor = true;
            // 
            // lblSCPAcceptedLocal
            // 
            this.lblSCPAcceptedLocal.AutoSize = true;
            this.lblSCPAcceptedLocal.Location = new System.Drawing.Point(5, 108);
            this.lblSCPAcceptedLocal.Name = "lblSCPAcceptedLocal";
            this.lblSCPAcceptedLocal.Size = new System.Drawing.Size(114, 13);
            this.lblSCPAcceptedLocal.TabIndex = 16;
            this.lblSCPAcceptedLocal.Text = "Accepted Thumbprints";
            // 
            // txtSCPAcceptedLocal
            // 
            this.txtSCPAcceptedLocal.Enabled = false;
            this.txtSCPAcceptedLocal.Location = new System.Drawing.Point(125, 105);
            this.txtSCPAcceptedLocal.Name = "txtSCPAcceptedLocal";
            this.txtSCPAcceptedLocal.Size = new System.Drawing.Size(281, 20);
            this.txtSCPAcceptedLocal.TabIndex = 15;
            // 
            // lblSCPMethodLocal
            // 
            this.lblSCPMethodLocal.AutoSize = true;
            this.lblSCPMethodLocal.Location = new System.Drawing.Point(4, 83);
            this.lblSCPMethodLocal.Name = "lblSCPMethodLocal";
            this.lblSCPMethodLocal.Size = new System.Drawing.Size(98, 13);
            this.lblSCPMethodLocal.TabIndex = 14;
            this.lblSCPMethodLocal.Text = "Verification Method";
            // 
            // cbSCPMethodLocal
            // 
            this.cbSCPMethodLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSCPMethodLocal.FormattingEnabled = true;
            this.cbSCPMethodLocal.Items.AddRange(new object[] {
            "None",
            "Trust Chain",
            "Thumbprints"});
            this.cbSCPMethodLocal.Location = new System.Drawing.Point(125, 80);
            this.cbSCPMethodLocal.Name = "cbSCPMethodLocal";
            this.cbSCPMethodLocal.Size = new System.Drawing.Size(121, 21);
            this.cbSCPMethodLocal.TabIndex = 13;
            this.cbSCPMethodLocal.SelectedIndexChanged += new System.EventHandler(this.cbSCPMethodLocal_SelectedIndexChanged);
            // 
            // btnSCPCertLocal
            // 
            this.btnSCPCertLocal.Location = new System.Drawing.Point(252, 9);
            this.btnSCPCertLocal.Name = "btnSCPCertLocal";
            this.btnSCPCertLocal.Size = new System.Drawing.Size(154, 21);
            this.btnSCPCertLocal.TabIndex = 12;
            this.btnSCPCertLocal.Text = "Browse";
            this.btnSCPCertLocal.UseVisualStyleBackColor = true;
            this.btnSCPCertLocal.Click += new System.EventHandler(this.btnSCPCertLocal_Click);
            // 
            // rbSCPThumbprint
            // 
            this.rbSCPThumbprint.AutoSize = true;
            this.rbSCPThumbprint.Location = new System.Drawing.Point(8, 58);
            this.rbSCPThumbprint.Name = "rbSCPThumbprint";
            this.rbSCPThumbprint.Size = new System.Drawing.Size(79, 17);
            this.rbSCPThumbprint.TabIndex = 6;
            this.rbSCPThumbprint.TabStop = true;
            this.rbSCPThumbprint.Text = "ThumbPrint";
            this.rbSCPThumbprint.UseVisualStyleBackColor = true;
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
            this.rbSCPSubject.Location = new System.Drawing.Point(8, 35);
            this.rbSCPSubject.Name = "rbSCPSubject";
            this.rbSCPSubject.Size = new System.Drawing.Size(92, 17);
            this.rbSCPSubject.TabIndex = 5;
            this.rbSCPSubject.TabStop = true;
            this.rbSCPSubject.Text = "Subject Name";
            this.rbSCPSubject.UseVisualStyleBackColor = true;
            this.rbSCPSubject.CheckedChanged += new System.EventHandler(this.rbSCPSubject_CheckedChanged);
            // 
            // cbSCPStoreLocal
            // 
            this.cbSCPStoreLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSCPStoreLocal.FormattingEnabled = true;
            this.cbSCPStoreLocal.Items.AddRange(new object[] {
            "Local Machine",
            "Current User"});
            this.cbSCPStoreLocal.Location = new System.Drawing.Point(125, 9);
            this.cbSCPStoreLocal.Name = "cbSCPStoreLocal";
            this.cbSCPStoreLocal.Size = new System.Drawing.Size(121, 21);
            this.cbSCPStoreLocal.TabIndex = 2;
            this.cbSCPStoreLocal.SelectedIndexChanged += new System.EventHandler(this.cbSCPStoreLocal_SelectedIndexChanged);
            // 
            // txtSCPThumbprint
            // 
            this.txtSCPThumbprint.Enabled = false;
            this.txtSCPThumbprint.Location = new System.Drawing.Point(125, 57);
            this.txtSCPThumbprint.Name = "txtSCPThumbprint";
            this.txtSCPThumbprint.Size = new System.Drawing.Size(281, 20);
            this.txtSCPThumbprint.TabIndex = 1;
            // 
            // txtSCPSubject
            // 
            this.txtSCPSubject.Location = new System.Drawing.Point(125, 34);
            this.txtSCPSubject.Name = "txtSCPSubject";
            this.txtSCPSubject.Size = new System.Drawing.Size(281, 20);
            this.txtSCPSubject.TabIndex = 0;
            // 
            // cbUseTLS
            // 
            this.cbUseTLS.AutoSize = true;
            this.cbUseTLS.Checked = true;
            this.cbUseTLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseTLS.Location = new System.Drawing.Point(4, 4);
            this.cbUseTLS.Name = "cbUseTLS";
            this.cbUseTLS.Size = new System.Drawing.Size(68, 17);
            this.cbUseTLS.TabIndex = 15;
            this.cbUseTLS.Text = "Use TLS";
            this.cbUseTLS.UseVisualStyleBackColor = true;
            this.cbUseTLS.CheckedChanged += new System.EventHandler(this.cbUseTLS_CheckedChanged);
            // 
            // CertControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbUseTLS);
            this.Controls.Add(this.pnlSCPTLSLocal);
            this.Name = "CertControl";
            this.Size = new System.Drawing.Size(423, 165);
            this.pnlSCPTLSLocal.ResumeLayout(false);
            this.pnlSCPTLSLocal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSCPTLSLocal;
        private System.Windows.Forms.CheckBox cbMutAuth;
        private System.Windows.Forms.Label lblSCPAcceptedLocal;
        private System.Windows.Forms.TextBox txtSCPAcceptedLocal;
        private System.Windows.Forms.Label lblSCPMethodLocal;
        private System.Windows.Forms.ComboBox cbSCPMethodLocal;
        private System.Windows.Forms.Button btnSCPCertLocal;
        private System.Windows.Forms.RadioButton rbSCPThumbprint;
        private System.Windows.Forms.Label lblSCPStoreLocal;
        private System.Windows.Forms.RadioButton rbSCPSubject;
        private System.Windows.Forms.ComboBox cbSCPStoreLocal;
        private System.Windows.Forms.TextBox txtSCPThumbprint;
        private System.Windows.Forms.TextBox txtSCPSubject;
        private System.Windows.Forms.CheckBox cbUseTLS;
    }
}
