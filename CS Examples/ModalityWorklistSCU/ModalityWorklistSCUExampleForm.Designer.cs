namespace ModalityWorklistSCU
{
    partial class ModalityWorklistSCUExampleForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StationNameEdit = new System.Windows.Forms.TextBox();
            this.QueryBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.dateStartDate = new System.Windows.Forms.DateTimePicker();
            this.dateEndDate = new System.Windows.Forms.DateTimePicker();
            this.checkDateTo = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkDateFrom = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dateExact = new System.Windows.Forms.DateTimePicker();
            this.checkDateExact = new System.Windows.Forms.CheckBox();
            this.timeTo = new System.Windows.Forms.DateTimePicker();
            this.timeExact = new System.Windows.Forms.DateTimePicker();
            this.timeFrom = new System.Windows.Forms.DateTimePicker();
            this.checkTimeExact = new System.Windows.Forms.CheckBox();
            this.checkTimeFrom = new System.Windows.Forms.CheckBox();
            this.checkTimeTo = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxModality = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvRP = new System.Windows.Forms.DataGridView();
            this.dgvSPS = new System.Windows.Forms.DataGridView();
            this.mppsStartBtn = new System.Windows.Forms.Button();
            this.mppsCompleteBtn = new System.Windows.Forms.Button();
            this.mppsAbortBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvMPPS = new System.Windows.Forms.DataGridView();
            this.btnBrowseForQuery = new System.Windows.Forms.Button();
            this.txtQueryFilePath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPS)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMPPS)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "From Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Scheduled Station:";
            // 
            // StationNameEdit
            // 
            this.StationNameEdit.Location = new System.Drawing.Point(110, 22);
            this.StationNameEdit.Name = "StationNameEdit";
            this.StationNameEdit.Size = new System.Drawing.Size(116, 20);
            this.StationNameEdit.TabIndex = 56;
            // 
            // QueryBtn
            // 
            this.QueryBtn.Location = new System.Drawing.Point(12, 162);
            this.QueryBtn.Name = "QueryBtn";
            this.QueryBtn.Size = new System.Drawing.Size(75, 23);
            this.QueryBtn.TabIndex = 55;
            this.QueryBtn.Text = "MWL Query";
            this.QueryBtn.UseVisualStyleBackColor = true;
            this.QueryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(119, 102);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 53;
            this.PortEdit.Text = "6104";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Target AE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Local AE:";
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(119, 75);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(116, 20);
            this.HostEdit.TabIndex = 49;
            this.HostEdit.Text = "localhost";
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(119, 48);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(116, 20);
            this.TargetAEEdit.TabIndex = 48;
            this.TargetAEEdit.Text = "JDICOM";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(119, 21);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 47;
            this.LocalAEEdit.Text = "RZDCX";
            // 
            // dateStartDate
            // 
            this.dateStartDate.Enabled = false;
            this.dateStartDate.Location = new System.Drawing.Point(110, 79);
            this.dateStartDate.Name = "dateStartDate";
            this.dateStartDate.Size = new System.Drawing.Size(176, 20);
            this.dateStartDate.TabIndex = 67;
            // 
            // dateEndDate
            // 
            this.dateEndDate.Enabled = false;
            this.dateEndDate.Location = new System.Drawing.Point(110, 107);
            this.dateEndDate.Name = "dateEndDate";
            this.dateEndDate.Size = new System.Drawing.Size(176, 20);
            this.dateEndDate.TabIndex = 67;
            // 
            // checkDateTo
            // 
            this.checkDateTo.AutoSize = true;
            this.checkDateTo.Location = new System.Drawing.Point(291, 110);
            this.checkDateTo.Name = "checkDateTo";
            this.checkDateTo.Size = new System.Drawing.Size(15, 14);
            this.checkDateTo.TabIndex = 68;
            this.checkDateTo.UseVisualStyleBackColor = true;
            this.checkDateTo.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "To Date:";
            // 
            // checkDateFrom
            // 
            this.checkDateFrom.AutoSize = true;
            this.checkDateFrom.Location = new System.Drawing.Point(291, 82);
            this.checkDateFrom.Name = "checkDateFrom";
            this.checkDateFrom.Size = new System.Drawing.Size(15, 14);
            this.checkDateFrom.TabIndex = 69;
            this.checkDateFrom.UseVisualStyleBackColor = true;
            this.checkDateFrom.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "Exact date:";
            // 
            // dateExact
            // 
            this.dateExact.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dateExact.Enabled = false;
            this.dateExact.Location = new System.Drawing.Point(110, 52);
            this.dateExact.Name = "dateExact";
            this.dateExact.Size = new System.Drawing.Size(176, 20);
            this.dateExact.TabIndex = 67;
            // 
            // checkDateExact
            // 
            this.checkDateExact.AutoSize = true;
            this.checkDateExact.Location = new System.Drawing.Point(291, 56);
            this.checkDateExact.Name = "checkDateExact";
            this.checkDateExact.Size = new System.Drawing.Size(15, 14);
            this.checkDateExact.TabIndex = 69;
            this.checkDateExact.UseVisualStyleBackColor = true;
            this.checkDateExact.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // timeTo
            // 
            this.timeTo.Enabled = false;
            this.timeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeTo.Location = new System.Drawing.Point(312, 107);
            this.timeTo.Name = "timeTo";
            this.timeTo.ShowUpDown = true;
            this.timeTo.Size = new System.Drawing.Size(82, 20);
            this.timeTo.TabIndex = 72;
            // 
            // timeExact
            // 
            this.timeExact.Enabled = false;
            this.timeExact.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeExact.Location = new System.Drawing.Point(312, 52);
            this.timeExact.Name = "timeExact";
            this.timeExact.ShowUpDown = true;
            this.timeExact.Size = new System.Drawing.Size(82, 20);
            this.timeExact.TabIndex = 71;
            // 
            // timeFrom
            // 
            this.timeFrom.Enabled = false;
            this.timeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeFrom.Location = new System.Drawing.Point(312, 79);
            this.timeFrom.Name = "timeFrom";
            this.timeFrom.ShowUpDown = true;
            this.timeFrom.Size = new System.Drawing.Size(82, 20);
            this.timeFrom.TabIndex = 70;
            // 
            // checkTimeExact
            // 
            this.checkTimeExact.AutoSize = true;
            this.checkTimeExact.Location = new System.Drawing.Point(402, 57);
            this.checkTimeExact.Name = "checkTimeExact";
            this.checkTimeExact.Size = new System.Drawing.Size(15, 14);
            this.checkTimeExact.TabIndex = 74;
            this.checkTimeExact.UseVisualStyleBackColor = true;
            this.checkTimeExact.CheckedChanged += new System.EventHandler(this.checkTimeExact_CheckedChanged);
            // 
            // checkTimeFrom
            // 
            this.checkTimeFrom.AutoSize = true;
            this.checkTimeFrom.Location = new System.Drawing.Point(402, 83);
            this.checkTimeFrom.Name = "checkTimeFrom";
            this.checkTimeFrom.Size = new System.Drawing.Size(15, 14);
            this.checkTimeFrom.TabIndex = 75;
            this.checkTimeFrom.UseVisualStyleBackColor = true;
            this.checkTimeFrom.CheckedChanged += new System.EventHandler(this.checkTimeFrom_CheckedChanged);
            // 
            // checkTimeTo
            // 
            this.checkTimeTo.AutoSize = true;
            this.checkTimeTo.Location = new System.Drawing.Point(402, 111);
            this.checkTimeTo.Name = "checkTimeTo";
            this.checkTimeTo.Size = new System.Drawing.Size(15, 14);
            this.checkTimeTo.TabIndex = 73;
            this.checkTimeTo.UseVisualStyleBackColor = true;
            this.checkTimeTo.CheckedChanged += new System.EventHandler(this.checkTimeTo_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.PortEdit);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.HostEdit);
            this.panel1.Controls.Add(this.TargetAEEdit);
            this.panel1.Controls.Add(this.LocalAEEdit);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 144);
            this.panel1.TabIndex = 76;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBoxModality);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.checkTimeExact);
            this.panel2.Controls.Add(this.checkTimeFrom);
            this.panel2.Controls.Add(this.checkTimeTo);
            this.panel2.Controls.Add(this.timeTo);
            this.panel2.Controls.Add(this.timeExact);
            this.panel2.Controls.Add(this.timeFrom);
            this.panel2.Controls.Add(this.checkDateExact);
            this.panel2.Controls.Add(this.checkDateFrom);
            this.panel2.Controls.Add(this.checkDateTo);
            this.panel2.Controls.Add(this.dateEndDate);
            this.panel2.Controls.Add(this.dateExact);
            this.panel2.Controls.Add(this.dateStartDate);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.StationNameEdit);
            this.panel2.Location = new System.Drawing.Point(265, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 144);
            this.panel2.TabIndex = 77;
            // 
            // comboBoxModality
            // 
            this.comboBoxModality.FormattingEnabled = true;
            this.comboBoxModality.Items.AddRange(new object[] {
            "CR",
            "US",
            "MR",
            "CT",
            "XA"});
            this.comboBoxModality.Location = new System.Drawing.Point(291, 22);
            this.comboBoxModality.Name = "comboBoxModality";
            this.comboBoxModality.Size = new System.Drawing.Size(126, 21);
            this.comboBoxModality.TabIndex = 78;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 77;
            this.label7.Text = "Modality:";
            // 
            // dgvRP
            // 
            this.dgvRP.AllowUserToAddRows = false;
            this.dgvRP.AllowUserToDeleteRows = false;
            this.dgvRP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRP.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvRP.Location = new System.Drawing.Point(0, 0);
            this.dgvRP.MultiSelect = false;
            this.dgvRP.Name = "dgvRP";
            this.dgvRP.ReadOnly = true;
            this.dgvRP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRP.Size = new System.Drawing.Size(689, 118);
            this.dgvRP.TabIndex = 78;
            this.dgvRP.SelectionChanged += new System.EventHandler(this.dgvQueryResults_SelectionChanged);
            // 
            // dgvSPS
            // 
            this.dgvSPS.AllowUserToAddRows = false;
            this.dgvSPS.AllowUserToDeleteRows = false;
            this.dgvSPS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSPS.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSPS.Location = new System.Drawing.Point(0, 0);
            this.dgvSPS.MultiSelect = false;
            this.dgvSPS.Name = "dgvSPS";
            this.dgvSPS.ReadOnly = true;
            this.dgvSPS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSPS.Size = new System.Drawing.Size(689, 170);
            this.dgvSPS.TabIndex = 79;
            this.dgvSPS.SelectionChanged += new System.EventHandler(this.dataGridSPS_SelectionChanged);
            // 
            // mppsStartBtn
            // 
            this.mppsStartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mppsStartBtn.Location = new System.Drawing.Point(12, 492);
            this.mppsStartBtn.Name = "mppsStartBtn";
            this.mppsStartBtn.Size = new System.Drawing.Size(75, 23);
            this.mppsStartBtn.TabIndex = 80;
            this.mppsStartBtn.Text = "Start";
            this.mppsStartBtn.UseVisualStyleBackColor = true;
            this.mppsStartBtn.Click += new System.EventHandler(this.mppsStartBtn_Click);
            // 
            // mppsCompleteBtn
            // 
            this.mppsCompleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mppsCompleteBtn.Location = new System.Drawing.Point(12, 614);
            this.mppsCompleteBtn.Name = "mppsCompleteBtn";
            this.mppsCompleteBtn.Size = new System.Drawing.Size(75, 23);
            this.mppsCompleteBtn.TabIndex = 80;
            this.mppsCompleteBtn.Text = "Complete";
            this.mppsCompleteBtn.UseVisualStyleBackColor = true;
            this.mppsCompleteBtn.Click += new System.EventHandler(this.mppsCompleteBtn_Click);
            // 
            // mppsAbortBtn
            // 
            this.mppsAbortBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mppsAbortBtn.Location = new System.Drawing.Point(93, 614);
            this.mppsAbortBtn.Name = "mppsAbortBtn";
            this.mppsAbortBtn.Size = new System.Drawing.Size(75, 23);
            this.mppsAbortBtn.TabIndex = 80;
            this.mppsAbortBtn.Text = "Abort";
            this.mppsAbortBtn.UseVisualStyleBackColor = true;
            this.mppsAbortBtn.Click += new System.EventHandler(this.mppsAbortBtn_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 497);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(262, 13);
            this.label8.TabIndex = 81;
            this.label8.Text = "Modality Performed Procedure Step (on selected SPS)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 191);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvRP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSPS);
            this.splitContainer1.Size = new System.Drawing.Size(689, 292);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.TabIndex = 82;
            // 
            // dgvMPPS
            // 
            this.dgvMPPS.AllowUserToAddRows = false;
            this.dgvMPPS.AllowUserToDeleteRows = false;
            this.dgvMPPS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMPPS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMPPS.Location = new System.Drawing.Point(15, 523);
            this.dgvMPPS.Name = "dgvMPPS";
            this.dgvMPPS.ReadOnly = true;
            this.dgvMPPS.Size = new System.Drawing.Size(686, 85);
            this.dgvMPPS.TabIndex = 83;
            // 
            // btnBrowseForQuery
            // 
            this.btnBrowseForQuery.Location = new System.Drawing.Point(623, 162);
            this.btnBrowseForQuery.Name = "btnBrowseForQuery";
            this.btnBrowseForQuery.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseForQuery.TabIndex = 84;
            this.btnBrowseForQuery.Text = "Browse";
            this.btnBrowseForQuery.UseVisualStyleBackColor = true;
            this.btnBrowseForQuery.Click += new System.EventHandler(this.btnBrowseForQuery_Click);
            // 
            // txtQueryFilePath
            // 
            this.txtQueryFilePath.Enabled = false;
            this.txtQueryFilePath.Location = new System.Drawing.Point(199, 163);
            this.txtQueryFilePath.Name = "txtQueryFilePath";
            this.txtQueryFilePath.Size = new System.Drawing.Size(415, 20);
            this.txtQueryFilePath.TabIndex = 85;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(96, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 86;
            this.label9.Text = "From saved query:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ModalityWorklistSCUExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 649);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtQueryFilePath);
            this.Controls.Add(this.btnBrowseForQuery);
            this.Controls.Add(this.dgvMPPS);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mppsAbortBtn);
            this.Controls.Add(this.mppsCompleteBtn);
            this.Controls.Add(this.mppsStartBtn);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.QueryBtn);
            this.Name = "ModalityWorklistSCUExampleForm";
            this.Text = "ModalityWorklistSCU";
            this.Load += new System.EventHandler(this.ModalityWorklistSCUExampleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPS)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMPPS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StationNameEdit;
        private System.Windows.Forms.Button QueryBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.DateTimePicker dateStartDate;
        private System.Windows.Forms.DateTimePicker dateEndDate;
        private System.Windows.Forms.CheckBox checkDateTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkDateFrom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateExact;
        private System.Windows.Forms.CheckBox checkDateExact;
        private System.Windows.Forms.DateTimePicker timeTo;
        private System.Windows.Forms.DateTimePicker timeExact;
        private System.Windows.Forms.DateTimePicker timeFrom;
        private System.Windows.Forms.CheckBox checkTimeExact;
        private System.Windows.Forms.CheckBox checkTimeFrom;
        private System.Windows.Forms.CheckBox checkTimeTo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxModality;

        private System.Windows.Forms.Button mppsStartBtn;
        private System.Windows.Forms.Button mppsCompleteBtn;
        private System.Windows.Forms.Button mppsAbortBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer splitContainer1;

        /**************** Data Grids *****************/

        // RP Grid
        private System.Windows.Forms.DataGridView dgvRP;
        private System.Windows.Forms.BindingSource bsRP = new System.Windows.Forms.BindingSource();

        // SPS Grid
        private System.Windows.Forms.DataGridView dgvSPS;
        private System.Windows.Forms.BindingSource bsSPS = new System.Windows.Forms.BindingSource();

        // MPPS Grid
        private System.Windows.Forms.DataGridView dgvMPPS;
        private System.Windows.Forms.BindingSource bsMPPS = new System.Windows.Forms.BindingSource();
        private System.Windows.Forms.Button btnBrowseForQuery;
        private System.Windows.Forms.TextBox txtQueryFilePath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

