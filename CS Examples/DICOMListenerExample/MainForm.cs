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
/// \section DICOMListener DICOM Listener
/// DICOM Listener
/// This demo shows simple DICOM listener which waits for connection, gets incoming DICOM file and store it in pre-defined place

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Reflection;

/// \example MainForm.cs
/// A C# DICOM Listener Example
namespace DICOMListener
{
    public partial class MainForm : Form
    {
        Configuration config;

        private IListener dicomListener = null;

        private bool _FirstActivation;

        public MainForm()
        {
            InitializeComponent();

            this._FirstActivation = true;
        }

        private void OnErrorHandler(Exception ex)
        {
            DoUiStuff(
                    () =>
                    {
                        if (this.txtLog.Text.Length > 2000)
                            this.txtLog.Text = this.txtLog.Text.Substring(2000);
                        this.txtLog.Text += "\r\n" + ex.Message + "\r\n";
                    }
                );
        }

        private void StartListener()
        {
            try
            {
                this.ResetOneSettingValue("ListenerStoragePath", this.txtStoragePath.Text);

                this.ResetOneSettingValue("AETitle", this.txtAETitle.Text.Trim());
                this.ResetOneSettingValue("Port", this.txtPort.Text);

                this.config.Save(ConfigurationSaveMode.Modified);

                if ((this.txtStoragePath.Text.Trim() == "") ||
                    (this.txtAETitle.Text.Trim() == "") ||
                    (this.txtPort.Text.Trim() == ""))
                {
                    this.lblError.Text = "One or more configuration values are not defined.\r\nPlease enter all values.";
                    return;
                }

                string ListenerStoragePath = this.config.AppSettings.Settings["ListenerStoragePath"].Value;
                string AETitle = this.config.AppSettings.Settings["AETitle"].Value;
                ushort Port = (ushort)Convert.ToInt16(this.config.AppSettings.Settings["Port"].Value);
                this.dicomListener = ListenerFactory.CreateListener(ListenerStoragePath, AETitle, Port, OnErrorHandler);
                this.dicomListener.GUIHandler = this.ShowInLog;
                this.dicomListener.Start();
                this.lblListenerStatus.Text = "Started";

                this.lblError.Text = "";
            }
            catch (Exception ex)
            {
                this.lblError.Text = "Listener can't start. The port may be in use by another application\r\n" + ex.Message;
                this.lblListenerStatus.Text = "Stopped";
                return;
            }
        }

        private void ShowInLog(string WriteToLog)
        {
            DoUiStuff(
                    () =>
                    {
                        if (this.txtLog.Text.Length > 2000)
                            this.txtLog.Text = this.txtLog.Text.Substring(2000);
                        this.txtLog.Text += "\r\n" + WriteToLog + "\r\n";
                    }
                );
        }

        private void StopListener()
        {
            if (this.dicomListener != null)
            {
                this.lblListenerStatus.Text = "Trying to stop";
                this.Refresh();
                this.dicomListener.Stop();
                this.lblListenerStatus.Text = "Stopped";
            }
        }

        /// <summary>
        /// Execute some GUI action from another thread
        /// </summary>
        /// <param name="a"></param>
        private void DoUiStuff(Action a)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(a));
            else
                a();

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ResetOneSettingValue("ListenerStoragePath", this.txtStoragePath.Text);
            
            this.ResetOneSettingValue("AETitle", this.txtAETitle.Text.Trim());
            this.ResetOneSettingValue("Port", this.txtPort.Text);

            this.config.Save(ConfigurationSaveMode.Modified);

            if(this.dicomListener != null)
                this.dicomListener.Stop();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (this._FirstActivation)
            {
                this.config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                this.txtAETitle.Text = this.config.AppSettings.Settings["AETitle"].Value;
                this.txtPort.Text = this.config.AppSettings.Settings["Port"].Value;

                this.txtStoragePath.Text = this.config.AppSettings.Settings["ListenerStoragePath"].Value;

                if ((this.txtStoragePath.Text.Trim() == "") ||
                    (this.txtAETitle.Text.Trim() == "") ||
                    (this.txtPort.Text.Trim() == ""))
                {
                    this.lblError.Text = "One or more configuration values are not defined.\r\nPlease enter all values.";
                }
                else
                {
                    this._FirstActivation = false;
                    this.lblListenerStatus.Text = "Starting...";
                    this.Refresh();
                    this.StartListener();
                }
            }
        }

        private void ResetOneSettingValue(string keyName, string newValue)
        {
            if (this.config.AppSettings.Settings[keyName].Value != newValue)
            {
                this.config.AppSettings.Settings.Remove(keyName);
                this.config.AppSettings.Settings.Add(keyName, newValue);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtStoragePath.Text = this.fbd.SelectedPath;
                this.ResetOneSettingValue("ListenerStoragePath", this.txtStoragePath.Text);
                this.config.Save(ConfigurationSaveMode.Modified);
            }
        }

        private void btnStartListener_Click(object sender, EventArgs e)
        {
            this.StopListener();
            this.StartListener();
        }

        private void btnStopListener_Click(object sender, EventArgs e)
        {
            this.StopListener();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dicomListener.AbortActiveAssociations();
        }
    }
}
