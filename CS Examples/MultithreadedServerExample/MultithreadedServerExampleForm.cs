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
/// \section MultithreadedServerExample A C# Multi-Threaded DICOM Server
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// Multithreaded Server Example
/// Waits for multiple connections simultaniously from the client

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace MultithreadedServerExample
{
    /// \example MultithreadedServerExampleForm.cs
    /// This example demonstrates the use of the Acceptor in a multithreaded way.
    public partial class MultithreadedServerExampleForm : Form
    {
        void DoUiStuff(Action a)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(a));
            else
                a();

        }

        public MultithreadedServerExampleForm()
        {
            InitializeComponent();
            DCXAPP app = new DCXAPP();
        }

        public class AssocHandler
        {
            string _CalledAETitle;
            ushort _port;
            int _storeCount = 0;
            MultithreadedServerExampleForm _theApp;
            
            public AssociationControl UserControl
            {
                set;
                get;
            }

            string _CallingAETitle;
            public string CallingAETitle
            {
                get 
                {
                    return _CallingAETitle;
                }
            }

            string _CallingHost;
            public string CallingHost
            {
                get
                {
                    return _CallingHost;
                }
            }

            public DCXACC accepter;

            volatile static private int _filecount = 0;

            public AssocHandler(string aeTitle, ushort port, MultithreadedServerExampleForm inTheApp)
            {
                _CalledAETitle = aeTitle;
                _port = port;
                _theApp = inTheApp;
            }

            public void DoWork()
            {
                bool recievedConnection = false;
                AssociationControl assocControl = null;

                try
                {
                    accepter = CreateAccepter();

                    lock (associations)
                    {
                        associations.Add(this);
                    }

                    // This is the standard loop to wait for connections and commands
                    while (running && !recievedConnection)
                    {
                        if (accepter.WaitForConnection(_CalledAETitle, _port, 1))
                        {
                            // Notify the main thread that a connection
                            // was established
                            lock (this)
                            {
                                Monitor.Pulse(this);
                            }

                            recievedConnection = true;
                            assocControl = new AssociationControl(this);
                            _theApp.AddAssociationControl(assocControl);

                            bool res;
                            do
                            {
                                // Wait for a command from the client
                                if (accepter.WaitForCommand(commandWaitTimeout))
                                {
                                    res = true;
                                }
                                else
                                {
                                    res = false;
                                }
                            } while (running && res);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (assocControl != null)
                        _theApp.RemoveAssociationControl(assocControl);

                    lock (associations)
                    {
                        associations.Remove(this);
                    }
                    if (accepter != null)
                    {
                        Marshal.ReleaseComObject(accepter);
                    }
                    // Notify the main thread we are done here if we didn't do so 
                    // because we didn't get any connection
                    lock (this)
                    {
                        if (!recievedConnection)
                            Monitor.Pulse(this);
                    }
                }
            }

            private DCXACC CreateAccepter()
            {
                // Create the acceptor object here
                DCXACC acc = new DCXACC();
                // Connect all the events to the callback methods
                acc.OnConnection += new IDCXACCEvents_OnConnectionEventHandler(OnConnectionEventHandler);
                acc.OnTimeout += new IDCXACCEvents_OnTimeoutEventHandler(OnTimeoutEventHandler);
                acc.OnCommitResult += new IDCXACCEvents_OnCommitResultEventHandler(OnCommitResultEventHandler);
                acc.OnStorageCommitRequest += new IDCXACCEvents_OnStorageCommitRequestEventHandler(OnStorageCommitRequestEventHandler);
                acc.OnStoreSetup += new IDCXACCEvents_OnStoreSetupEventHandler(OnStoreSetupEventHandler);
                acc.OnStoreDone += new IDCXACCEvents_OnStoreDoneEventHandler(OnStoreDoneEventHandler);
                return acc;
            }

            /* Callback method when a connection is recieved
           Here you can check the calling ae and accept or 
           reject the association*/
            private void OnConnectionEventHandler(string callingTitle,
                                           string calledTitle,
                                           string callingHost,
                                           ref bool acceptConnection)
            {
                _CallingAETitle = callingTitle;
                _CallingHost = callingHost;

                if (_theApp.checkAcceptAssoc.Checked)
                {
                    if (_theApp.checkCalledAETitle.Checked)
                    {
                        if (calledTitle == _theApp.LocalAEEdit.Text)
                        {
                            acceptConnection = true;
                        }
                        else
                        {
                            acceptConnection = false;
                        }
                    }
                    else
                    {
                        acceptConnection = true;
                    }
                }
                else
                {
                    acceptConnection = false;
                }
            }

            /* When a store command is recieved, you can provide the
            filename to store to*/
            private void OnStoreSetupEventHandler(ref string filename)
            {
                filename = _theApp.textStoreFilesIn.Text + "\\" + _CallingAETitle + "\\F" + (++_filecount) + ".dcm";
                FileInfo f = new FileInfo(filename);
                if (!f.Directory.Exists)
                    f.Directory.Create();

            }

            /* When store is done, handle the file here and respond if storage went well */
            private void OnStoreDoneEventHandler(string filename, bool status, ref bool accept)
            {
                accept = true;
                _storeCount++;
                _theApp.DoUiStuff(() =>
                {
                    if (UserControl != null)
                        UserControl.StorageCounter = _storeCount;
                });
            }

            /* Handle the on timeout event here */
            private void OnTimeoutEventHandler()
            {
                //MessageBox.Show("A timeout occurred");
            }

            /* When a result for the storage commit is recieved 
               this event occurs. Handle the succeeded and failed instances here*/
            private void OnCommitResultEventHandler(bool status,
                                                    string transactionUID,
                                                    string succeededInstances,
                                                    string failedInstances)
            {
                //MessageBox.Show("Commit result: Status: " + status + "\n" +
                //                "TransactionUID: " + transactionUID + "\n" +
                //                "SucceededInstances: " + succeededInstances + "\n" +
                //                "FailedInstances: " + failedInstances);
            }

            /* When a request for the storage commit is recieved 
               this event occurs.*/
            private void OnStorageCommitRequestEventHandler(string transactionUID,
                                                    string instances,
                                                    ref bool acceptStorageCommit)
            {
                //MessageBox.Show("Commit Request: " +
                //                "TransactionUID: " + transactionUID + "\n" +
                //                "Instances: " + instances
                //                );

                acceptStorageCommit = true;
            }
        }

        static bool running = false;

        private void StartListening()
        {
            //DCXAPP app = new DCXAPP();
            //app.StartLogging("c:\\rzdcxLog.txt");

            // This is the standard loop to wait for connections and commands
            while (running)
            {
                AssocHandler handler = new AssocHandler(LocalAEEdit.Text, ushort.Parse(PortEdit.Text), this);
                Thread t = new Thread(new ThreadStart(handler.DoWork));
                t.SetApartmentState(ApartmentState.MTA);
                t.Start();

                // Wait until the thread tells us that a connection was established
                // or that it quits waiting
                // so we can create a new thread and wait for another connection
                lock (handler)
                {
                    Monitor.Wait(handler);
                }
            }
            //app.StopLogging();
            //ReleaseComObject(app);
        }

        Thread listenerThread = null;
        private void RunListenerEventLoopOnThread()
        {
            listenerThread = new Thread(new ThreadStart(this.StartListening));
            listenerThread.SetApartmentState(ApartmentState.MTA);
            listenerThread.Start();
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        private void checkStartStop_CheckedChanged(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(textStoreFilesIn.Text))
                System.IO.Directory.CreateDirectory(textStoreFilesIn.Text);

            lock (this)
            {
                running = checkStartStop.Checked;
                if (running)
                {
                    if (listenerThread == null)
                    {
                        RunListenerEventLoopOnThread();
                    }
                }
                else
                {
                    if (listenerThread != null)
                    {
                        listenerThread = null;
                    }
                }
            }
        }

        public static int commandWaitTimeout;
        private void numCommandWaitTimeout_ValueChanged(object sender, EventArgs e)
        {

        }

        static List<AssocHandler> associations;

        private void MultithreadedServerExampleForm_Load(object sender, EventArgs e)
        {
            commandWaitTimeout = Convert.ToInt32(this.numCommandWaitTimeout.Value);
            associations = new List<AssocHandler>();
        }

        public static ushort nextCStoreStatusVal = 0;
        private void nextCStoreStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            nextCStoreStatusVal = UInt16.Parse(nextCStoreStatus.Text, System.Globalization.NumberStyles.HexNumber);
            lock (associations)
            {
                foreach (AssocHandler handler in associations)
                {
                    handler.accepter.NextStorageResponseStatus = nextCStoreStatusVal;
                }
            }
        }

        private void MultithreadedServerExampleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            running = false;
        }

        public void AddAssociationControl(AssociationControl assocControl)
        {
            DoUiStuff(() =>
            {
                activeAssociations.Controls.Add(assocControl);
                assocControl.BringToFront();
                assocControl.Dock = DockStyle.Top;

            });
        }

        public void RemoveAssociationControl(AssociationControl assocControl)
        {
            DoUiStuff(() =>
            {
                activeAssociations.Controls.Remove(assocControl);
            });
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textStoreFilesIn.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}