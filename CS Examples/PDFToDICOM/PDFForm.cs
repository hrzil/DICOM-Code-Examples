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
/// \section PDFToDICOM PDF to DICOM Demo
/// PDF To DICOM Demo. 
/// This demo shows how to use the RZDCX methods to create DICOM file containing embedded PDF

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using rzdcxLib;

/// \example PDFForm.cs
/// A C# PDF to DICOM Example

namespace PDFToDICOM
{
    public partial class PDFForm : Form
    {
        public PDFForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofd.Filter = "PDF Files (pdf)|*.pdf";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtSelectedPDFFile.Text = this.ofd.FileName;
            }
        }

        private void btnSaveAsDICOM_Click(object sender, EventArgs e)
        {
            if (this.txtSelectedPDFFile.Text == "")
            {
                MessageBox.Show("Please select file to convert");
                return;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.ConverToDICOM();
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

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (this.txtDICOMFile.Text == "")
            {
                MessageBox.Show("Please select file to proceed");
                return;
            }

            this.ExtractPDF();
        }

        /// <summary>
        /// Create DICOM object and insert into it encapsulated PDF using RZDCX method
        /// and save DICOM file in selected place
        /// </summary>
        private unsafe void ConverToDICOM()
        {
            try
            {
                //Create DCXOBJ and insert into it encapsulated PDF
                DCXOBJ output = new DCXOBJ();

                //Insert all required tags for example:
                this.InsertElement(output, DICOM_TAGS_ENUM.StudyDescription, "Convert MPEG to DICOM");

                //Insert tags defining file as containing encapsulated PDF
                this.InsertElement(output, DICOM_TAGS_ENUM.SpecificCharacterSet, "ISO_IR 100");
                this.InsertElement(output, DICOM_TAGS_ENUM.sopClassUid, "1.2.840.10008.5.1.4.1.1.104.1");
                this.InsertElement(output, DICOM_TAGS_ENUM.ConversionType, "WSD");
                this.InsertElement(output, DICOM_TAGS_ENUM.MIMETypeOfEncapsulatedDocument, "application/pdf");
                this.InsertElement(output, DICOM_TAGS_ENUM.BurnedInAnnotation, "YES");

                //Insert encapsulated PDF
                DCXELM e = new DCXELM();
                e.Init((int)DICOM_TAGS_ENUM.EncapsulatedDocument);

                System.IO.FileInfo pdfFileInfo = new System.IO.FileInfo(this.txtSelectedPDFFile.Text);
                long len = pdfFileInfo.Length;

                //Check PDF file length
                if (len % 2 > 0)
                {
                    byte[] b = new byte[len + 1];
                    byte[] b1 = System.IO.File.ReadAllBytes(this.txtSelectedPDFFile.Text);
                    b1.CopyTo(b, 0);
                    b[len] = 0;
                    len++;

                    e.Length = (uint)len;
                    fixed (byte* p = b)
                    {
                        UIntPtr p1 = (UIntPtr)p;
                        e.Value = p1;
                    }
                }
                else
                {
                    e.SetValueFromFile(this.txtSelectedPDFFile.Text, 0, (int)len);
                }

                output.insertElement(e);

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
        /// Insert one DXCELM object into given DCXOBJ
        /// If Value parameter is not empty string - set it as element value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <param name="Value"></param>
        private void InsertElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, string Value = "")
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);

            if (Value != "")
                el.Value = Value;

            obj.insertElement(el);

        }

        /// <summary>
        /// Open selected DICOM file as DCXOBJ,
        /// check that it contains encapsulated PDF,
        /// extract PDF using RZDCX methods
        /// and save it as temporary file to open in system reader
        /// </summary>
        private void ExtractPDF()
        {
            try
            {
                //Open DICOM file
                DCXOBJ dicomObject = new DCXOBJ();
                dicomObject.openFile(this.txtDICOMFile.Text);

                //try to get MIME type from the DICOM
                string Ext = this.GetEmbeddedExtension(dicomObject);
                if (Ext != String.Empty)
                {
                    if (Ext.ToUpper().Contains("PDF"))
                    {

                        //Create path to save extracted PDF
                        string tmpPDFFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EXTRACTED_PDF");
                        if (!Directory.Exists(tmpPDFFile))
                        {
                            Directory.CreateDirectory(tmpPDFFile);
                        }
                        tmpPDFFile = Path.Combine(tmpPDFFile, new FileInfo(this.txtDICOMFile.Text).Name + ".pdf");
                        if (File.Exists(tmpPDFFile))
                        {
                            File.Delete(tmpPDFFile);
                        }

                        this.SaveEmbeddedFile(dicomObject, tmpPDFFile);

                        //Open PDF using system reader
                        System.Diagnostics.Process pr = System.Diagnostics.Process.Start(tmpPDFFile);

                        return;
                    }
                }
                MessageBox.Show("File doesn't contain encapsulated PDF");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Extracts MIME type from DICOM object
        /// </summary>
        /// <param name="dicomObject"></param>
        /// <returns></returns>
        private string GetEmbeddedExtension(DCXOBJ dicomObject)
        {
            try
            {
                DCXELM docEl = dicomObject.getElementByTag((int)DICOM_TAGS_ENUM.sopClassUid);
                if (docEl.Value.ToString() == "1.2.840.10008.5.1.4.1.1.104.1")
                {
                    docEl = dicomObject.getElementByTag((int)DICOM_TAGS_ENUM.MIMETypeOfEncapsulatedDocument);

                    return (this.ConvertMimeTypeToExtension(docEl.Value.ToString()));
                }
            }
            catch (Exception)
            {
                //Not exist, do nothing
            }
            return String.Empty;
        }

        /// <summary>
        /// Convert MIME type to file extension from DICOM object according by info from system Regidtry
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private string ConvertMimeTypeToExtension(string mimeType)
        {
            string key = string.Format(@"MIME\Database\Content Type\{0}", mimeType);
            string result;

            RegistryKey regKey;
            object value;

            regKey = Registry.ClassesRoot.OpenSubKey(key, false);
            value = regKey != null ? regKey.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        /// <summary>
        /// Extracts embedded file from DICOM object and saves it's content by given path
        /// </summary>
        /// <param name="dicomObject"></param>
        /// <param name="FName"></param>
        private unsafe void SaveEmbeddedFile(DCXOBJ dicomObject, string FName)
        {
            //Extract encapsulated document
            DCXELM docEl = dicomObject.getElementByTag((int)DICOM_TAGS_ENUM.EncapsulatedDocument);
            int len = Convert.ToInt32(docEl.Length);
            UInt32 p = (UInt32)docEl.Value;
            byte[] bytes = new byte[len];
            Marshal.Copy((IntPtr)p, bytes, 0, len);

            // Open file for writing
            FileStream _FileStream = new FileStream(FName, FileMode.Create, System.IO.FileAccess.Write);
            
            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(bytes, 0, len);

            // close file stream
            _FileStream.Close();
        }
    }
}
