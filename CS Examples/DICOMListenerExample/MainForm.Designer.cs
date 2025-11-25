namespace DICOMListener
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.lblError = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtStoragePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblListenerStatus = new System.Windows.Forms.Label();
            this.btnStopListener = new System.Windows.Forms.Button();
            this.btnStartListener = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAETitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.BackColor = System.Drawing.Color.FloralWhite;
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblError.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(21, 26);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(730, 100);
            this.lblError.TabIndex = 1;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.txtStoragePath);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblListenerStatus);
            this.groupBox2.Controls.Add(this.btnStopListener);
            this.groupBox2.Controls.Add(this.btnStartListener);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtAETitle);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox2.Location = new System.Drawing.Point(21, 148);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(733, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DICOM listener";
            // 
            // txtStoragePath
            // 
            this.txtStoragePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoragePath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStoragePath.ForeColor = System.Drawing.Color.Black;
            this.txtStoragePath.Location = new System.Drawing.Point(242, 148);
            this.txtStoragePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStoragePath.Name = "txtStoragePath";
            this.txtStoragePath.ReadOnly = true;
            this.txtStoragePath.Size = new System.Drawing.Size(376, 28);
            this.txtStoragePath.TabIndex = 13;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(624, 146);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(98, 35);
            this.btnBrowse.TabIndex = 14;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(14, 152);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 22);
            this.label6.TabIndex = 15;
            this.label6.Text = "Path to storage folder";
            // 
            // lblListenerStatus
            // 
            this.lblListenerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblListenerStatus.ForeColor = System.Drawing.Color.Black;
            this.lblListenerStatus.Location = new System.Drawing.Point(14, 40);
            this.lblListenerStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListenerStatus.Name = "lblListenerStatus";
            this.lblListenerStatus.Size = new System.Drawing.Size(306, 35);
            this.lblListenerStatus.TabIndex = 12;
            this.lblListenerStatus.Text = "Stopped";
            this.lblListenerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStopListener
            // 
            this.btnStopListener.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopListener.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopListener.ForeColor = System.Drawing.Color.Black;
            this.btnStopListener.Location = new System.Drawing.Point(536, 40);
            this.btnStopListener.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopListener.Name = "btnStopListener";
            this.btnStopListener.Size = new System.Drawing.Size(188, 35);
            this.btnStopListener.TabIndex = 2;
            this.btnStopListener.Text = "Stop listener";
            this.btnStopListener.UseVisualStyleBackColor = true;
            this.btnStopListener.Click += new System.EventHandler(this.btnStopListener_Click);
            // 
            // btnStartListener
            // 
            this.btnStartListener.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartListener.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartListener.ForeColor = System.Drawing.Color.Black;
            this.btnStartListener.Location = new System.Drawing.Point(340, 40);
            this.btnStartListener.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartListener.Name = "btnStartListener";
            this.btnStartListener.Size = new System.Drawing.Size(188, 35);
            this.btnStartListener.TabIndex = 1;
            this.btnStartListener.Text = "Start listener";
            this.btnStartListener.UseVisualStyleBackColor = true;
            this.btnStartListener.Click += new System.EventHandler(this.btnStartListener_Click);
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.ForeColor = System.Drawing.Color.Black;
            this.txtPort.Location = new System.Drawing.Point(624, 91);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.Mask = "00000";
            this.txtPort.Name = "txtPort";
            this.txtPort.PromptChar = ' ';
            this.txtPort.Size = new System.Drawing.Size(96, 29);
            this.txtPort.TabIndex = 4;
            this.txtPort.ValidatingType = typeof(int);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(564, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port ";
            // 
            // txtAETitle
            // 
            this.txtAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAETitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAETitle.ForeColor = System.Drawing.Color.Black;
            this.txtAETitle.Location = new System.Drawing.Point(242, 91);
            this.txtAETitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAETitle.Name = "txtAETitle";
            this.txtAETitle.Size = new System.Drawing.Size(284, 29);
            this.txtAETitle.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "AE Title";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(21, 374);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(733, 182);
            this.txtLog.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(21, 565);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Abort connections";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(769, 614);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblError);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(1189, 670);
            this.MinimumSize = new System.Drawing.Size(784, 670);
            this.Name = "MainForm";
            this.Text = "DICOM Listener";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAETitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.MaskedTextBox txtPort;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnStopListener;
        private System.Windows.Forms.Button btnStartListener;
        private System.Windows.Forms.Label lblListenerStatus;
        private System.Windows.Forms.TextBox txtStoragePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button1;
    }
}

