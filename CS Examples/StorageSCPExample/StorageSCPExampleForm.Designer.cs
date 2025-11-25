namespace StorageSCPExample
{
    partial class StorageSCPExampleForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.PortEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CallingAEEdit = new System.Windows.Forms.TextBox();
            this.ListenBtn = new System.Windows.Forms.Button();
            this.certControl1 = new CertControl.CertControl();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Listening port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(99, 32);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 28;
            this.PortEdit.Text = "104";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Local AE:";
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(99, 6);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 22;
            this.LocalAEEdit.Text = "B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Calling AE:";
            // 
            // CallingAEEdit
            // 
            this.CallingAEEdit.Location = new System.Drawing.Point(99, 58);
            this.CallingAEEdit.Name = "CallingAEEdit";
            this.CallingAEEdit.Size = new System.Drawing.Size(116, 20);
            this.CallingAEEdit.TabIndex = 30;
            this.CallingAEEdit.Text = "A";
            // 
            // ListenBtn
            // 
            this.ListenBtn.Location = new System.Drawing.Point(360, 4);
            this.ListenBtn.Name = "ListenBtn";
            this.ListenBtn.Size = new System.Drawing.Size(75, 23);
            this.ListenBtn.TabIndex = 32;
            this.ListenBtn.Text = "Listen";
            this.ListenBtn.UseVisualStyleBackColor = true;
            this.ListenBtn.Click += new System.EventHandler(this.ListenBtn_Click);
            // 
            // certControl1
            // 
            this.certControl1.Location = new System.Drawing.Point(12, 84);
            this.certControl1.Name = "certControl1";
            this.certControl1.Size = new System.Drawing.Size(423, 165);
            this.certControl1.TabIndex = 33;
            this.certControl1.use_mutualAuth = false;
            // 
            // StorageSCPExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 246);
            this.Controls.Add(this.certControl1);
            this.Controls.Add(this.ListenBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CallingAEEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LocalAEEdit);
            this.Name = "StorageSCPExampleForm";
            this.Text = "Storage SCP Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CallingAEEdit;
        private System.Windows.Forms.Button ListenBtn;
        private CertControl.CertControl certControl1;
    }
}

