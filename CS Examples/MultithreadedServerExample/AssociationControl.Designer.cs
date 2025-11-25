namespace MultithreadedServerExample
{
    partial class AssociationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCallingTitle = new System.Windows.Forms.Label();
            this.labelCallingHost = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCStoreCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCallingTitle
            // 
            this.labelCallingTitle.AutoSize = true;
            this.labelCallingTitle.Location = new System.Drawing.Point(93, 4);
            this.labelCallingTitle.Name = "labelCallingTitle";
            this.labelCallingTitle.Size = new System.Drawing.Size(61, 13);
            this.labelCallingTitle.TabIndex = 0;
            this.labelCallingTitle.Text = "Calling Title";
            // 
            // labelCallingHost
            // 
            this.labelCallingHost.AutoSize = true;
            this.labelCallingHost.Location = new System.Drawing.Point(282, 4);
            this.labelCallingHost.Name = "labelCallingHost";
            this.labelCallingHost.Size = new System.Drawing.Size(63, 13);
            this.labelCallingHost.TabIndex = 0;
            this.labelCallingHost.Text = "Calling Host";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Calling AE Title: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Calling Host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "C-STORE Requests:";
            // 
            // labelCStoreCount
            // 
            this.labelCStoreCount.AutoSize = true;
            this.labelCStoreCount.Location = new System.Drawing.Point(115, 27);
            this.labelCStoreCount.Name = "labelCStoreCount";
            this.labelCStoreCount.Size = new System.Drawing.Size(13, 13);
            this.labelCStoreCount.TabIndex = 1;
            this.labelCStoreCount.Text = "0";
            // 
            // AssociationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelCStoreCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCallingHost);
            this.Controls.Add(this.labelCallingTitle);
            this.Name = "AssociationControl";
            this.Size = new System.Drawing.Size(449, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCallingTitle;
        private System.Windows.Forms.Label labelCallingHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCStoreCount;
    }
}
