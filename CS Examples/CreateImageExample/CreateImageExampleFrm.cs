using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using rzdcxLib;

namespace CreateImageExample
{
    public partial class CreateImageExampleFrm : Form
    {
        static int ROWS = 512;
        static int COLUMNS = 512;
        static int BITS_ALLOCATED = 16;
        static int BITS_STORED = 16;
        static int RESCALE_INTERCEPT = 0;
        static int SAMPLES_PER_PIXEL = 1;
        static int NUMBER_OF_FRAMES = 1;
        static string PHOTOMETRIC_INTERPRETATION = "MONOCHROME2";

        public CreateImageExampleFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ConvertImage(saveFileDialog1.FileName);
            }
        }

        private unsafe void ConvertImage(string fileName)
        {

            try
            {
                //Create DCXOBJ and insert into it encapsulated PDF
                DCXOBJ output = new DCXOBJ();

                DCXUID id = new DCXUID();

                //Insert all required tags for example:
                this.InsertStringElement(output, DICOM_TAGS_ENUM.SpecificCharacterSet, "ISO_IR 100");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.StudyDescription, "Convert RAW image to DICOM");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.PatientsName, "John^Doe");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.patientID, "123765");
                this.InsertStringElement(output, DICOM_TAGS_ENUM.studyInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_STUDY));
                this.InsertStringElement(output, DICOM_TAGS_ENUM.seriesInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_SERIES));
                this.InsertStringElement(output, DICOM_TAGS_ENUM.sopInstanceUID, id.CreateUID(UID_TYPE.UID_TYPE_INSTANCE));

                //Insert tag defining file as containing image
                this.InsertStringElement(output, DICOM_TAGS_ENUM.sopClassUid, "1.2.840.10008.5.1.4.1.1.7"); // Secondary Capture

                //Insert image-related tags
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.Rows, ROWS);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.Columns, COLUMNS);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.SamplesPerPixel, SAMPLES_PER_PIXEL);
                this.InsertStringElement(output, DICOM_TAGS_ENUM.PhotometricInterpretation, PHOTOMETRIC_INTERPRETATION);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.BitsAllocated, BITS_ALLOCATED);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.BitsStored, BITS_STORED);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.HighBit, BITS_STORED - 1);
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.WindowCenter, (int)(1 << (BITS_STORED - 1)));
                this.InsertIntegerElement(output, DICOM_TAGS_ENUM.WindowWidth, (int)(1 << BITS_STORED));
                this.InsertShortElement(output, DICOM_TAGS_ENUM.RescaleIntercept, (short)RESCALE_INTERCEPT);
                this.InsertShortElement(output, DICOM_TAGS_ENUM.RescaleSlope, (short)1);
                this.InsertStringElement(output, DICOM_TAGS_ENUM.GraphicData, "456\\8934\\39843\\223\\332\\231\\100\\200\\300\\400");
                this.InsertShortElement(output, DICOM_TAGS_ENUM.PixelRepresentation, 0);
                //Insert image
                DCXELM el = new DCXELM();
                el.Init((int)DICOM_TAGS_ENUM.PixelData);

                el.Length = (uint)(ROWS * COLUMNS * SAMPLES_PER_PIXEL * NUMBER_OF_FRAMES);

                ushort[] pixels = new ushort[ROWS * COLUMNS * NUMBER_OF_FRAMES];
                for (int n = 0; n < NUMBER_OF_FRAMES; n++)
                {
                    for (int y = 0; y < ROWS; y++)
                    {
                        for (int x = 0; x < COLUMNS; x++)
                        {
                            int i = x + COLUMNS * y * n;
                            pixels[i] = (ushort)(((i) % (1 << BITS_STORED)) - RESCALE_INTERCEPT);
                        }
                    }
                }
                el.Value = pixels;
                //must change VR after value is set
                el.ValueRepresentation = VR_CODE.VR_CODE_OB;
                //fixed (ushort* p = pixels)
                //{
                //    UIntPtr p1 = (UIntPtr)(ulong*)p;
                //    el.Value = p1;
                //}

                output.insertElement(el);

                //Save DCXOBJ as DICOM file
                string toSave = fileName;
                if (!toSave.Contains(".dcm"))
                    toSave = toSave + ".dcm";
                
                output.saveFile(toSave);

                this.lblResult.Text = "Saved as " + toSave;

            }
            catch (Exception ex)
            {
                this.lblResult.Text = ex.Message;
            }
        }

        /// <summary>
        /// Insert one DXCELM object into given DCXOBJ
        /// If Value parameter is not empty string - set it as element value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <param name="Value"></param>
        private void InsertStringElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, string Value = "")
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);

            if (Value != "")
                el.Value = Value;

            obj.insertElement(el);

        }

        /// <summary>
        /// Insert one DXCELM object of integer value into given DCXOBJ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <param name="Value"></param>
        private void InsertIntegerElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, int Value)
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);
            el.Value = Value;

            obj.insertElement(el);

        }

        private void InsertShortElement(DCXOBJ obj, DICOM_TAGS_ENUM tag, short Value)
        {
            DCXELM el = new DCXELM();

            el.Init((int)tag);
            el.Value = Value;

            obj.insertElement(el);

        }
    }
}
