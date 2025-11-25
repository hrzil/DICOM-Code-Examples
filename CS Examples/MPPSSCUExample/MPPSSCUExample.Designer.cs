namespace MPPSSCUExample
{
    partial class MPPSSCUExample
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
            this.components = new System.ComponentModel.Container();
            this.txtStudyUID = new System.Windows.Forms.TextBox();
            this.lblStudyUID = new System.Windows.Forms.Label();
            this.btnCreateStudyUID = new System.Windows.Forms.Button();
            this.txtPPSID = new System.Windows.Forms.TextBox();
            this.lblPPSID = new System.Windows.Forms.Label();
            this.txtAETitle = new System.Windows.Forms.TextBox();
            this.lblAETitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.rbInProgress = new System.Windows.Forms.RadioButton();
            this.rbCompleted = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbModality = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.rbDiscontinued = new System.Windows.Forms.RadioButton();
            this.txtRemoteHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemoteAE = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInstanceUID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCreate2 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textSentUID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStudyUID
            // 
            this.txtStudyUID.Location = new System.Drawing.Point(118, 13);
            this.txtStudyUID.Name = "txtStudyUID";
            this.txtStudyUID.Size = new System.Drawing.Size(307, 20);
            this.txtStudyUID.TabIndex = 0;
            this.txtStudyUID.Text = "1.2.3.4.5";
            // 
            // lblStudyUID
            // 
            this.lblStudyUID.AutoSize = true;
            this.lblStudyUID.Location = new System.Drawing.Point(12, 16);
            this.lblStudyUID.Name = "lblStudyUID";
            this.lblStudyUID.Size = new System.Drawing.Size(100, 13);
            this.lblStudyUID.TabIndex = 1;
            this.lblStudyUID.Text = "Study Instance UID";
            // 
            // btnCreateStudyUID
            // 
            this.btnCreateStudyUID.Location = new System.Drawing.Point(431, 11);
            this.btnCreateStudyUID.Name = "btnCreateStudyUID";
            this.btnCreateStudyUID.Size = new System.Drawing.Size(75, 23);
            this.btnCreateStudyUID.TabIndex = 2;
            this.btnCreateStudyUID.Text = "New UID";
            this.btnCreateStudyUID.UseVisualStyleBackColor = true;
            this.btnCreateStudyUID.Click += new System.EventHandler(this.btnCreateStudyUID_Click);
            // 
            // txtPPSID
            // 
            this.txtPPSID.Location = new System.Drawing.Point(118, 39);
            this.txtPPSID.Name = "txtPPSID";
            this.txtPPSID.Size = new System.Drawing.Size(307, 20);
            this.txtPPSID.TabIndex = 0;
            this.txtPPSID.Text = "1";
            // 
            // lblPPSID
            // 
            this.lblPPSID.AutoSize = true;
            this.lblPPSID.Location = new System.Drawing.Point(12, 42);
            this.lblPPSID.Name = "lblPPSID";
            this.lblPPSID.Size = new System.Drawing.Size(42, 13);
            this.lblPPSID.TabIndex = 1;
            this.lblPPSID.Text = "PPS ID";
            // 
            // txtAETitle
            // 
            this.txtAETitle.Location = new System.Drawing.Point(118, 65);
            this.txtAETitle.Name = "txtAETitle";
            this.txtAETitle.Size = new System.Drawing.Size(307, 20);
            this.txtAETitle.TabIndex = 0;
            this.txtAETitle.Text = "RDCX";
            // 
            // lblAETitle
            // 
            this.lblAETitle.AutoSize = true;
            this.lblAETitle.Location = new System.Drawing.Point(12, 68);
            this.lblAETitle.Name = "lblAETitle";
            this.lblAETitle.Size = new System.Drawing.Size(44, 13);
            this.lblAETitle.TabIndex = 1;
            this.lblAETitle.Text = "AE Title";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Date and Time";
            // 
            // dateStart
            // 
            this.dateStart.Location = new System.Drawing.Point(118, 90);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(307, 20);
            this.dateStart.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Status";
            // 
            // rbInProgress
            // 
            this.rbInProgress.AutoSize = true;
            this.rbInProgress.Location = new System.Drawing.Point(118, 118);
            this.rbInProgress.Name = "rbInProgress";
            this.rbInProgress.Size = new System.Drawing.Size(78, 17);
            this.rbInProgress.TabIndex = 4;
            this.rbInProgress.TabStop = true;
            this.rbInProgress.Text = "In Progress";
            this.rbInProgress.UseVisualStyleBackColor = true;
            // 
            // rbCompleted
            // 
            this.rbCompleted.AutoSize = true;
            this.rbCompleted.Location = new System.Drawing.Point(202, 118);
            this.rbCompleted.Name = "rbCompleted";
            this.rbCompleted.Size = new System.Drawing.Size(75, 17);
            this.rbCompleted.TabIndex = 4;
            this.rbCompleted.TabStop = true;
            this.rbCompleted.Text = "Completed";
            this.rbCompleted.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Modality";
            // 
            // cbModality
            // 
            this.cbModality.FormattingEnabled = true;
            this.cbModality.Items.AddRange(new object[] {
            "CR",
            "CT",
            "MR",
            "NM",
            "MG",
            "XA"});
            this.cbModality.Location = new System.Drawing.Point(118, 140);
            this.cbModality.Name = "cbModality";
            this.cbModality.Size = new System.Drawing.Size(307, 21);
            this.cbModality.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "End Date and Time";
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(118, 167);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(307, 20);
            this.dateEnd.TabIndex = 3;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(449, 384);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 6;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(287, 384);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.toolTip1.SetToolTip(this.btnCreate, "Create MPPS and get SOP Instance UID from SCP");
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button2_Click);
            // 
            // rbDiscontinued
            // 
            this.rbDiscontinued.AutoSize = true;
            this.rbDiscontinued.Location = new System.Drawing.Point(283, 118);
            this.rbDiscontinued.Name = "rbDiscontinued";
            this.rbDiscontinued.Size = new System.Drawing.Size(87, 17);
            this.rbDiscontinued.TabIndex = 4;
            this.rbDiscontinued.TabStop = true;
            this.rbDiscontinued.Text = "Discontinued";
            this.rbDiscontinued.UseVisualStyleBackColor = true;
            // 
            // txtRemoteHost
            // 
            this.txtRemoteHost.Location = new System.Drawing.Point(118, 262);
            this.txtRemoteHost.Name = "txtRemoteHost";
            this.txtRemoteHost.Size = new System.Drawing.Size(307, 20);
            this.txtRemoteHost.TabIndex = 0;
            this.txtRemoteHost.Text = "127.0.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Remote Host (or IP)";
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(118, 288);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(307, 20);
            this.txtRemotePort.TabIndex = 0;
            this.txtRemotePort.Text = "104";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Remote Port";
            // 
            // txtRemoteAE
            // 
            this.txtRemoteAE.AcceptsReturn = true;
            this.txtRemoteAE.Location = new System.Drawing.Point(118, 236);
            this.txtRemoteAE.Name = "txtRemoteAE";
            this.txtRemoteAE.Size = new System.Drawing.Size(307, 20);
            this.txtRemoteAE.TabIndex = 0;
            this.txtRemoteAE.Text = "TEST";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Remote AE Title";
            // 
            // txtInstanceUID
            // 
            this.txtInstanceUID.Enabled = false;
            this.txtInstanceUID.Location = new System.Drawing.Point(118, 314);
            this.txtInstanceUID.Name = "txtInstanceUID";
            this.txtInstanceUID.Size = new System.Drawing.Size(307, 20);
            this.txtInstanceUID.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Instance UID";
            // 
            // btnCreate2
            // 
            this.btnCreate2.Location = new System.Drawing.Point(368, 384);
            this.btnCreate2.Name = "btnCreate2";
            this.btnCreate2.Size = new System.Drawing.Size(75, 23);
            this.btnCreate2.TabIndex = 6;
            this.btnCreate2.Text = "Create 2";
            this.toolTip1.SetToolTip(this.btnCreate2, "Create MPPS and set SOP Instance UID explicitly");
            this.btnCreate2.UseVisualStyleBackColor = true;
            this.btnCreate2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textSentUID
            // 
            this.textSentUID.Enabled = false;
            this.textSentUID.Location = new System.Drawing.Point(118, 340);
            this.textSentUID.Name = "textSentUID";
            this.textSentUID.Size = new System.Drawing.Size(307, 20);
            this.textSentUID.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 343);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Sent UID";
            // 
            // MPPSSCUExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 419);
            this.Controls.Add(this.btnCreate2);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.cbModality);
            this.Controls.Add(this.rbDiscontinued);
            this.Controls.Add(this.rbCompleted);
            this.Controls.Add(this.rbInProgress);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreateStudyUID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblAETitle);
            this.Controls.Add(this.lblPPSID);
            this.Controls.Add(this.lblStudyUID);
            this.Controls.Add(this.textSentUID);
            this.Controls.Add(this.txtInstanceUID);
            this.Controls.Add(this.txtRemotePort);
            this.Controls.Add(this.txtRemoteHost);
            this.Controls.Add(this.txtRemoteAE);
            this.Controls.Add(this.txtAETitle);
            this.Controls.Add(this.txtPPSID);
            this.Controls.Add(this.txtStudyUID);
            this.Name = "MPPSSCUExample";
            this.Text = "TEST";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStudyUID;
        private System.Windows.Forms.Label lblStudyUID;
        private System.Windows.Forms.Button btnCreateStudyUID;
        private System.Windows.Forms.TextBox txtPPSID;
        private System.Windows.Forms.Label lblPPSID;
        private System.Windows.Forms.TextBox txtAETitle;
        private System.Windows.Forms.Label lblAETitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbInProgress;
        private System.Windows.Forms.RadioButton rbCompleted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbModality;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.RadioButton rbDiscontinued;
        private System.Windows.Forms.TextBox txtRemoteHost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemoteAE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInstanceUID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCreate2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textSentUID;
        private System.Windows.Forms.Label label9;
    }
}

