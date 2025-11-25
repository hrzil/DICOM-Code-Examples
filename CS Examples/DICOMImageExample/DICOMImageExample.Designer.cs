namespace DICOMImageExample
{
    partial class DICOMImageExample
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dcmfile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bmpFile = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.useRefPtr = new System.Windows.Forms.CheckBox();
            this.prev = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.numFrames = new System.Windows.Forms.TextBox();
            this.curFrame = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.play = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button5 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dcmfile
            // 
            this.dcmfile.Location = new System.Drawing.Point(26, 25);
            this.dcmfile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dcmfile.Name = "dcmfile";
            this.dcmfile.Size = new System.Drawing.Size(900, 31);
            this.dcmfile.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(942, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open DICOM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(942, 104);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(228, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save Bitmap";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bmpFile
            // 
            this.bmpFile.Location = new System.Drawing.Point(24, 104);
            this.bmpFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.bmpFile.Name = "bmpFile";
            this.bmpFile.Size = new System.Drawing.Size(900, 31);
            this.bmpFile.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(28, 175);
            this.button3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 44);
            this.button3.TabIndex = 1;
            this.button3.Text = "Show Image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(28, 328);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1822, 908);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(190, 175);
            this.button4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(290, 44);
            this.button4.TabIndex = 3;
            this.button4.Text = "Show First JPEG Frame";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // useRefPtr
            // 
            this.useRefPtr.AutoSize = true;
            this.useRefPtr.Location = new System.Drawing.Point(546, 175);
            this.useRefPtr.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.useRefPtr.Name = "useRefPtr";
            this.useRefPtr.Size = new System.Drawing.Size(141, 29);
            this.useRefPtr.TabIndex = 4;
            this.useRefPtr.Text = "use ref ptr";
            this.useRefPtr.UseVisualStyleBackColor = true;
            // 
            // prev
            // 
            this.prev.Location = new System.Drawing.Point(744, 175);
            this.prev.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(104, 44);
            this.prev.TabIndex = 5;
            this.prev.Text = "Prev";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(878, 175);
            this.next.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(92, 44);
            this.next.TabIndex = 6;
            this.next.Text = "Next";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // numFrames
            // 
            this.numFrames.Location = new System.Drawing.Point(1222, 179);
            this.numFrames.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numFrames.Name = "numFrames";
            this.numFrames.Size = new System.Drawing.Size(120, 31);
            this.numFrames.TabIndex = 7;
            // 
            // curFrame
            // 
            this.curFrame.Location = new System.Drawing.Point(1050, 179);
            this.curFrame.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.curFrame.Name = "curFrame";
            this.curFrame.Size = new System.Drawing.Size(120, 31);
            this.curFrame.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1186, 185);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "/";
            // 
            // play
            // 
            this.play.Location = new System.Drawing.Point(1378, 175);
            this.play.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(150, 44);
            this.play.TabIndex = 10;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(28, 251);
            this.button5.Margin = new System.Windows.Forms.Padding(6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 44);
            this.button5.TabIndex = 11;
            this.button5.Text = "Histogram";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(190, 251);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(290, 31);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // DICOMImageExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1892, 1260);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.play);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.curFrame);
            this.Controls.Add(this.numFrames);
            this.Controls.Add(this.next);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.useRefPtr);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bmpFile);
            this.Controls.Add(this.dcmfile);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "DICOMImageExample";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox dcmfile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox bmpFile;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox useRefPtr;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.TextBox numFrames;
        private System.Windows.Forms.TextBox curFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

