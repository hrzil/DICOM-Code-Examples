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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using rzdcxLib;

namespace DICOMPresentationModuleExample
{
    public partial class GSPSForm : Form
    {
        private DICOMPresentationModule _PresentationModule = null;
        private string LoadedDICOM = "";

        private Bitmap _currentBitmap;
        private string _currentSeries;
        private string _currentSOPClass;
        private string _currentSOPInst;
        private int _numberOfFrames;

        public GSPSForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On form loading - display initial position of "input" boundaries
        /// and attach text changed event to boundaries control text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GSPSForm_Load(object sender, EventArgs e)
        {
            this.txtTLX.Text = this.pnlBorders.Left.ToString();
            this.txtTLY.Text = this.pnlBorders.Top.ToString();

            this.txtWidth.Text = this.pnlBorders.Width.ToString();
            this.txtHeight.Text = this.pnlBorders.Height.ToString();

            this.txtWidth.TextChanged += new System.EventHandler(this.txtTLX_TextChanged);
            this.txtHeight.TextChanged += new System.EventHandler(this.txtTLX_TextChanged);
            this.txtTLY.TextChanged += new System.EventHandler(this.txtTLX_TextChanged);
            this.txtTLX.TextChanged += new System.EventHandler(this.txtTLX_TextChanged);
        }

        /// <summary>
        /// Update "input" boundaries size/position after control texts changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTLX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.pnlBorders.Left = Convert.ToInt32(this.txtTLX.Text);
                this.pnlBorders.Top = Convert.ToInt32(this.txtTLY.Text);

                this.pnlBorders.Width = Convert.ToInt32(this.txtWidth.Text);
                this.pnlBorders.Height = Convert.ToInt32(this.txtHeight.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid value");
            }
        }

        /// <summary>
        /// Select DICOM source file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDICOMToInsert_Click(object sender, EventArgs e)
        {
            if (this.ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenDICOMToAddPresentation(this.ofd.FileName);
            }
        }

        /// <summary>
        /// Add text user entered in the the input text box as text annotation
        /// to current presentation module (text area is current size/position of boundaries)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertText_Click(object sender, EventArgs e)
        {
            if (this._PresentationModule == null)
            {
                MessageBox.Show("Please open DICOM file to insert annotations");
                return;
            }

            if (this.txtAnnotation.Text.Trim() == "")
            {
                MessageBox.Show("Please enter text");
                return;
            }

            if (this._numberOfFrames == 1)
            {
                //If there is only one frame - insert annotation on "study level" (for demonstration purposes)
                this._PresentationModule.AddText(this.txtAnnotation.Text.Trim(),
                                                 new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                           "", "", "", -1);
            }
            else
            {
                //Insert annotation into first and last frames  (for demonstration purposes)
                this._PresentationModule.AddText(this.txtAnnotation.Text.Trim(),
                                 new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, 1);
                this._PresentationModule.AddText(this.txtAnnotation.Text.Trim(),
                                 new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, this._numberOfFrames);
            }

            this._PresentationModule.DrawOnBitmap(this._currentBitmap, this._currentSOPInst, 1);
            this.pnlImage.BackgroundImage = this._currentBitmap;
            this.Refresh();
        }

        /// <summary>
        /// Add graphic annotation of type rectangle
        /// to current presentation module (rectangle area is current size/position of boundaries)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertRectangle_Click(object sender, EventArgs e)
        {
            if (this._PresentationModule == null)
            {
                MessageBox.Show("Please open DICOM file to insert annotations");
                return;
            }

            if (this._numberOfFrames == 1)
            {
                //If there is only one frame - insert annotation on "study level" (for demonstration purposes)
                this._PresentationModule.AddRectangle(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chRectFilled.Checked, "", "", "", -1);
            }
            else
            {
                //Insert annotation into first and last frames  (for demonstration purposes)
                this._PresentationModule.AddRectangle(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chRectFilled.Checked,
                                                      this._currentSeries, this._currentSOPInst, this._currentSOPClass, 1);
                this._PresentationModule.AddRectangle(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chRectFilled.Checked,
                                                      this._currentSeries, this._currentSOPInst, this._currentSOPClass, this._numberOfFrames);
            }

            if (!this.chRectFilled.Checked)
            {
                this.pnlBorders.Left += 10;
                this.pnlBorders.Top += 10;
            }

            this._PresentationModule.DrawOnBitmap(this._currentBitmap, this._currentSOPInst, 1);
            this.pnlImage.BackgroundImage = this._currentBitmap;
            this.Refresh();
        }

        /// <summary>
        /// Add graphic annotation of type ellipse
        /// to current presentation module (ellipse area is current size/position of boundaries)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertEllipse_Click(object sender, EventArgs e)
        {
            if (this._PresentationModule == null)
            {
                MessageBox.Show("Please open DICOM file to insert annotations");
                return;
            }

            if (this._numberOfFrames == 1)
            {
                //If there is only one frame - insert annotation on "study level" (for demonstration purposes)
                this._PresentationModule.AddEllipse(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chEllipseFilled.Checked, "", "", "", -1);
            }
            else
            {
                //Insert annotation into first and last frames  (for demonstration purposes)
                this._PresentationModule.AddEllipse(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chEllipseFilled.Checked,
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, 1);
                this._PresentationModule.AddEllipse(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                      new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                                Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                      this.chEllipseFilled.Checked,
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, this._numberOfFrames);
            }

            this._PresentationModule.DrawOnBitmap(this._currentBitmap, this._currentSOPInst, 1);
            this.pnlImage.BackgroundImage = this._currentBitmap;
            this.Refresh();
        }

        /// <summary>
        /// Add graphic annotation of type line
        /// to current presentation module (line will be diagonal by current size/position of boundaries)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertLine_Click(object sender, EventArgs e)
        {
            if (this._PresentationModule == null)
            {
                MessageBox.Show("Please open DICOM file to insert annotations");
                return;
            }

            if (this._numberOfFrames == 1)
            {
                //If there is only one frame - insert annotation on "study level" (for demonstration purposes)
                this._PresentationModule.AddLine(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                                           "", "", "", -1);
            }
            else
            {
                //Insert annotation into first and last frames  (for demonstration purposes)
                this._PresentationModule.AddLine(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, 1);
                this._PresentationModule.AddLine(new Point(Convert.ToInt32(this.txtTLX.Text), Convert.ToInt32(this.txtTLY.Text)),
                                                 new Point(Convert.ToInt32(this.txtTLX.Text) + Convert.ToInt32(this.txtWidth.Text),
                                                           Convert.ToInt32(this.txtTLY.Text) + Convert.ToInt32(this.txtHeight.Text)),
                                           this._currentSeries, this._currentSOPInst, this._currentSOPClass, this._numberOfFrames);
            }


            this._PresentationModule.DrawOnBitmap(this._currentBitmap, this._currentSOPInst, 1);
            this.pnlImage.BackgroundImage = this._currentBitmap;
            this.Refresh();
        }

        /// <summary>
        /// Select place to store DICOM presentation file and copy its DICOM source file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (LoadedDICOM == "")
            {
                MessageBox.Show("Select DICOM");
                return;
            }

            if (this.sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.SavePresentation(this.sfd.FileName, this.chOpenSaved.Checked);
            }
        }

        /// <summary>
        /// Select DICOM file (not presentation!) for which presentation might exist
        /// Application will search for corresponding presentation file in the same folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenPresentation_Click(object sender, EventArgs e)
        {
            if (this.ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenDICOMWithPresentation(this.ofd.FileName);
            }
        }

        /// <summary>
        /// Open some source DICOM file (by user choice) to add annotations
        /// </summary>
        /// <param name="FileName"></param>
        private void OpenDICOMToAddPresentation(string FileName)
        {
            try
            {
                //Load source file into DCXImage object to extract image
                DCXIMG img = new DCXIMG();
                img.LoadFile(FileName);

                _currentBitmap = this.GetFirstFrameBitmap(img);

                //Set extracted image as display panel background
                this.pnlImage.BackgroundImage = _currentBitmap;
                this.pnlImage.Width = _currentBitmap.Width;
                this.pnlImage.Height = _currentBitmap.Height;
                this.LoadedDICOM = FileName;

                //Load source file into DCXOBJ object to extract required info about study etc.
                DCXOBJ objInput = new DCXOBJ();
                objInput.openFile(FileName);
                this._currentSeries = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.seriesInstanceUID);
                this._currentSOPInst = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.sopInstanceUID);
                this._currentSOPClass = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.sopClassUid);

                //Display image size
                this.label1.Text = "Dimension: " + _currentBitmap.Width.ToString() + "x" + _currentBitmap.Height.ToString() + " pixels";

                //Create new presentation module to store annotations
                _PresentationModule = new DICOMPresentationModule();


                //Set Presentation Module properties from source file
                _PresentationModule.StudyInstanceUID = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.studyInstanceUID);
                _PresentationModule.PatientID = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.patientID);

                //Set displayed area of the Presentation Module as entire image
                _PresentationModule.AddDisplaydArea(new PointF(0, 0), new PointF(this.pnlImage.Width, this.pnlImage.Height));

                //Try to extract number of frames for further inserting annotation to the last frame (for demonstration purposes)
                this._numberOfFrames = 1;
                string fn = DCXFunctions.GetElementValueAsString(objInput, (int)DICOM_TAGS_ENUM.NumberOfFrames);
                
                if (fn != "")
                {
                    _numberOfFrames = Convert.ToInt32(fn);
                }

                if (this._numberOfFrames == 1)
                {
                    //If there is only one frame - insert instance info on "study level" (for demonstration purposes)
                    //This method must be called for each image of the study if you want to add annotation of "study level"
                    this._PresentationModule.AddReferencedImage(this._currentSeries, this._currentSOPInst, this._currentSOPClass);
                }

                this.pnlBorders.Visible = true;
                this.btnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Save all created annotations into DICOM .pre file (path selected by user)
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="OpenSaved"></param>
        private void SavePresentation(string FileName, bool OpenSaved)
        {
            ///Example of how to add set of points into presentation module
            //for (int i = 0; i < 12; i++)
            //{
            //    this._PresentationModule.AddPoint(new Point(90 + i*20, 70));
            //}

            ///Example of how to add circle into presentation module
            ///First parameter is center of the circle
            ///Second - point on the circumference of a circle
            ///Like:  [Center: X1,Y]-----------[point on the circumference: X2,Y]
            //this._PresentationModule.AddCircle(new Point(200, 110), new Point(300, 110), false, "", "", "", -1);

            ///Example of how to add set of curved lines into presentation module
            //this._PresentationModule.AddInterpolated(new PointF[] { new PointF(64, 128), new PointF(96, 64), 
            //                                  new PointF(160, 64), new PointF(192, 128),
            //                                  new PointF(160, 192), new PointF(96, 192),
            //                                  new PointF(64, 128) }, false, "", "", "", -1);

            ///Example of how to add polygone into presentation module
            //this._PresentationModule.AddPolyline(new PointF[] { new PointF(1, 50),  
            //                                  new PointF(50, 1), 
            //                                  new PointF(100, 50), 
            //                                  new PointF(50, 100),
            //                                  new PointF(1, 50) }, true, "", "", "", -1);


            //Build DCXOBJ from all data stored in the presentation module
            DCXOBJ obj = new DCXOBJ();

            //Set some series UID for test purposes
            this._PresentationModule.SeriesInstanceUID = (new DCXUID()).CreateUID(UID_TYPE.UID_TYPE_SERIES);
            
            //Insert all info into DCXOBJ object
            this._PresentationModule.ToDICOM(obj);

            //Save DCXOBJ as file
            obj.saveFile(FileName);

            //Copy source DICOM file into the same place as .pre file
            //with name like [output].pre.dcm
            string CopyDICOM = sfd.FileName + ".dcm";
            File.Copy(this.LoadedDICOM, CopyDICOM, true);

            //Open saved [output].pre.dcm to display stored annotations
            if (OpenSaved)
                this.OpenDICOMWithPresentation(CopyDICOM);
        }

        /// <summary>
        /// Open DICOM file, search in the same folder for .pre file containing
        /// the same StudyUID, SeriesUID and SOPInstanceUID in referenced image sequence
        /// load presentation module and display annotation on the image
        /// </summary>
        /// <param name="FileName"></param>
        private void OpenDICOMWithPresentation(string FileName)
        {
            try
            {
                //Load input file into DCXOBJ object
                DCXOBJ obj = new DCXOBJ();
                obj.openFile(FileName);
                string DStudyUID = DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.studyInstanceUID);

                //Get all .pre files from the unput folder
                string[] files = Directory.GetFiles((new FileInfo(FileName)).DirectoryName, "*.pre", SearchOption.TopDirectoryOnly);
                bool Found = false;
                DICOMPresentationModule pm = null;
                foreach (string f in files)
                {
                    //Load .pre file into DCXOBJ object
                    DCXOBJ obj1 = new DCXOBJ();
                    obj1.openFile(f);

                    //Check that modality is "PR" in case file has "pre" extension but is not presentation module
                    if (DCXFunctions.GetElementValueAsString(obj1, (int)DICOM_TAGS_ENUM.Modality) == "PR")
                    {
                        if (DCXFunctions.GetElementValueAsString(obj1, (int)DICOM_TAGS_ENUM.studyInstanceUID) != DStudyUID)
                        {
                            //Study instance UID of .pre file is not equal to input, don't proceed
                            continue;
                        }

                        //Load Presentation Module from .pre file
                        pm = new DICOMPresentationModule();
                        pm.FromDICOM(obj1);

                        //Check whether series UID and SOP instance of the input file belong to referenced image of the Presentation Module
                        if (pm.IsMySOPInstance(DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.seriesInstanceUID),
                                               DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.sopInstanceUID)))
                        {
                            Found = true;
                            break;
                        }
                    }
                }

                if (!Found)
                {
                    MessageBox.Show("No presentation for this DICOM");
                    return;
                }

                //Load input file into DCXImage object to extract image
                DCXIMG img = new DCXIMG();
                img.LoadFile(FileName);
                Bitmap bm = this.GetFirstFrameBitmap(img);

                //Draw all annotations stored in the Presentation Module on the DICOM image (first frame)
                pm.DrawOnBitmap(bm, DCXFunctions.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.sopInstanceUID), 1);

                //Set image with annotations as display panel background
                this.pnlImage.BackgroundImage = bm;
                this.pnlImage.Width = bm.Width;
                this.pnlImage.Height = bm.Height;

                this.pnlBorders.Visible = false;
                this.btnSave.Enabled = false;

                this._PresentationModule = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Extract from DCXIMG one frame image as Bitmap object
        /// </summary>
        /// <param name="FrameNum"></param>
        /// <returns></returns>
        private Bitmap GetFirstFrameBitmap(DCXIMG img)
        {
            Bitmap bit = null;
            try
            {
                bit = new Bitmap((int)((float)img.width),
                                        (int)((float)img.Height),
                                        PixelFormat.Format24bppRgb);
                // Lock all bitmap's pixels.
                BitmapData bitmapData = bit.LockBits(new Rectangle(0, 0, img.width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                img.GetBitmap(0, bitmapData.Stride * bitmapData.Height, (uint)bitmapData.Scan0);

                bit.UnlockBits(bitmapData);

                return bit;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
