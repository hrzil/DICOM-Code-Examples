namespace PDFToDICOM
{
    partial class PDFForm
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
            this.btnExtract = new System.Windows.Forms.Button();
            this.txtDICOMFile = new System.Windows.Forms.TextBox();
            this.btnBrowseDICOM = new System.Windows.Forms.Button();
            this.btnSaveAsDICOM = new System.Windows.Forms.Button();
            this.txtSelectedPDFFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(442, 127);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(140, 23);
            this.btnExtract.TabIndex = 11;
            this.btnExtract.Text = "Extract PDF from DICOM";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // txtDICOMFile
            // 
            this.txtDICOMFile.Location = new System.Drawing.Point(125, 101);
            this.txtDICOMFile.Name = "txtDICOMFile";
            this.txtDICOMFile.ReadOnly = true;
            this.txtDICOMFile.Size = new System.Drawing.Size(457, 20);
            this.txtDICOMFile.TabIndex = 10;
            // 
            // btnBrowseDICOM
            // 
            this.btnBrowseDICOM.Location = new System.Drawing.Point(12, 98);
            this.btnBrowseDICOM.Name = "btnBrowseDICOM";
            this.btnBrowseDICOM.Size = new System.Drawing.Size(106, 23);
            this.btnBrowseDICOM.TabIndex = 9;
            this.btnBrowseDICOM.Text = "Select DICOM file";
            this.btnBrowseDICOM.UseVisualStyleBackColor = true;
            this.btnBrowseDICOM.Click += new System.EventHandler(this.btnBrowseDICOM_Click);
            // 
            // btnSaveAsDICOM
            // 
            this.btnSaveAsDICOM.Location = new System.Drawing.Point(442, 52);
            this.btnSaveAsDICOM.Name = "btnSaveAsDICOM";
            this.btnSaveAsDICOM.Size = new System.Drawing.Size(140, 23);
            this.btnSaveAsDICOM.TabIndex = 8;
            this.btnSaveAsDICOM.Text = "Convert to DICOM";
            this.btnSaveAsDICOM.UseVisualStyleBackColor = true;
            this.btnSaveAsDICOM.Click += new System.EventHandler(this.btnSaveAsDICOM_Click);
            // 
            // txtSelectedPDFFile
            // 
            this.txtSelectedPDFFile.Location = new System.Drawing.Point(125, 26);
            this.txtSelectedPDFFile.Name = "txtSelectedPDFFile";
            this.txtSelectedPDFFile.ReadOnly = true;
            this.txtSelectedPDFFile.Size = new System.Drawing.Size(457, 20);
            this.txtSelectedPDFFile.TabIndex = 7;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(12, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(106, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Select PDF file";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // sfd
            // 
            this.sfd.Filter = "DICOM Files (dcm)|*.dcm";
            // 
            // PDFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 177);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.txtDICOMFile);
            this.Controls.Add(this.btnBrowseDICOM);
            this.Controls.Add(this.btnSaveAsDICOM);
            this.Controls.Add(this.txtSelectedPDFFile);
            this.Controls.Add(this.btnBrowse);
            this.Name = "PDFForm";
            this.Text = "PDF to DICOM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.TextBox txtDICOMFile;
        private System.Windows.Forms.Button btnBrowseDICOM;
        private System.Windows.Forms.Button btnSaveAsDICOM;
        private System.Windows.Forms.TextBox txtSelectedPDFFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
    }
}

