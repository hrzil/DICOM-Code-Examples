namespace DICOMIZERExample
{
    partial class DICOMIZERExample
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.bitmapsFolder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dicomsFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.EchoBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.patName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.patId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.accNo = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.studyUid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.seriesUid = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioDicomUncompressed = new System.Windows.Forms.RadioButton();
            this.radioDicomJpeg = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Images to convert";
            // 
            // bitmapsFolder
            // 
            this.bitmapsFolder.Enabled = false;
            this.bitmapsFolder.Location = new System.Drawing.Point(112, 94);
            this.bitmapsFolder.Name = "bitmapsFolder";
            this.bitmapsFolder.Size = new System.Drawing.Size(168, 20);
            this.bitmapsFolder.TabIndex = 1;
            this.bitmapsFolder.Text = "filenames";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(284, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dicomsFolder
            // 
            this.dicomsFolder.Enabled = false;
            this.dicomsFolder.Location = new System.Drawing.Point(112, 129);
            this.dicomsFolder.Name = "dicomsFolder";
            this.dicomsFolder.Size = new System.Drawing.Size(168, 20);
            this.dicomsFolder.TabIndex = 4;
            this.dicomsFolder.Text = "Where the DICOMs will be";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination Folder";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(15, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(344, 41);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "This example shows how to convert filename images to DICOM and then send them to " +
                "a PACS";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 357);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(342, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Convert to DICOM";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EchoBtn
            // 
            this.EchoBtn.Location = new System.Drawing.Point(21, 511);
            this.EchoBtn.Name = "EchoBtn";
            this.EchoBtn.Size = new System.Drawing.Size(338, 23);
            this.EchoBtn.TabIndex = 28;
            this.EchoBtn.Text = "Send to PACS";
            this.EchoBtn.UseVisualStyleBackColor = true;
            this.EchoBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 477);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(223, 474);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(44, 20);
            this.PortEdit.TabIndex = 26;
            this.PortEdit.Text = "5104";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 477);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 450);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Target AE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 423);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Local AE:";
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(77, 474);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(106, 20);
            this.HostEdit.TabIndex = 22;
            this.HostEdit.Text = "localhost";
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(77, 447);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(282, 20);
            this.TargetAEEdit.TabIndex = 21;
            this.TargetAEEdit.Text = "PACS";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(77, 420);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(282, 20);
            this.LocalAEEdit.TabIndex = 20;
            this.LocalAEEdit.Text = "RZDCX";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Enabled = false;
            this.richTextBox2.Location = new System.Drawing.Point(15, 60);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(344, 20);
            this.richTextBox2.TabIndex = 29;
            this.richTextBox2.Text = "Step 1: Convert to DICOM";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Enabled = false;
            this.richTextBox3.Location = new System.Drawing.Point(15, 388);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(344, 20);
            this.richTextBox3.TabIndex = 30;
            this.richTextBox3.Text = "Step 2: Send to PACS";
            // 
            // patName
            // 
            this.patName.Location = new System.Drawing.Point(112, 165);
            this.patName.Name = "patName";
            this.patName.Size = new System.Drawing.Size(247, 20);
            this.patName.TabIndex = 32;
            this.patName.Text = "Doe^John";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Patient Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Patient ID";
            // 
            // patId
            // 
            this.patId.Location = new System.Drawing.Point(112, 191);
            this.patId.Name = "patId";
            this.patId.Size = new System.Drawing.Size(247, 20);
            this.patId.TabIndex = 32;
            this.patId.Text = "123456789";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Accession Number";
            // 
            // accNo
            // 
            this.accNo.Location = new System.Drawing.Point(112, 217);
            this.accNo.Name = "accNo";
            this.accNo.Size = new System.Drawing.Size(247, 20);
            this.accNo.TabIndex = 32;
            this.accNo.Text = "99999";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(284, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 35;
            this.button4.Text = "Generate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // studyUid
            // 
            this.studyUid.Location = new System.Drawing.Point(112, 243);
            this.studyUid.Name = "studyUid";
            this.studyUid.Size = new System.Drawing.Size(168, 20);
            this.studyUid.TabIndex = 34;
            this.studyUid.Text = "Study Instance UID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Study Instance UID";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Series Instance UID";
            // 
            // seriesUid
            // 
            this.seriesUid.Location = new System.Drawing.Point(112, 269);
            this.seriesUid.Name = "seriesUid";
            this.seriesUid.Size = new System.Drawing.Size(168, 20);
            this.seriesUid.TabIndex = 34;
            this.seriesUid.Text = "Study Instance UID";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(284, 267);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 35;
            this.button5.Text = "Generate";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(284, 472);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 36;
            this.button6.Text = "Echo";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioDicomUncompressed);
            this.groupBox2.Controls.Add(this.radioDicomJpeg);
            this.groupBox2.Location = new System.Drawing.Point(17, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 51);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Format to save in DICOM";
            // 
            // radioDicomUncompressed
            // 
            this.radioDicomUncompressed.AutoSize = true;
            this.radioDicomUncompressed.Checked = true;
            this.radioDicomUncompressed.Location = new System.Drawing.Point(69, 19);
            this.radioDicomUncompressed.Name = "radioDicomUncompressed";
            this.radioDicomUncompressed.Size = new System.Drawing.Size(96, 17);
            this.radioDicomUncompressed.TabIndex = 38;
            this.radioDicomUncompressed.TabStop = true;
            this.radioDicomUncompressed.Text = "Uncompressed";
            this.radioDicomUncompressed.UseVisualStyleBackColor = true;
            // 
            // radioDicomJpeg
            // 
            this.radioDicomJpeg.AutoSize = true;
            this.radioDicomJpeg.Location = new System.Drawing.Point(8, 19);
            this.radioDicomJpeg.Name = "radioDicomJpeg";
            this.radioDicomJpeg.Size = new System.Drawing.Size(48, 17);
            this.radioDicomJpeg.TabIndex = 37;
            this.radioDicomJpeg.Text = "Jpeg";
            this.radioDicomJpeg.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DICOMIZERExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 549);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.seriesUid);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.studyUid);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.accNo);
            this.Controls.Add(this.patId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.patName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.EchoBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HostEdit);
            this.Controls.Add(this.TargetAEEdit);
            this.Controls.Add(this.LocalAEEdit);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dicomsFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bitmapsFolder);
            this.Controls.Add(this.label1);
            this.Name = "DICOMIZERExample";
            this.Text = "Bitmap to DICOM";
            this.Load += new System.EventHandler(this.DICOMIZERExample_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bitmapsFolder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox dicomsFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button EchoBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox patName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox patId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox accNo;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox studyUid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox seriesUid;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioDicomUncompressed;
        private System.Windows.Forms.RadioButton radioDicomJpeg;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

