namespace VideoToDICOM
{
    partial class VideoForm
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
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSelectedMPEGFile = new System.Windows.Forms.TextBox();
            this.btnSaveAsDICOM = new System.Windows.Forms.Button();
            this.btnBrowseDICOM = new System.Windows.Forms.Button();
            this.btnExtract = new System.Windows.Forms.Button();
            this.txtDICOMFile = new System.Windows.Forms.TextBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.FileSizeCombo = new System.Windows.Forms.ComboBox();
            this.CreateFileBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sgf = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(12, 67);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(106, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Select MPEG file";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSelectedMPEGFile
            // 
            this.txtSelectedMPEGFile.Location = new System.Drawing.Point(124, 70);
            this.txtSelectedMPEGFile.Name = "txtSelectedMPEGFile";
            this.txtSelectedMPEGFile.ReadOnly = true;
            this.txtSelectedMPEGFile.Size = new System.Drawing.Size(457, 20);
            this.txtSelectedMPEGFile.TabIndex = 1;
            // 
            // btnSaveAsDICOM
            // 
            this.btnSaveAsDICOM.Location = new System.Drawing.Point(442, 96);
            this.btnSaveAsDICOM.Name = "btnSaveAsDICOM";
            this.btnSaveAsDICOM.Size = new System.Drawing.Size(140, 23);
            this.btnSaveAsDICOM.TabIndex = 2;
            this.btnSaveAsDICOM.Text = "Convert to DICOM";
            this.btnSaveAsDICOM.UseVisualStyleBackColor = true;
            this.btnSaveAsDICOM.Click += new System.EventHandler(this.btnSaveAsDICOM_Click);
            // 
            // btnBrowseDICOM
            // 
            this.btnBrowseDICOM.Location = new System.Drawing.Point(12, 122);
            this.btnBrowseDICOM.Name = "btnBrowseDICOM";
            this.btnBrowseDICOM.Size = new System.Drawing.Size(106, 23);
            this.btnBrowseDICOM.TabIndex = 3;
            this.btnBrowseDICOM.Text = "Select DICOM file";
            this.btnBrowseDICOM.UseVisualStyleBackColor = true;
            this.btnBrowseDICOM.Click += new System.EventHandler(this.btnBrowseDICOM_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(442, 151);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(140, 23);
            this.btnExtract.TabIndex = 5;
            this.btnExtract.Text = "Extract video from DICOM";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // txtDICOMFile
            // 
            this.txtDICOMFile.Location = new System.Drawing.Point(124, 125);
            this.txtDICOMFile.Name = "txtDICOMFile";
            this.txtDICOMFile.ReadOnly = true;
            this.txtDICOMFile.Size = new System.Drawing.Size(457, 20);
            this.txtDICOMFile.TabIndex = 4;
            // 
            // sfd
            // 
            this.sfd.Filter = "DICOM Files (dcm)|*.dcm";
            // 
            // FileSizeCombo
            // 
            this.FileSizeCombo.FormattingEnabled = true;
            this.FileSizeCombo.Items.AddRange(new object[] {
            "1_GB",
            "2_GB",
            "3_GB",
            "4_GB"});
            this.FileSizeCombo.Location = new System.Drawing.Point(310, 37);
            this.FileSizeCombo.Name = "FileSizeCombo";
            this.FileSizeCombo.Size = new System.Drawing.Size(107, 21);
            this.FileSizeCombo.TabIndex = 7;
            this.FileSizeCombo.Text = "(Choose a filesize)";
            this.FileSizeCombo.SelectedIndexChanged += new System.EventHandler(this.FileSizeCombo_SelectedIndexChanged);
            // 
            // CreateFileBtn
            // 
            this.CreateFileBtn.Enabled = false;
            this.CreateFileBtn.Location = new System.Drawing.Point(442, 38);
            this.CreateFileBtn.Name = "CreateFileBtn";
            this.CreateFileBtn.Size = new System.Drawing.Size(140, 26);
            this.CreateFileBtn.TabIndex = 8;
            this.CreateFileBtn.Text = "Generate";
            this.CreateFileBtn.UseVisualStyleBackColor = true;
            this.CreateFileBtn.Click += new System.EventHandler(this.CreateFileBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Generate a dummy video file of size";
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 177);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreateFileBtn);
            this.Controls.Add(this.FileSizeCombo);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.txtDICOMFile);
            this.Controls.Add(this.btnBrowseDICOM);
            this.Controls.Add(this.btnSaveAsDICOM);
            this.Controls.Add(this.txtSelectedMPEGFile);
            this.Controls.Add(this.btnBrowse);
            this.Name = "VideoForm";
            this.Text = "MPEG to DICOM";
            this.Load += new System.EventHandler(this.VideoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSelectedMPEGFile;
        private System.Windows.Forms.Button btnSaveAsDICOM;
        private System.Windows.Forms.Button btnBrowseDICOM;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.TextBox txtDICOMFile;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ComboBox FileSizeCombo;
        private System.Windows.Forms.Button CreateFileBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog sgf;  // save generated file dialouge
    }
}

