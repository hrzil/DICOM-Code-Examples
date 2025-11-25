namespace StructuredReportExample
{
    partial class StructuredReportExample
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
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCreateReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCreateReport.Location = new System.Drawing.Point(0, 0);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(292, 23);
            this.btnCreateReport.TabIndex = 0;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 23);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(292, 243);
            this.webBrowser1.TabIndex = 1;
            // 
            // StructuredReportExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnCreateReport);
            this.Name = "StructuredReportExample";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

