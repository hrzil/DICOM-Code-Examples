namespace QueryRetrieveSCU
{
    partial class QueryRetrieveSCUExampleForm
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PatientNameEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PatientIDEdit = new System.Windows.Forms.TextBox();
            this.QueryBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.MoveBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textListenPort = new System.Windows.Forms.TextBox();
            this.textBoxSaveDir = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxRemaining = new System.Windows.Forms.TextBox();
            this.textBoxCompleted = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxFailed = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxWarning = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.radioMoveAndStore = new System.Windows.Forms.RadioButton();
            this.retreiveMethod = new System.Windows.Forms.GroupBox();
            this.radioCget = new System.Windows.Forms.RadioButton();
            this.radioOtherApp = new System.Windows.Forms.RadioButton();
            this.retreiveMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(94, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 20);
            this.label7.TabIndex = 60;
            this.label7.Text = "Query by patient";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Patient Name:";
            // 
            // PatientNameEdit
            // 
            this.PatientNameEdit.Location = new System.Drawing.Point(98, 212);
            this.PatientNameEdit.Name = "PatientNameEdit";
            this.PatientNameEdit.Size = new System.Drawing.Size(116, 20);
            this.PatientNameEdit.TabIndex = 58;
            this.PatientNameEdit.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Patient ID:";
            // 
            // PatientIDEdit
            // 
            this.PatientIDEdit.Location = new System.Drawing.Point(98, 186);
            this.PatientIDEdit.Name = "PatientIDEdit";
            this.PatientIDEdit.Size = new System.Drawing.Size(116, 20);
            this.PatientIDEdit.TabIndex = 56;
            this.PatientIDEdit.Text = "*";
            // 
            // QueryBtn
            // 
            this.QueryBtn.Location = new System.Drawing.Point(98, 252);
            this.QueryBtn.Name = "QueryBtn";
            this.QueryBtn.Size = new System.Drawing.Size(75, 23);
            this.QueryBtn.TabIndex = 55;
            this.QueryBtn.Text = "Query";
            this.QueryBtn.UseVisualStyleBackColor = true;
            this.QueryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(98, 117);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 53;
            this.PortEdit.Text = "104";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Target AE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Local AE:";
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(98, 90);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(116, 20);
            this.HostEdit.TabIndex = 49;
            this.HostEdit.Text = "LOCALHOST";
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(98, 63);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(116, 20);
            this.TargetAEEdit.TabIndex = 48;
            this.TargetAEEdit.Text = "DSRSVC";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(98, 36);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 47;
            this.LocalAEEdit.Text = "RZDCXTEST";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(273, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 61;
            this.label8.Text = "------>";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(364, 36);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(207, 139);
            this.checkedListBox1.TabIndex = 62;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(292, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Query results:";
            // 
            // MoveBtn
            // 
            this.MoveBtn.Location = new System.Drawing.Point(367, 251);
            this.MoveBtn.Name = "MoveBtn";
            this.MoveBtn.Size = new System.Drawing.Size(100, 23);
            this.MoveBtn.TabIndex = 64;
            this.MoveBtn.Text = "Retrieve Selected";
            this.MoveBtn.UseVisualStyleBackColor = true;
            this.MoveBtn.Click += new System.EventHandler(this.MoveBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(259, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "Listen on port:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(259, 218);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Store file in folder:";
            // 
            // textListenPort
            // 
            this.textListenPort.Location = new System.Drawing.Point(364, 186);
            this.textListenPort.Name = "textListenPort";
            this.textListenPort.Size = new System.Drawing.Size(60, 20);
            this.textListenPort.TabIndex = 53;
            this.textListenPort.Text = "3104";
            // 
            // textBoxSaveDir
            // 
            this.textBoxSaveDir.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBoxSaveDir.Location = new System.Drawing.Point(364, 215);
            this.textBoxSaveDir.Name = "textBoxSaveDir";
            this.textBoxSaveDir.Size = new System.Drawing.Size(132, 20);
            this.textBoxSaveDir.TabIndex = 67;
            this.textBoxSaveDir.Text = "c:\\tmp";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 329);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Remaining";
            // 
            // textBoxRemaining
            // 
            this.textBoxRemaining.Location = new System.Drawing.Point(88, 326);
            this.textBoxRemaining.Name = "textBoxRemaining";
            this.textBoxRemaining.Size = new System.Drawing.Size(42, 20);
            this.textBoxRemaining.TabIndex = 69;
            this.textBoxRemaining.Text = "0";
            // 
            // textBoxCompleted
            // 
            this.textBoxCompleted.Location = new System.Drawing.Point(199, 326);
            this.textBoxCompleted.Name = "textBoxCompleted";
            this.textBoxCompleted.Size = new System.Drawing.Size(42, 20);
            this.textBoxCompleted.TabIndex = 71;
            this.textBoxCompleted.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(136, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 70;
            this.label13.Text = "Completed";
            // 
            // textBoxFailed
            // 
            this.textBoxFailed.Location = new System.Drawing.Point(310, 326);
            this.textBoxFailed.Name = "textBoxFailed";
            this.textBoxFailed.Size = new System.Drawing.Size(42, 20);
            this.textBoxFailed.TabIndex = 73;
            this.textBoxFailed.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(273, 329);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Failed";
            // 
            // textBoxWarning
            // 
            this.textBoxWarning.Location = new System.Drawing.Point(421, 326);
            this.textBoxWarning.Name = "textBoxWarning";
            this.textBoxWarning.Size = new System.Drawing.Size(42, 20);
            this.textBoxWarning.TabIndex = 75;
            this.textBoxWarning.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(368, 329);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 74;
            this.label15.Text = "Warning";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(476, 329);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 74;
            this.label16.Text = "Status";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(529, 326);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(42, 20);
            this.textBoxStatus.TabIndex = 75;
            this.textBoxStatus.Text = "0";
            // 
            // radioMoveAndStore
            // 
            this.radioMoveAndStore.AutoSize = true;
            this.radioMoveAndStore.Checked = true;
            this.radioMoveAndStore.Location = new System.Drawing.Point(27, 36);
            this.radioMoveAndStore.Name = "radioMoveAndStore";
            this.radioMoveAndStore.Size = new System.Drawing.Size(144, 17);
            this.radioMoveAndStore.TabIndex = 76;
            this.radioMoveAndStore.TabStop = true;
            this.radioMoveAndStore.Text = "DCXREQ.MoveAndStore";
            this.radioMoveAndStore.UseVisualStyleBackColor = true;
            this.radioMoveAndStore.CheckedChanged += new System.EventHandler(this.radioMoveAndStore_CheckedChanged);
            // 
            // retreiveMethod
            // 
            this.retreiveMethod.Controls.Add(this.radioCget);
            this.retreiveMethod.Controls.Add(this.radioOtherApp);
            this.retreiveMethod.Controls.Add(this.radioMoveAndStore);
            this.retreiveMethod.Location = new System.Drawing.Point(502, 218);
            this.retreiveMethod.Name = "retreiveMethod";
            this.retreiveMethod.Size = new System.Drawing.Size(200, 100);
            this.retreiveMethod.TabIndex = 77;
            this.retreiveMethod.TabStop = false;
            this.retreiveMethod.Text = "Retreive Method";
            // 
            // radioCget
            // 
            this.radioCget.AutoSize = true;
            this.radioCget.Location = new System.Drawing.Point(27, 77);
            this.radioCget.Name = "radioCget";
            this.radioCget.Size = new System.Drawing.Size(57, 17);
            this.radioCget.TabIndex = 78;
            this.radioCget.TabStop = true;
            this.radioCget.Text = "C-GET";
            this.radioCget.UseVisualStyleBackColor = true;
            this.radioCget.CheckedChanged += new System.EventHandler(this.radioCget_CheckedChanged);
            // 
            // radioOtherApp
            // 
            this.radioOtherApp.AutoSize = true;
            this.radioOtherApp.Location = new System.Drawing.Point(27, 56);
            this.radioOtherApp.Name = "radioOtherApp";
            this.radioOtherApp.Size = new System.Drawing.Size(130, 17);
            this.radioOtherApp.TabIndex = 77;
            this.radioOtherApp.TabStop = true;
            this.radioOtherApp.Text = "Move (to another app)";
            this.radioOtherApp.UseVisualStyleBackColor = true;
            this.radioOtherApp.CheckedChanged += new System.EventHandler(this.radioOtherApp_CheckedChanged);
            // 
            // QueryRetrieveSCUExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 384);
            this.Controls.Add(this.retreiveMethod);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxWarning);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBoxFailed);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxCompleted);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxRemaining);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxSaveDir);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.MoveBtn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.PatientNameEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PatientIDEdit);
            this.Controls.Add(this.QueryBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textListenPort);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HostEdit);
            this.Controls.Add(this.TargetAEEdit);
            this.Controls.Add(this.LocalAEEdit);
            this.Name = "QueryRetrieveSCUExampleForm";
            this.Text = "QueryRetrieveSCU";
            this.retreiveMethod.ResumeLayout(false);
            this.retreiveMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox PatientNameEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PatientIDEdit;
        private System.Windows.Forms.Button QueryBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button MoveBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textListenPort;
        private System.Windows.Forms.TextBox textBoxSaveDir;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxRemaining;
        private System.Windows.Forms.TextBox textBoxCompleted;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxFailed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxWarning;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.RadioButton radioMoveAndStore;
        private System.Windows.Forms.GroupBox retreiveMethod;
        private System.Windows.Forms.RadioButton radioCget;
        private System.Windows.Forms.RadioButton radioOtherApp;
    }
}

