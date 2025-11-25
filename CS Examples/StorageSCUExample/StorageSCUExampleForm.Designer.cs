namespace StorageSCUExample
{
    partial class StorageSCUExampleForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FilePathEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.StoreBtn = new System.Windows.Forms.Button();
            this.certControl1 = new CertControl.CertControl();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Select a text file";
            // 
            // FilePathEdit
            // 
            this.FilePathEdit.Location = new System.Drawing.Point(70, 248);
            this.FilePathEdit.Name = "FilePathEdit";
            this.FilePathEdit.Size = new System.Drawing.Size(218, 20);
            this.FilePathEdit.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filename:";
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(294, 246);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.BrowseBtn.TabIndex = 2;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(91, 16);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 3;
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(91, 43);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(116, 20);
            this.TargetAEEdit.TabIndex = 4;
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(303, 16);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(116, 20);
            this.HostEdit.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Local AE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Target AE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "host:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(303, 43);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "port:";
            // 
            // StoreBtn
            // 
            this.StoreBtn.Location = new System.Drawing.Point(375, 246);
            this.StoreBtn.Name = "StoreBtn";
            this.StoreBtn.Size = new System.Drawing.Size(75, 23);
            this.StoreBtn.TabIndex = 11;
            this.StoreBtn.Text = "Store files";
            this.StoreBtn.UseVisualStyleBackColor = true;
            this.StoreBtn.Click += new System.EventHandler(this.StoreBtn_Click);
            // 
            // certControl1
            // 
            this.certControl1.Location = new System.Drawing.Point(12, 69);
            this.certControl1.Name = "certControl1";
            this.certControl1.Size = new System.Drawing.Size(423, 165);
            this.certControl1.TabIndex = 12;
            this.certControl1.use_mutualAuth = false;
            // 
            // StorageSCUExampleForm
            // 
            this.ClientSize = new System.Drawing.Size(504, 273);
            this.Controls.Add(this.certControl1);
            this.Controls.Add(this.StoreBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HostEdit);
            this.Controls.Add(this.TargetAEEdit);
            this.Controls.Add(this.LocalAEEdit);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilePathEdit);
            this.Name = "StorageSCUExampleForm";
            this.Text = "StorageSCU";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox FilePathEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button StoreBtn;
        private CertControl.CertControl certControl1;
    }
}

