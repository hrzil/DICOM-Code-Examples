/*
 * 
 * Copyright (c) 2015, H.R.Z. SOftware Services LTD
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions 
 * are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
 *    in the documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from 
 *    this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

/// \page CSTestApplications C# Test Applications
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// \section DICOMImageExample DICOM Image Example
/// DICOM Image Example
/// This simple example demonstrates the use of the DCXIMG class to convert a DICOM
/// Image into bitmap file.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using rzdcxLib;

/// \example DICOMImageExample.cs
/// A C# convert DICOM Image into bitmap file example
namespace DICOMImageExample
{
    public partial class DICOMImageExample : Form
    {
        DCXOBJ obj;
        DCXIMG img = null;
        BitmapData bitmapData = null;
        Bitmap bitmap = null;
        Rectangle wholeBitmap;
        PixelFormat imagePixelFormat = PixelFormat.Format24bppRgb; // Known type.

        public DICOMImageExample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                dcmfile.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                bmpFile.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(dcmfile.Text))
                return;
            img = new DCXIMG();
            img.LoadFile(dcmfile.Text);

            Size imageSize = new Size(img.width, img.Height); // Known size.

            // Set bitmap known image's metadata.
            bitmap = new Bitmap(imageSize.Width, imageSize.Height, imagePixelFormat);

            // Prepare working rectangle.
            wholeBitmap = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            GetBitmap(0);

            pictureBox1.Image = bitmap;

            img.GetWindow(out center, out width);

            button5.Enabled = img != null;
        }

        int lastX;
        int lastY;
        double center;
        double width;
        bool mouseDown = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastX = e.X;
            lastY = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && img != null)
            {
                int diffX = e.X - lastX;
                int diffY = e.Y - lastY;

                width += diffX;
                center += diffY;

                if (width < 0)
                    width = 0;
                if (width > 65536)
                    width = 65536;

                if (center < 0)
                    center = 0;
                if (center > 65536)
                    center = 65536;

                img.SetWindow(center, width);

                GetBitmap(0);

                pictureBox1.Image = bitmap;

                lastX = e.X;
                lastY = e.Y;
            }
        }

        private unsafe Bitmap GetEncapsulatedFrame(int frameNo)
        {
            int length = obj.GetEncapsulatedFrameLength(frameNo);
            byte[] data = new byte[length];
            fixed (byte* p = data)
            {
                UIntPtr p1 = (UIntPtr)p;

#if _WIN64
                obj.GetEncapsulatedFrameData(frameNo, (ulong)p1, length);
#else
                obj.GetEncapsulatedFrameData(frameNo, (uint)p1, length);
#endif
            }
            MemoryStream s = new MemoryStream(data);
            return new Bitmap(s, false);
        }

        private unsafe void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists(dcmfile.Text))
                return;

            obj = new DCXOBJ();
            obj.openFile(dcmfile.Text);
            numFrames.Text = obj.FrameCount.ToString();
            curFrame.Text = "1";
            //if (useRefPtr.Checked)
            //{
            //    ulong data;
            //    int length = obj.GetEncapsulatedFrameRef(0, out data);
            //    MemoryStream s = new MemoryStream(data);
            //}
            //else
            //{
            try
            {
                bitmap = GetEncapsulatedFrame(0);

                pictureBox1.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //}
            //fixed (byte* p = values)
            //{
            //    UIntPtr p1 = (UIntPtr)p;
            //    e.Value = p1;
            //}
            //IntPtr
            //bitmap = new Bitmap(
        }

        private unsafe void next_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Int32.Parse(curFrame.Text);
                int n = Int32.Parse(numFrames.Text);
                if (i < n)
                {
                    i++;

                    bitmap = GetEncapsulatedFrame(i - 1);

                    pictureBox1.Image = bitmap;
                    curFrame.Text = i.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private unsafe void prev_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Int32.Parse(curFrame.Text);
                int n = Int32.Parse(numFrames.Text);
                if (i > 1)
                {
                    i--;

                    bitmap = GetEncapsulatedFrame(i - 1);

                    pictureBox1.Image = bitmap;
                    curFrame.Text = i.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool running = false;
        private void play_Click(object sender, EventArgs e)
        {
            if (running)
                timer1.Stop();
            else
                timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = Int32.Parse(curFrame.Text);
            int n = Int32.Parse(numFrames.Text);
            if (i >= n)
                i = 0;
            curFrame.Text = i.ToString();
            next_Click(sender, e);
        }

        /// <summary>
        /// Get bitmap of frame from ing
        /// </summary>
        /// <param name="frameNo">Frame number</param>
        /// <param name="img">DICOM Image</param>
        /// <param name="bitmap">Output bitmap</param>
        void GetBitmap(int frameNo, Rectangle rect)
        {
            if (img == null | bitmap == null)
                return;
                // Lock all bitmap's pixels.
            
            bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, imagePixelFormat);
#if _WIN64
            img.GetBitmap(0, bitmapData.Stride * bitmapData.Height, (ulong)bitmapData.Scan0);
#else
            img.GetBitmap(0, bitmapData.Stride * bitmapData.Height, (uint)bitmapData.Scan0);
#endif
            bitmap.UnlockBits(bitmapData);
        }

        void GetBitmap(int frameNo)
        {
            GetBitmap(frameNo, wholeBitmap);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (img != null)
            {
                img.GetHistogramWindow((double)numericUpDown1.Value, out center, out width);
                img.SetWindow(center, width);

                GetBitmap(0);

                pictureBox1.Image = bitmap;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
