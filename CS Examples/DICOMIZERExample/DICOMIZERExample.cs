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
/// \section DICOMIZERExample DICOMIZER Demo
/// This is a simple DICOMIZER example.
/// It takes bitmaps or jpegs and make them DICOM, then send to PACS.
/// The objects that are created by this example are Secondary Captures.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using rzdcxLib;

/// \example DICOMIZERExample.cs
/// A C# JPEG/Bitmap to DICOM Converter
namespace DICOMIZERExample
{
    public partial class DICOMIZERExample : Form
    {
        public DICOMIZERExample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Borwse for input folder button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg Images (.jpg)|*.jpg|Bitmap Images (.bmp)|*.bmp";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmapsFolder.Text = openFileDialog1.FileNames.ToString();
            }
        }

        /// <summary>
        /// Browse for output button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dicomsFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Convert to DICOM button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (string bitmap in openFileDialog1.FileNames)
            {
                try
                {
                    convertAndSave(bitmap, i++);
                }
                catch (System.Runtime.InteropServices.COMException com_e)
                {
                    MessageBox.Show("Failed to convert file: " + bitmap +
                        "\nError message: " + com_e.Message, "Conversion Failed");
                }
            }
            MessageBox.Show("DICOM Files Creation Done.\nCheck the output folder.", "DICOMIZER");
        }

        /// <summary>
        /// To be on the safe side, create a true filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string ConvertToBitmap(string filename) 
        {
            string tempFileName = dicomsFolder.Text + Path.DirectorySeparatorChar + new FileInfo(filename).Name + ".bmp";
            Image img = Image.FromFile(filename);
            /// Because the images are soooooo biiiiiggg, lets make them a bit smaller
            /// You don't have to do that but better export smaller files
            int w = img.Width;
            int h = img.Height;
            while (h > 1000 || w > 1000) {
                w/=2;
                h/=2;
            }
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Bmp;
            if (radioDicomJpeg.Checked)
                format = System.Drawing.Imaging.ImageFormat.Jpeg;
            Image newImage = img.GetThumbnailImage(w, h, null, IntPtr.Zero);
            newImage.Save(tempFileName, format);
            //img.Save(tempFileName, System.Drawing.Imaging.ImageFormat.Bmp);
            return tempFileName;
        }
            
        /// <summary>
        /// Convert the filename file into a secondary capture file
        /// All files 
        /// </summary>
        /// <param name="filename"></param>
        private void convertAndSave(string bitmap, int intanceNum)
        {
            DCXOBJ o = new DCXOBJ();
            DCXELM e = new DCXELM();

            //////////////////////////////////////////////
            // Insert the image
            //////////////////////////////////////////////
            string tempFileName = ConvertToBitmap(bitmap);

            if (radioDicomJpeg.Checked)
                o.SetJpegFrames(tempFileName);
            else
                o.SetBMPFrames(tempFileName);

            File.Delete(tempFileName);

            //////////////////////////////////////////////
            // Insert all other info
            //////////////////////////////////////////////

            // Manufecturer
            e.Init((int)DICOM_TAGS_ENUM.Manufacturer);
            e.Value = "RZ - Software Services"; // Set to your company name
            o.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.ManufacturerModelName);
            e.Value = "RZDCX  Fast Strike DICOM Toolkit Test Applications"; // Set here your model name
            o.insertElement(e);

            // SOP Instance UID - The unique id of the image
            DCXUID uid = new DCXUID();
            e.Init((int)DICOM_TAGS_ENUM.sopInstanceUID);
            e.Value = uid.CreateUID(UID_TYPE.UID_TYPE_INSTANCE);
            o.insertElement(e);

            // Instance Number - can be zero lenght but we will number them by the order of the report
            e.Init((int)DICOM_TAGS_ENUM.InstanceNumber);
            e.Value = intanceNum;
            o.insertElement(e);

            //////////////////////////////////////////////
            // Patient info

            // Patient name
            e.Init((int)DICOM_TAGS_ENUM.patientName);
            e.Value = patName.Text;
            o.insertElement(e);

            // Patient ID
            e.Init((int)DICOM_TAGS_ENUM.patientID);
            e.Value = patId.Text;
            o.insertElement(e);

            // patient sex
            e.Init((int)DICOM_TAGS_ENUM.PatientSex);
            e.Value = "O"; // Don't know. Can be M - Male/F - Female/O - Other
            o.insertElement(e);

            // patient birth date
            e.Init((int)DICOM_TAGS_ENUM.PatientBirthDate);
            e.Value = ""; // Don't know. Format is YYYYMMDD
            o.insertElement(e);

            //////////////////////////////////////////////
            // Study info
            
            // Study Instance UID
            e.Init((int)DICOM_TAGS_ENUM.studyInstanceUID);
            e.Value = studyUid.Text; // Other
            o.insertElement(e);

            // Study Date
            e.Init((int)DICOM_TAGS_ENUM.StudyDate);
            e.Value = DateTime.Now; // Other
            o.insertElement(e);

            // Study Time
            e.Init((int)DICOM_TAGS_ENUM.StudyTime);
            e.Value = DateTime.Now; // Other
            o.insertElement(e);

            // Study Description
            e.Init((int)DICOM_TAGS_ENUM.StudyDescription);
            e.Value = "RZDCX - DICOMIZER Example - Study Description";
            o.insertElement(e);

            // Study ID - can be zero length (""). We put 1. You can number it as you like.
            e.Init((int)DICOM_TAGS_ENUM.StudyID);
            e.Value = "1";
            o.insertElement(e);

            // This number comes from the RIS. You can put it "" if you don't have the value
            // When integrating with Modality Worklist, you will have it
            e.Init((int)DICOM_TAGS_ENUM.AccessionNumber);
            e.Value = "";
            o.insertElement(e);

            // If you know it, put it in.
            e.Init((int)DICOM_TAGS_ENUM.ReferringPhysicianName);
            e.Value = "";
            o.insertElement(e);


            //////////////////////////////////////////////
            // Series info

            // Series Instance UID
            e.Init((int)DICOM_TAGS_ENUM.seriesInstanceUID);
            e.Value = seriesUid.Text;
            o.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.Modality);
            e.Value = "OT"; // Other
            o.insertElement(e);

            //DV = Digitized Video
            //DI = Digital Interface
            //DF = Digitized Film
            //WSD = Workstation
            //SD  = Scanned Document
            //SI  = Scanned Image
            //DRW   = Drawing
            //SYN  = Synthetic Image
            e.Init((int)DICOM_TAGS_ENUM.ConversionType);
            e.Value = "DRW"; 
            o.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.PatientOrientation);
            e.Value = ""; // No value
            o.insertElement(e);

            e.Init((int)DICOM_TAGS_ENUM.SeriesNumber);
            e.Value = "1"; // Can be with no value. We put 1 by default
            o.insertElement(e);

            //////////////////////////////////////////////
            // Save the file
            //////////////////////////////////////////////

            FileInfo fi = new FileInfo(bitmap);

            o.saveFile(dicomsFolder.Text + Path.DirectorySeparatorChar + fi.Name + ".dcm");
        }

        /// <summary>
        /// Create uid's for study and series when form loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DICOMIZERExample_Load(object sender, EventArgs e)
        {
            button4_Click(null, null);
            button5_Click(null, null);
        }

        /// <summary>
        /// Create study uid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            DCXUID uid = new DCXUID();
            studyUid.Text = uid.CreateUID(UID_TYPE.UID_TYPE_STUDY);
        }

        /// <summary>
        /// Create series UID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            DCXUID uid = new DCXUID();
            seriesUid.Text = uid.CreateUID(UID_TYPE.UID_TYPE_SERIES);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            DCXREQ requester = new DCXREQ();
            try
            {
                string succeededList;
                string failedList;
                string filesToSend="";
                foreach (string dicomFile in Directory.GetFiles(dicomsFolder.Text, "*.dcm", SearchOption.TopDirectoryOnly))
                {
                    filesToSend+=dicomFile;
                    filesToSend+=";";
                }

                requester.Send(LocalAEEdit.Text, TargetAEEdit.Text, HostEdit.Text, UInt16.Parse(PortEdit.Text), 
                    filesToSend, out succeededList, out failedList);

                MessageBox.Show("Send ended.\nSent files: " + succeededList + "\nFailed files: " + failedList, "C-STORE");
            }
            catch (System.Runtime.InteropServices.COMException com_e)
            {
                MessageBox.Show("Echo failed: " + com_e.Message, "C-ECHO");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DCXREQ requester = new DCXREQ();
            try
            {
                requester.Echo(LocalAEEdit.Text, TargetAEEdit.Text, HostEdit.Text, UInt16.Parse(PortEdit.Text));
                MessageBox.Show("Echo succeeded", "C-ECHO");
            }
            catch (System.Runtime.InteropServices.COMException com_e)
            {
                MessageBox.Show("Echo failed: " + com_e.Message, "C-ECHO");
            }  
        }

    }
}
