namespace DICOMLoader
{
    partial class LoaderFrm
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
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnLoadFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadFiles = new System.Windows.Forms.Button();
            this.lbNotDICOM = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(158, 25);
            this.txtFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(768, 31);
            this.txtFolder.TabIndex = 0;
            // 
            // btnLoadFolder
            // 
            this.btnLoadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFolder.Location = new System.Drawing.Point(942, 21);
            this.btnLoadFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLoadFolder.Name = "btnLoadFolder";
            this.btnLoadFolder.Size = new System.Drawing.Size(146, 44);
            this.btnLoadFolder.TabIndex = 1;
            this.btnLoadFolder.Text = "Load folder";
            this.btnLoadFolder.UseVisualStyleBackColor = true;
            this.btnLoadFolder.Click += new System.EventHandler(this.btnLoadFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path to folder";
            // 
            // lbResults
            // 
            this.lbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbResults.FormattingEnabled = true;
            this.lbResults.ItemHeight = 25;
            this.lbResults.Location = new System.Drawing.Point(24, 214);
            this.lbResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(1060, 254);
            this.lbResults.TabIndex = 3;
            // 
            // fbd
            // 
            this.fbd.ShowNewFolderButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 167);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "List of SOP Instance UIDs";
            // 
            // ofd
            // 
            this.ofd.Multiselect = true;
            // 
            // btnLoadFiles
            // 
            this.btnLoadFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFiles.Location = new System.Drawing.Point(860, 106);
            this.btnLoadFiles.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLoadFiles.Name = "btnLoadFiles";
            this.btnLoadFiles.Size = new System.Drawing.Size(228, 44);
            this.btnLoadFiles.TabIndex = 5;
            this.btnLoadFiles.Text = "Select files to load";
            this.btnLoadFiles.UseVisualStyleBackColor = true;
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // lbNotDICOM
            // 
            this.lbNotDICOM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNotDICOM.FormattingEnabled = true;
            this.lbNotDICOM.ItemHeight = 25;
            this.lbNotDICOM.Location = new System.Drawing.Point(24, 493);
            this.lbNotDICOM.Margin = new System.Windows.Forms.Padding(6);
            this.lbNotDICOM.Name = "lbNotDICOM";
            this.lbNotDICOM.Size = new System.Drawing.Size(1060, 329);
            this.lbNotDICOM.TabIndex = 6;
            // 
            // LoaderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 846);
            this.Controls.Add(this.lbNotDICOM);
            this.Controls.Add(this.btnLoadFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.lbResults);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadFolder);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "LoaderFrm";
            this.Text = "DICOM Loader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnLoadFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbResults;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnLoadFiles;
        private System.Windows.Forms.ListBox lbNotDICOM;
    }
}

