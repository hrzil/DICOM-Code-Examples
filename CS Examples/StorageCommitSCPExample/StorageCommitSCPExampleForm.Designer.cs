namespace StorageCommitSCPExample
{
    partial class StorageCommitSCPExampleForm
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
            this.ListenBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CallingAEEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListenBtn
            // 
            this.ListenBtn.Location = new System.Drawing.Point(89, 112);
            this.ListenBtn.Name = "ListenBtn";
            this.ListenBtn.Size = new System.Drawing.Size(75, 23);
            this.ListenBtn.TabIndex = 39;
            this.ListenBtn.Text = "Listen";
            this.ListenBtn.UseVisualStyleBackColor = true;
            this.ListenBtn.Click += new System.EventHandler(this.ListenBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Calling AE:";
            // 
            // CallingAEEdit
            // 
            this.CallingAEEdit.Location = new System.Drawing.Point(89, 64);
            this.CallingAEEdit.Name = "CallingAEEdit";
            this.CallingAEEdit.Size = new System.Drawing.Size(116, 20);
            this.CallingAEEdit.TabIndex = 37;
            this.CallingAEEdit.Text = "StorageSCU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Listening port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(89, 38);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 35;
            this.PortEdit.Text = "104";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Local AE:";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(89, 12);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 33;
            this.LocalAEEdit.Text = "RZDEMO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 233);
            this.Controls.Add(this.ListenBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CallingAEEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LocalAEEdit);
            this.Name = "Form1";
            this.Text = "StorageCommitSCPExample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ListenBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CallingAEEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocalAEEdit;

    }
}

