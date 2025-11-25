namespace StorageCommitSCUExample
{
    partial class StorageCommitSCUExampleForm
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
            this.CommitBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FilePathEdit = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // CommitBtn
            // 
            this.CommitBtn.Location = new System.Drawing.Point(98, 190);
            this.CommitBtn.Name = "CommitBtn";
            this.CommitBtn.Size = new System.Drawing.Size(75, 23);
            this.CommitBtn.TabIndex = 0;
            this.CommitBtn.Text = "Commit";
            this.CommitBtn.UseVisualStyleBackColor = true;
            this.CommitBtn.Click += new System.EventHandler(this.CommitBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(98, 91);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Target AE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Local AE:";
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(98, 64);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(116, 20);
            this.HostEdit.TabIndex = 16;
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(98, 37);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(116, 20);
            this.TargetAEEdit.TabIndex = 15;
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(98, 10);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 14;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(337, 131);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.BrowseBtn.TabIndex = 13;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Files to commit:";
            // 
            // FilePathEdit
            // 
            this.FilePathEdit.Location = new System.Drawing.Point(98, 135);
            this.FilePathEdit.Name = "FilePathEdit";
            this.FilePathEdit.Size = new System.Drawing.Size(218, 20);
            this.FilePathEdit.TabIndex = 11;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 273);
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
            this.Controls.Add(this.CommitBtn);
            this.Name = "Form1";
            this.Text = "Storage Commit SCU Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CommitBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilePathEdit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

