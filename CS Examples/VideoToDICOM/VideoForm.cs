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
/// \section VideoToDICOM Video to DICOM Demo
/// Video To DICOM Demo. 
/// This demo shows how to use the RZDCX methods to create DICOM file containing embedded MPEG Video

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using rzdcxLib;

/// \example VideoForm.cs
/// A C# Video to DICOM Example

namespace VideoToDICOM
{
    public partial class VideoForm : Form
    {
        /// <summary>
        /// Format of the embedded video
        /// </summary>
        public enum VideoFormat
        {
            MPEG2 = 0,
            MPEG4,
        }

        /// <summary>
        /// Defines whether or not Windows Media Player is installed 
        /// if not - video files can not be converted in this example, because we need it to extract video properties 
        /// but other tools can be used for this purpose
        /// </summary>
        private bool IsMediaPlayerAvailable = false;

        public VideoForm()
        {
            InitializeComponent();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            this.CreateMediaPlayer();
            this.btnBrowse.Enabled = this.IsMediaPlayerAvailable;
            this.btnSaveAsDICOM.Enabled = this.IsMediaPlayerAvailable;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofd.Filter = "MPEG Video Files (mpg,mpeg,m2v,mp4,m4v)|*.mpg;*.mpeg;*.m2v;*.mp4;*.m4v|Data Files (dat)|*.dat";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtSelectedMPEGFile.Text = this.ofd.FileName;
            }
        }

        private void btnBrowseDICOM_Click(object sender, EventArgs e)
        {
            ofd.Filter = "DICOM Files (dcm)|*.dcm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtDICOMFile.Text = this.ofd.FileName;
            }
        }

        private void btnSaveAsDICOM_Click(object sender, EventArgs e)
        {
            if (this.txtSelectedMPEGFile.Text == "")
            {
                MessageBox.Show("Please select file to convert");
                return;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.ConverToDICOM();
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (this.txtDICOMFile.Text == "")
            {
                MessageBox.Show("Please select file to proceed");
                return;
            }

            this.ExtractVideo();
        }

        /// <summary>
        /// Try to create WMPLib.WindowsMediaPlayer object
        /// If succeded - set "Player available" flag to true allowing user to select MPEG files
        /// If not - set flag to false
        /// </summary>
        private void CreateMediaPlayer()
        {
            try
            {
                WindowsMediaPlayer wmp = new WindowsMediaPlayer();
                this.IsMediaPlayerAvailable = true;
            }
            catch (Exception)
            {
                //Media player is not installed
                this.IsMediaPlayerAvailable = false;
            }
        }

        private void InsertStringElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, string Value = "")
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);

            if (Value != "")
                el.Value = Value;

            obj.insertElement(el);

        }

        /// <summary>
        /// Extract all required info from selected MPEG file,
        /// create DICOM object and insert into it embedded video using RZDCX method
        /// and save DICOM file in selected place
        /// </summary>
        private void ConverToDICOM()
        {
            try
            {
                VideoFormat vf = VideoFormat.MPEG4;
                IWMPMedia mediainfo = null;
                double FrameDuration = 0;
                double FrameNumbers = 0;

               ENCAPSULATED_VIDEO_PROPS VIDEO_PROPS;
                if (!this.txtSelectedMPEGFile.Text.EndsWith(".dat"))
                {
                    //Try to extract video properties, if not succeeded - don't convert file
                    mediainfo = this.GetVideoProperties(this.txtSelectedMPEGFile.Text,
                                                        (new FileInfo(this.txtSelectedMPEGFile.Text)).Extension.ToUpper(),
                                                        ref vf, ref FrameDuration, ref FrameNumbers);

                    if (mediainfo == null)
                        return;

                    //Fill RZDCX structure for video converting
                    VIDEO_PROPS.FrameDurationMiliSec = (short)FrameDuration;
                    VIDEO_PROPS.Height = Convert.ToInt16(mediainfo.getItemInfo("VideoHeight"));
                    VIDEO_PROPS.width = Convert.ToInt16(mediainfo.getItemInfo("VideoWidth"));
                    VIDEO_PROPS.NumberOfFrames = (short)FrameNumbers;
                    VIDEO_PROPS.PixelAspectRatioX = Convert.ToInt16(mediainfo.getItemInfo("PixelAspectRatioX"));
                    VIDEO_PROPS.PixelAspectRatioY = Convert.ToInt16(mediainfo.getItemInfo("PixelAspectRatioY"));
                    VIDEO_PROPS.VideoFormat = (vf == VideoFormat.MPEG2 ? VIDEO_FORMAT.MPEG2_AT_MAIN_LEVEL : VIDEO_FORMAT.MPEG4);

               }
                else
                {
                    //Fill RZDCX structure for video converting
                    VIDEO_PROPS.FrameDurationMiliSec = 100;
                    VIDEO_PROPS.Height = 100;
                    VIDEO_PROPS.width = 100;
                    VIDEO_PROPS.NumberOfFrames = 100;
                    VIDEO_PROPS.PixelAspectRatioX = 1;
                    VIDEO_PROPS.PixelAspectRatioY = 1;
                    VIDEO_PROPS.VideoFormat = VIDEO_FORMAT.MPEG4;
                }

                //Create DCXOBJ and insert into it embedded video
                DCXOBJ output = new DCXOBJ();

                //Insert all required tags for example:
                DCXELM el = new DCXELM();
                el.Init((int)DICOM_TAGS_ENUM.StudyDescription);
                el.Value = "Convert MPEG to DICOM";
                output.insertElement(el);

                //////////////////////////////////////////////
                // Insert video to DICOM object
                //////////////////////////////////////////////
                output.SetVideoStream(this.txtSelectedMPEGFile.Text, VIDEO_PROPS);
                if (File.Exists(sfd.FileName)){
                    File.Delete(sfd.FileName);
                }

                 DCXUID id = new DCXUID();
               // Add SOP Instance UID (its missing???)
                //Insert all required tags for example:
                this.InsertStringElement(output, DICOM_TAGS_ENUM.SpecificCharacterSet, "ISO_IR 100");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.StudyDescription, "Convert RAW image to DICOM");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.PatientsName, "John^Doe");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.patientID, "123765");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.studyInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_STUDY));
                this.InsertStringElement(output, DICOM_TAGS_ENUM.seriesInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_SERIES));
                this.InsertStringElement(output, DICOM_TAGS_ENUM.sopInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_INSTANCE));

                //Save DCXOBJ as DICOM file
                output.saveFile(sfd.FileName);

                //Set saved file as input for extracting video from DICOM
                this.txtDICOMFile.Text = sfd.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Try to extract required info from given MPEG file (Frame rate, duration etc.)
        /// If not succeeded - return null to know that file can not be converted to DICOM
        /// If succeded - calculate frame duration and frame numbers 
        /// and return WMPLib.IWMPMedia object created from the file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="fExt"></param>
        /// <param name="vf"></param>
        /// <param name="FrameDuration"></param>
        /// <param name="FrameNumbers"></param>
        /// <returns></returns>
        private IWMPMedia GetVideoProperties(string FilePath, string fExt,
                                        ref VideoFormat vf, ref double FrameDuration, ref double FrameNumbers)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            IWMPMedia mediainfo = wmp.newMedia(FilePath);

            if (fExt.Contains("MP4") || fExt.Contains("M4V"))
                vf = VideoFormat.MPEG4;

            try
            {
                string s = mediainfo.getItemInfo("Framerate");
                double rate = Convert.ToDouble(s);
                FrameDuration = Math.Round((1000.0 / rate) * 1000.0, MidpointRounding.ToEven);

                s = mediainfo.getItemInfo("duration");
                double dur = Convert.ToDouble(s) * 1000.0;
                FrameNumbers = dur / FrameDuration;
                return mediainfo;
            }
            catch (Exception)
            {
                MessageBox.Show("Video properties can not be extracted from the file " + FilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Open selected DICOM file as DCXOBJ,
        /// check type of embedded video,
        /// extract video using RZDCX methods
        /// and save it as temporary file to open in system player
        /// </summary>
        private unsafe void ExtractVideo()
        {
            try
            {
                //Open DICOM file
                DCXOBJ dicomObject = new DCXOBJ();
                dicomObject.openFile(this.txtDICOMFile.Text);

                //Check what type of MPEG video contains
                string MPEGExt = ".mpg";
                if (dicomObject.FileMetaInfoExists)
                {
                    DCXELM el = dicomObject.FileMetaInfo.getElementByTag((int)DICOM_TAGS_ENUM.TransferSyntaxUID);
                    if (el != null && el.Value != null)
                    {
                        string TransferSyntaxUID = el.Value.ToString();
                        if ((TransferSyntaxUID == "1.2.840.10008.1.2.4.102") || (TransferSyntaxUID == "1.2.840.10008.1.2.4.103"))
                        {
                            MPEGExt = ".mp4";
                        }
                    }
                }

                //Create path to save extracted video
                string tmpVideoFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EXTRACTED_VIDEO");
                if (!Directory.Exists(tmpVideoFile))
                {
                    Directory.CreateDirectory(tmpVideoFile);
                }
                tmpVideoFile = Path.Combine(tmpVideoFile, new FileInfo(this.txtDICOMFile.Text).Name + MPEGExt);
                if (File.Exists(tmpVideoFile))
                {
                    File.Delete(tmpVideoFile);
                }

                //Extract video from DICOM and save as temporary file to open
                int len = dicomObject.GetEncapsulatedFrameLength(0);
                byte[] data = new byte[len];
                fixed (byte* p = data)
                {
                    UIntPtr p1 = (UIntPtr)p;
                    dicomObject.GetEncapsulatedFrameData(0, (uint)p1, len);
                }
                FileStream fs = new FileStream(tmpVideoFile, FileMode.Create, FileAccess.Write);
                fs.Write(data, 0, len);
                fs.Close();
                fs.Dispose();

                //Display video using system player
                System.Diagnostics.Process pr = System.Diagnostics.Process.Start(tmpVideoFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateFileBtn_Click(object sender, EventArgs e)
        {
            
            uint fileSize = uint.Parse(FileSizeCombo.SelectedItem.ToString().Split('_')[0]);
            uint prefixLength = 10000;
            uint contentLength = fileSize * 1024 * 1024 * 1024 - 2 * prefixLength;

            FileStream stream = null;
            BinaryWriter writer = null;

            sgf.Filter = "Data File|*.dat";
            sgf.FileName = FileSizeCombo.SelectedItem.ToString() + "ExampleDataFile";
            if (sgf.ShowDialog() == DialogResult.OK)
            {

                string fileName = sgf.FileName;
                MessageBox.Show("Generating a file...\n", "wait", MessageBoxButtons.OK);
                if (File.Exists(fileName))  File.Delete(fileName);

                stream = File.Create(fileName);
                writer = new BinaryWriter(stream);

                //for (long i = 0; i < prefixLength; i++)
                //{
                //    writer.Write((byte)0);
                //}

                for (uint i = 0; i < contentLength; i++)
                {
                    byte val = (byte)(i % 256);
                    writer.Write(val);
                }

                //for (long i = 0; i < prefixLength; i++)
                //{
                //    writer.Write((byte)0);
                //}

                writer.Close();
                stream.Close();
                MessageBox.Show("Finished!", "finished", MessageBoxButtons.OK);
            }
            return;
        }

        private void FileSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateFileBtn.Enabled = true;
        }
    }
}
