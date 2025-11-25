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
/// \section SRDemo DICOM Structured Report Demo
/// Structured Report Demo
/// This demo shows how to use the IDCXSR interface to create a simple report
/// \Note The application assumes that the SR dictionary StructuredReport.dic is located in the application directory
/// If the dictionary is located in a different place, uncomment the OpenDictionary command and modify it accordingly.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;

/// \example StructuredReportExample.cs
/// A C# DICOM SR Creator Example
namespace StructuredReportExample
{
    public partial class StructuredReportExample : Form
    {
        public StructuredReportExample()
        {
            InitializeComponent();
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string sop_class = "1.2.840.10008.5.1.4.1.1.88.22"; /// Enhanced SR
                DCXSR SR = new DCXSR();
                // SR.OpenDictionary("StructuredReport.dic");
                SR.CreateNewReport(sop_class, "209074");
                //SR.AddDictContainer("209074", SR_CONTINUITY.SR_SEPARATE);
                SR.AddDictContainer("121070", SR_CONTINUITY.SR_SEPARATE);
                /// Example of UID reference: Add a reference to the STUDY INSTANCE UID created for the Procedure
                SR.AddDictUIDREFValue("121018", "1.2.3.4.5.6");
                SR.AddDictCodedValue("G-C0E3", "T-35300");
                SR.AddDictNumericValue("G-0380", (float)12.34, null);

                /// Add just a stand-alone IMAGE element
                SR.AddDictImageValue("121080" /* Best illustration of finding */, "1.2.840.10008.5.1.4.1.1.6.1", "1.2.3.4.5.6.7", null, null, null);

                /// Add a waveform reference
                UInt16[] ChannelsTuples = new UInt16[4];
                ChannelsTuples[0] = 1;
                ChannelsTuples[1] = 2;
                ChannelsTuples[2] = 3;
                ChannelsTuples[3] = 4;
                SR.AddDictWaveformValue("121112" /*Source of Measurement*/,
                    "1.2.840.10008.5.1.4.1.1.9.1.1" /* 12 lead ECG waveform */,
                    "1.2.3.4.5.111" /* Some dummy UID */,
                    ChannelsTuples);

                /// Check SCOORD
                float[,] scoord = new float[2, 1];
                scoord[0, 0] = 0.5F;
                scoord[1, 0] = 3.9F;
                SR.AddDictSCOORDValue("209239", rzdcxLib.SR_GRAPHIC_TYPE.SR_GT_POINT, scoord);
                /// Add Image subnode to SCOORD
                SR.AddDictImageValue(null, "1.2.840.10008.5.1.4.1.1.6.1", "1.2.3.4.5.6.7", "1;22", null, null);
                SR.UpOneLevel();

                /// Check TCOORD with time offsets
                double[] f64tcoord = new double[1];
                f64tcoord[0] = DateTime.Now.ToFileTime();
                SR.AddDictTCOORDValue("109041", SR_TEMPORAL_RANGE_TYPE.SR_TR_BEGIN, f64tcoord);
                SR.UpOneLevel();

                /// Check TCOORD with sample positions
                uint[] u32tcoord = new uint[2];
                u32tcoord[0] = 0;
                u32tcoord[1] = 4;
                SR.AddDictTCOORDValue("109041", SR_TEMPORAL_RANGE_TYPE.SR_TR_SEGMENT, u32tcoord);
                SR.UpOneLevel();

                /// Check TCOORD with dates
                string[] strtcoord = new string[3];
                strtcoord[0] = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                strtcoord[1] = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)).ToString("yyyyMMddHHmmss.fff");
                strtcoord[2] = DateTime.Now.Add(new TimeSpan(2, 0, 0, 0)).ToString("yyyyMMddHHmmss.fff");
                SR.AddDictTCOORDValue("109041", SR_TEMPORAL_RANGE_TYPE.SR_TR_MULTIPOINT, strtcoord);
                SR.UpOneLevel();

                string html = SR.ToHtml();
                this.webBrowser1.DocumentText = html;
                //System.IO.File.WriteAllText("sr1.xml", SR.XML);

                DCXOBJ obj = SR.Object;
                obj.saveFile("sr1.dcm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
