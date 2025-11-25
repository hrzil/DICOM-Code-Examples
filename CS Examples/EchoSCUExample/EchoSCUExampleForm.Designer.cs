namespace EchoSCUExample
{
    partial class EchoSCUExampleForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HostEdit = new System.Windows.Forms.TextBox();
            this.TargetAEEdit = new System.Windows.Forms.TextBox();
            this.LocalAEEdit = new System.Windows.Forms.TextBox();
            this.EchoBtn = new System.Windows.Forms.Button();
            this.certControl1 = new CertControl.CertControl();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "port:";
            // 
            // PortEdit
            // 
            this.PortEdit.Location = new System.Drawing.Point(258, 32);
            this.PortEdit.Name = "PortEdit";
            this.PortEdit.Size = new System.Drawing.Size(116, 20);
            this.PortEdit.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Target AE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Local AE:";
            // 
            // HostEdit
            // 
            this.HostEdit.Location = new System.Drawing.Point(258, 6);
            this.HostEdit.Name = "HostEdit";
            this.HostEdit.Size = new System.Drawing.Size(116, 20);
            this.HostEdit.TabIndex = 13;
            // 
            // TargetAEEdit
            // 
            this.TargetAEEdit.Location = new System.Drawing.Point(71, 32);
            this.TargetAEEdit.Name = "TargetAEEdit";
            this.TargetAEEdit.Size = new System.Drawing.Size(116, 20);
            this.TargetAEEdit.TabIndex = 12;
            // 
            // LocalAEEdit
            // 
            this.LocalAEEdit.Location = new System.Drawing.Point(71, 6);
            this.LocalAEEdit.Name = "LocalAEEdit";
            this.LocalAEEdit.Size = new System.Drawing.Size(116, 20);
            this.LocalAEEdit.TabIndex = 11;
            // 
            // EchoBtn
            // 
            this.EchoBtn.Location = new System.Drawing.Point(421, 4);
            this.EchoBtn.Name = "EchoBtn";
            this.EchoBtn.Size = new System.Drawing.Size(60, 48);
            this.EchoBtn.TabIndex = 19;
            this.EchoBtn.Text = "Echo";
            this.EchoBtn.UseVisualStyleBackColor = true;
            this.EchoBtn.Click += new System.EventHandler(this.EchoBtn_Click);
            // 
            // certControl1
            // 
            this.certControl1.Location = new System.Drawing.Point(10, 58);
            this.certControl1.Name = "certControl1";
            this.certControl1.Size = new System.Drawing.Size(423, 165);
            this.certControl1.TabIndex = 20;
            this.certControl1.use_mutualAuth = false;
            // 
            // EchoSCUExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 227);
            this.Controls.Add(this.certControl1);
            this.Controls.Add(this.EchoBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PortEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HostEdit);
            this.Controls.Add(this.TargetAEEdit);
            this.Controls.Add(this.LocalAEEdit);
            this.Name = "EchoSCUExampleForm";
            this.Text = "EchoSCU";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostEdit;
        private System.Windows.Forms.TextBox TargetAEEdit;
        private System.Windows.Forms.TextBox LocalAEEdit;
        private System.Windows.Forms.Button EchoBtn;
        private CertControl.CertControl certControl1;
    }
}

