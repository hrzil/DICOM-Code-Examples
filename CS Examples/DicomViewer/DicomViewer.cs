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
/// \section DicomViewer Dicom Viewer
/// This is a simple Dicom Viewer example which loads DICOM file and extracts image from it.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using rzdcxLib;

/// \example DicomViewer.cs
/// A C# Dicom Viewer
namespace DicomViewer
{
    public partial class DicomViewer : Form
    {
        public DicomViewer()
        {
            InitializeComponent();
        }

        private void OpenDicomFile()
        {
            string filename = openFileDialog1.FileName;

            try
            {
                BitmapData bitmapData;
                DCXIMG InitImg = new DCXIMG();
                InitImg.LoadFile(filename);

                Bitmap bit = new Bitmap(InitImg.width, InitImg.Height,PixelFormat.Format24bppRgb);
                // Lock all bitmap's pixels.
                bitmapData = bit.LockBits(new Rectangle(0, 0, InitImg.width, InitImg.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                InitImg.GetBitmap(0, bitmapData.Stride * bitmapData.Height, (uint)bitmapData.Scan0);

                bit.UnlockBits(bitmapData);

                this.pictureBox1.Image = bit;

                //Some form adjusting
                this.Width = bit.Width;
                this.Height = bit.Height + 65;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                OpenDicomFile();

        }
    }
}
