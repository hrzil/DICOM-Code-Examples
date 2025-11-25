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
/// \section DicomObjectDemo Dicom Object Demo
/// Dicom Object Demo
/// This demo shows how to use the DCXOBJ object, put elements in it and
/// retrieve data from it

using System;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example DicomObjectExampleForm.cs
/// This demo shows how to use the DCXOBJ object, put elements in it and
/// retrieve data from it
namespace DicomObjectExample
{
    public partial class DicomObjectExampleForm : Form
    {
        private DCXOBJ obj = null;
        public DicomObjectExampleForm()
        {
            InitializeComponent();
        }

        ~DicomObjectExampleForm()
        {
            ReleaseComObject(obj);  
        }

        private void FillBtn_Click(object sender, EventArgs e)
        {
            DCXELM el = null;
            DCXAPP app = null;
            try
            {
                // Release the com object if it hasn't been released
                ReleaseComObject(obj);

                app = new DCXAPP();
                app.StartLogging("c:\\rzdcxLog.txt");

                obj = new DCXOBJ();
                el = new DCXELM();

                // Initialize the elements and put them in the object
                el.Init((int)rzdcxLib.DICOM_TAGS_ENUM.PatientsName);
                el.Value = PNameEdit.Text;
                obj.insertElement(el);

                el.Init((int)DICOM_TAGS_ENUM.PatientBirthTime);
                DateTime now = DateTime.Now;
                el.Value = now;
                obj.insertElement(el);

                el.Init((int)rzdcxLib.DICOM_TAGS_ENUM.patientID);
                el.Value = PIDEdit.Text;
                obj.insertElement(el);

                el.Init((int)rzdcxLib.DICOM_TAGS_ENUM.StudyID);
                el.Value = StudyIDEdit.Text;
                obj.insertElement(el);

                el.Init((int)rzdcxLib.DICOM_TAGS_ENUM.StudyDate);
                el.Value = StudyDateEdit.Text;
                obj.insertElement(el);

                el.Init((int)rzdcxLib.DICOM_TAGS_ENUM.seriesInstanceUID);
                el.Value = SeriesInstUIDEdit.Text;
                obj.insertElement(el);

                char[] delimiter = {' '};

                // Insert a sequence into the object
                DCXOBJIterator it = new DCXOBJIterator();
                for (int i = 0; i < 4; i++)
                {
                    string sopClass = "";
                    string sopInstance = "";

                    switch (i)
                    {
                        case 0:
                            {
                                sopClass = sopClass1.Text;
                                sopInstance = sopInst1.Text;
                                break;
                            }
                        case 1:
                            {
                                sopClass = sopClass2.Text;
                                sopInstance = sopInst2.Text;
                                break;
                            }
                        case 2:
                            {
                                sopClass = sopClass3.Text;
                                sopInstance = sopInst3.Text;
                                break;
                            }
                        case 3:
                            {
                                sopClass = sopClass4.Text;
                                sopInstance = sopInst4.Text;
                                break;
                            }
                        default:
                            break;
                    }

                    DCXOBJ currObj = new DCXOBJ();

                    el.Init(0x00081150);
                    el.Value = sopClass;
                    currObj.insertElement(el);

                    el.Init(0x00081155);
                    el.Value = sopInstance;
                    currObj.insertElement(el);

                    it.Insert(currObj);

                    ReleaseComObject(currObj);
                }

                el.Init(0x00081199);
                el.Value = it;
                obj.insertElement(el);

                ReleaseComObject(it);

                app.StopLogging();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill object failed: " + ex.Message);
            }
            finally
            {
                ReleaseComObject(el);
                ReleaseComObject(app);
            }
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            DCXELM el = null;
            try
            {
                // Retrieve all the elements data from the object

                string message = "";
                el = obj.GetElement(0x00100010);
                if (el != null)
                {
                    message += "Patient ID: " + el.Value + "\n";

                    ReleaseComObject(el);
                }

                el = obj.GetElement(0x00100020);
                if (el != null)
                {
                    message += "Patient ID: " + el.Value + "\n";

                    ReleaseComObject(el);
                }

                el = obj.GetElement(0x00200010);
                if (el != null)
                {
                    message += "Study ID: " + el.Value + "\n";

                    ReleaseComObject(el);
                }

                el = obj.GetElement(0x00080020);
                if (el != null)
                {
                    message += "StudyDate: " + el.Value + "\n";

                    ReleaseComObject(el);
                }

                el = obj.GetElement(0x0020000e);
                if (el != null)
                {
                    message += "Series Instance UID: " + el.Value + "\n";

                    ReleaseComObject(el);
                }

                // Retrieve the sequence from the object

                el = obj.GetElement(0x00081199);
                if (el != null)
                {
                    message += "Referenced SOP Sequence: \n";

                    DCXOBJIterator it = (DCXOBJIterator)el.Value;

                    for (; !it.AtEnd(); it.Next())
                    {
                        DCXOBJ currObj = it.Get();
                        if (currObj != null){
                            DCXELM currElem = currObj.GetElement(0x00081150);
                            if (currElem != null){
                                message += "SOP class uid: " + currElem.Value + " ";
                                ReleaseComObject(currElem);
                            }

                            currElem = currObj.GetElement(0x00081155);
                            if (currElem != null)
                            {
                                message += "SOP instance uid: " + currElem.Value + "\n";
                                ReleaseComObject(currElem);
                            }

                            ReleaseComObject(currObj);
                        }
                    }

                    ReleaseComObject(it);
                    ReleaseComObject(el);
                }
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill object failed: " + ex.Message);
            }
            finally
            {
                ReleaseComObject(el);
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DCXAPP app = new DCXAPP();
                try
                {
                    
                    app.LogLevel = LOG_LEVEL.LOG_LEVEL_DEBUG;
                    app.StartLogging("OpenFile.log");
                    app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "Calling DCXOBJ.openFile");
                    obj = new DCXOBJ();
                    obj.openFile(openFileDialog1.FileName);
                    app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "DCXOBJ.openFile returned with no error");
                    MessageBox.Show("File Loaded");
                }
                catch (COMException ex)
                {
                    app.WriteToLog(LOG_LEVEL.LOG_LEVEL_INFO, "DCXOBJ.openFile failed with error: " + ex.Message);
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    app.StopLogging();
                }
            }
        }
    }
}