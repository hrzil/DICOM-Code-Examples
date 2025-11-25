namespace DICOMPresentationModuleExample
{
    partial class GSPSForm
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
            this.btnOpenPresentation = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.btnOpenDICOMToInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chOpenSaved = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInsertLine = new System.Windows.Forms.Button();
            this.chEllipseFilled = new System.Windows.Forms.CheckBox();
            this.btnInsertEllipse = new System.Windows.Forms.Button();
            this.chRectFilled = new System.Windows.Forms.CheckBox();
            this.btnInsertRectangle = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTLY = new System.Windows.Forms.TextBox();
            this.txtTLX = new System.Windows.Forms.TextBox();
            this.btnInsertText = new System.Windows.Forms.Button();
            this.txtAnnotation = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pnlBorders = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenPresentation
            // 
            this.btnOpenPresentation.Location = new System.Drawing.Point(6, 406);
            this.btnOpenPresentation.Name = "btnOpenPresentation";
            this.btnOpenPresentation.Size = new System.Drawing.Size(194, 23);
            this.btnOpenPresentation.TabIndex = 2;
            this.btnOpenPresentation.Text = "Open DICOM with presentation";
            this.btnOpenPresentation.UseVisualStyleBackColor = true;
            this.btnOpenPresentation.Click += new System.EventHandler(this.btnOpenPresentation_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // sfd
            // 
            this.sfd.Filter = "Presentation file | *.pre";
            // 
            // btnOpenDICOMToInsert
            // 
            this.btnOpenDICOMToInsert.Location = new System.Drawing.Point(6, 26);
            this.btnOpenDICOMToInsert.Name = "btnOpenDICOMToInsert";
            this.btnOpenDICOMToInsert.Size = new System.Drawing.Size(194, 23);
            this.btnOpenDICOMToInsert.TabIndex = 14;
            this.btnOpenDICOMToInsert.Text = "Open DICOM file to add presentation";
            this.btnOpenDICOMToInsert.UseVisualStyleBackColor = true;
            this.btnOpenDICOMToInsert.Click += new System.EventHandler(this.btnOpenDICOMToInsert_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 18);
            this.label1.TabIndex = 16;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.chOpenSaved);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnInsertLine);
            this.panel1.Controls.Add(this.chEllipseFilled);
            this.panel1.Controls.Add(this.btnInsertEllipse);
            this.panel1.Controls.Add(this.chRectFilled);
            this.panel1.Controls.Add(this.btnInsertRectangle);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtHeight);
            this.panel1.Controls.Add(this.txtWidth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTLY);
            this.panel1.Controls.Add(this.txtTLX);
            this.panel1.Controls.Add(this.btnInsertText);
            this.panel1.Controls.Add(this.txtAnnotation);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnOpenPresentation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnOpenDICOMToInsert);
            this.panel1.Location = new System.Drawing.Point(7, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 597);
            this.panel1.TabIndex = 17;
            // 
            // chOpenSaved
            // 
            this.chOpenSaved.AutoSize = true;
            this.chOpenSaved.Checked = true;
            this.chOpenSaved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chOpenSaved.Location = new System.Drawing.Point(74, 371);
            this.chOpenSaved.Name = "chOpenSaved";
            this.chOpenSaved.Size = new System.Drawing.Size(122, 17);
            this.chOpenSaved.TabIndex = 39;
            this.chOpenSaved.Text = "Open saved DICOM";
            this.chOpenSaved.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "EXAMPLES";
            // 
            // btnInsertLine
            // 
            this.btnInsertLine.Location = new System.Drawing.Point(8, 293);
            this.btnInsertLine.Name = "btnInsertLine";
            this.btnInsertLine.Size = new System.Drawing.Size(188, 23);
            this.btnInsertLine.TabIndex = 37;
            this.btnInsertLine.Text = "Insert diagonal line by boundaries";
            this.btnInsertLine.UseVisualStyleBackColor = true;
            this.btnInsertLine.Click += new System.EventHandler(this.btnInsertLine_Click);
            // 
            // chEllipseFilled
            // 
            this.chEllipseFilled.AutoSize = true;
            this.chEllipseFilled.Location = new System.Drawing.Point(152, 267);
            this.chEllipseFilled.Name = "chEllipseFilled";
            this.chEllipseFilled.Size = new System.Drawing.Size(50, 17);
            this.chEllipseFilled.TabIndex = 36;
            this.chEllipseFilled.Text = "Filled";
            this.chEllipseFilled.UseVisualStyleBackColor = true;
            // 
            // btnInsertEllipse
            // 
            this.btnInsertEllipse.Location = new System.Drawing.Point(8, 263);
            this.btnInsertEllipse.Name = "btnInsertEllipse";
            this.btnInsertEllipse.Size = new System.Drawing.Size(143, 23);
            this.btnInsertEllipse.TabIndex = 35;
            this.btnInsertEllipse.Text = "Insert ellipse by boundaries";
            this.btnInsertEllipse.UseVisualStyleBackColor = true;
            this.btnInsertEllipse.Click += new System.EventHandler(this.btnInsertEllipse_Click);
            // 
            // chRectFilled
            // 
            this.chRectFilled.AutoSize = true;
            this.chRectFilled.Location = new System.Drawing.Point(152, 237);
            this.chRectFilled.Name = "chRectFilled";
            this.chRectFilled.Size = new System.Drawing.Size(50, 17);
            this.chRectFilled.TabIndex = 34;
            this.chRectFilled.Text = "Filled";
            this.chRectFilled.UseVisualStyleBackColor = true;
            // 
            // btnInsertRectangle
            // 
            this.btnInsertRectangle.Location = new System.Drawing.Point(8, 233);
            this.btnInsertRectangle.Name = "btnInsertRectangle";
            this.btnInsertRectangle.Size = new System.Drawing.Size(143, 23);
            this.btnInsertRectangle.TabIndex = 33;
            this.btnInsertRectangle.Text = "Insert rectangle";
            this.btnInsertRectangle.UseVisualStyleBackColor = true;
            this.btnInsertRectangle.Click += new System.EventHandler(this.btnInsertRectangle_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Boundaries width    /   height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(112, 120);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(56, 20);
            this.txtHeight.TabIndex = 31;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(34, 120);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(50, 20);
            this.txtWidth.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Top/left point of boundaries";
            // 
            // txtTLY
            // 
            this.txtTLY.Location = new System.Drawing.Point(112, 75);
            this.txtTLY.Name = "txtTLY";
            this.txtTLY.Size = new System.Drawing.Size(56, 20);
            this.txtTLY.TabIndex = 21;
            // 
            // txtTLX
            // 
            this.txtTLX.Location = new System.Drawing.Point(34, 75);
            this.txtTLX.Name = "txtTLX";
            this.txtTLX.Size = new System.Drawing.Size(50, 20);
            this.txtTLX.TabIndex = 20;
            // 
            // btnInsertText
            // 
            this.btnInsertText.Location = new System.Drawing.Point(8, 203);
            this.btnInsertText.Name = "btnInsertText";
            this.btnInsertText.Size = new System.Drawing.Size(188, 23);
            this.btnInsertText.TabIndex = 19;
            this.btnInsertText.Text = "Insert text into  boundaries";
            this.btnInsertText.UseVisualStyleBackColor = true;
            this.btnInsertText.Click += new System.EventHandler(this.btnInsertText_Click);
            // 
            // txtAnnotation
            // 
            this.txtAnnotation.Location = new System.Drawing.Point(8, 176);
            this.txtAnnotation.Name = "txtAnnotation";
            this.txtAnnotation.Size = new System.Drawing.Size(188, 20);
            this.txtAnnotation.TabIndex = 18;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(5, 342);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(191, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save presentations";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.pnlBorders);
            this.pnlImage.Location = new System.Drawing.Point(216, 12);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(665, 597);
            this.pnlImage.TabIndex = 18;
            // 
            // pnlBorders
            // 
            this.pnlBorders.BackColor = System.Drawing.Color.Transparent;
            this.pnlBorders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBorders.Location = new System.Drawing.Point(3, 4);
            this.pnlBorders.Name = "pnlBorders";
            this.pnlBorders.Size = new System.Drawing.Size(200, 29);
            this.pnlBorders.TabIndex = 0;
            this.pnlBorders.Visible = false;
            // 
            // GSPSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(884, 612);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.panel1);
            this.Name = "GSPSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GSPSForm";
            this.Load += new System.EventHandler(this.GSPSForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenPresentation;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Button btnOpenDICOMToInsert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Panel pnlBorders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTLY;
        private System.Windows.Forms.TextBox txtTLX;
        private System.Windows.Forms.Button btnInsertText;
        private System.Windows.Forms.TextBox txtAnnotation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.CheckBox chRectFilled;
        private System.Windows.Forms.Button btnInsertRectangle;
        private System.Windows.Forms.CheckBox chEllipseFilled;
        private System.Windows.Forms.Button btnInsertEllipse;
        private System.Windows.Forms.Button btnInsertLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chOpenSaved;
    }
}