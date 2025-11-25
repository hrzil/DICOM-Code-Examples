namespace MultithreadedServerExample
{
    partial class MultithreadedServerExampleForm
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
            this.lblPort = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.lblAETitle = new System.Windows.Forms.Label();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.checkCalledAETitle = new System.Windows.Forms.CheckBox();
            this.numCommandWaitTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblCommandWaitTimeout = new System.Windows.Forms.Label();
            this.checkStartStop = new System.Windows.Forms.CheckBox();
            this.nextCStoreStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.activeAssociations = new System.Windows.Forms.Panel();
            this.labelStoreInFolder = new System.Windows.Forms.Label();
            this.textStoreFilesIn = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkAcceptAssoc = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCommandWaitTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 35);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 13);
            this.lblPort.TabIndex = 43;
            this.lblPort.Text = "PORT";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(76, 32);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 42;
            this.PortEdit.Text = "104";
            // 
            // lblAETitle
            // 
            this.lblAETitle.AutoSize = true;
            this.lblAETitle.Location = new System.Drawing.Point(12, 9);
            this.lblAETitle.Name = "lblAETitle";
            this.lblAETitle.Size = new System.Drawing.Size(54, 13);
            this.lblAETitle.TabIndex = 41;
            this.lblAETitle.Text = "AE TITLE";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(76, 6);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 40;
            this.LocalAEEdit.Text = "RZDEMO";
            // 
            // checkCalledAETitle
            // 
            this.checkCalledAETitle.AutoSize = true;
            this.checkCalledAETitle.Location = new System.Drawing.Point(199, 9);
            this.checkCalledAETitle.Name = "checkCalledAETitle";
            this.checkCalledAETitle.Size = new System.Drawing.Size(63, 17);
            this.checkCalledAETitle.TabIndex = 47;
            this.checkCalledAETitle.Text = "Enforce";
            this.checkCalledAETitle.UseVisualStyleBackColor = true;
            // 
            // numCommandWaitTimeout
            // 
            this.numCommandWaitTimeout.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCommandWaitTimeout.Location = new System.Drawing.Point(459, 6);
            this.numCommandWaitTimeout.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numCommandWaitTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCommandWaitTimeout.Name = "numCommandWaitTimeout";
            this.numCommandWaitTimeout.Size = new System.Drawing.Size(77, 20);
            this.numCommandWaitTimeout.TabIndex = 49;
            this.numCommandWaitTimeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numCommandWaitTimeout.ValueChanged += new System.EventHandler(this.numCommandWaitTimeout_ValueChanged);
            // 
            // lblCommandWaitTimeout
            // 
            this.lblCommandWaitTimeout.AutoSize = true;
            this.lblCommandWaitTimeout.Location = new System.Drawing.Point(307, 9);
            this.lblCommandWaitTimeout.Name = "lblCommandWaitTimeout";
            this.lblCommandWaitTimeout.Size = new System.Drawing.Size(146, 13);
            this.lblCommandWaitTimeout.TabIndex = 41;
            this.lblCommandWaitTimeout.Text = "COMMAND WAIT TIMEOUT";
            // 
            // checkStartStop
            // 
            this.checkStartStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkStartStop.Location = new System.Drawing.Point(12, 95);
            this.checkStartStop.Name = "checkStartStop";
            this.checkStartStop.Size = new System.Drawing.Size(180, 23);
            this.checkStartStop.TabIndex = 50;
            this.checkStartStop.Text = "Start/Stop";
            this.checkStartStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkStartStop.UseVisualStyleBackColor = true;
            this.checkStartStop.CheckedChanged += new System.EventHandler(this.checkStartStop_CheckedChanged);
            // 
            // nextCStoreStatus
            // 
            this.nextCStoreStatus.FormattingEnabled = true;
            this.nextCStoreStatus.Items.AddRange(new object[] {
            "0000",
            "A700",
            "B000",
            "B001",
            "C000",
            "C001"});
            this.nextCStoreStatus.Location = new System.Drawing.Point(459, 31);
            this.nextCStoreStatus.Name = "nextCStoreStatus";
            this.nextCStoreStatus.Size = new System.Drawing.Size(77, 21);
            this.nextCStoreStatus.TabIndex = 51;
            this.nextCStoreStatus.Text = "0000";
            this.nextCStoreStatus.SelectedIndexChanged += new System.EventHandler(this.nextCStoreStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "NEXT C-STORE STATUS";
            // 
            // activeAssociations
            // 
            this.activeAssociations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeAssociations.AutoScroll = true;
            this.activeAssociations.Location = new System.Drawing.Point(12, 124);
            this.activeAssociations.Name = "activeAssociations";
            this.activeAssociations.Size = new System.Drawing.Size(768, 437);
            this.activeAssociations.TabIndex = 52;
            // 
            // labelStoreInFolder
            // 
            this.labelStoreInFolder.AutoSize = true;
            this.labelStoreInFolder.Location = new System.Drawing.Point(13, 63);
            this.labelStoreInFolder.Name = "labelStoreInFolder";
            this.labelStoreInFolder.Size = new System.Drawing.Size(90, 13);
            this.labelStoreInFolder.TabIndex = 41;
            this.labelStoreInFolder.Text = "STORE FILES IN";
            // 
            // textStoreFilesIn
            // 
            this.textStoreFilesIn.Location = new System.Drawing.Point(109, 60);
            this.textStoreFilesIn.Name = "textStoreFilesIn";
            this.textStoreFilesIn.Size = new System.Drawing.Size(344, 20);
            this.textStoreFilesIn.TabIndex = 53;
            this.textStoreFilesIn.Text = "C:\\RZDCX";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(459, 58);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(77, 23);
            this.btnBrowse.TabIndex = 54;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // checkAcceptAssoc
            // 
            this.checkAcceptAssoc.Checked = true;
            this.checkAcceptAssoc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAcceptAssoc.Location = new System.Drawing.Point(199, 95);
            this.checkAcceptAssoc.Name = "checkAcceptAssoc";
            this.checkAcceptAssoc.Size = new System.Drawing.Size(105, 23);
            this.checkAcceptAssoc.TabIndex = 50;
            this.checkAcceptAssoc.Text = "Accept/Reject";
            this.checkAcceptAssoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkAcceptAssoc.UseVisualStyleBackColor = true;
            // 
            // MultithreadedServerExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textStoreFilesIn);
            this.Controls.Add(this.activeAssociations);
            this.Controls.Add(this.nextCStoreStatus);
            this.Controls.Add(this.checkAcceptAssoc);
            this.Controls.Add(this.checkStartStop);
            this.Controls.Add(this.numCommandWaitTimeout);
            this.Controls.Add(this.checkCalledAETitle);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.labelStoreInFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCommandWaitTimeout);
            this.Controls.Add(this.lblAETitle);
            this.Controls.Add(this.LocalAEEdit);
            this.Name = "MultithreadedServerExampleForm";
            this.Text = "Multithreaded DICOM Server Example";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MultithreadedServerExampleForm_FormClosed);
            this.Load += new System.EventHandler(this.MultithreadedServerExampleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCommandWaitTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label lblAETitle;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.CheckBox checkCalledAETitle;
        private System.Windows.Forms.NumericUpDown numCommandWaitTimeout;
        private System.Windows.Forms.Label lblCommandWaitTimeout;
        private System.Windows.Forms.CheckBox checkStartStop;
        private System.Windows.Forms.ComboBox nextCStoreStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel activeAssociations;
        private System.Windows.Forms.Label labelStoreInFolder;
        private System.Windows.Forms.TextBox textStoreFilesIn;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkAcceptAssoc;
    }
}

