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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using rzdcxLib;

namespace DICOMListener
{
    public delegate void UpdateGUI(string WriteToLog);

    public delegate void DICOMListenerOnError(Exception ex);
    public delegate void OnConnectionEvent(string callingTitle, string calledTitle, string callingHost);
    public delegate void OnConnectionStarted();
    public delegate void OnConnectionEnded();

    //Defines DICOM Listener Interface
    public interface IListener
    {
        //Starts listening
        void Start();
        //Stop listening
        void Stop();

        void AbortActiveAssociations();

        bool IsRunning { get; }

        event OnConnectionStarted OnConnectionStartedEvent;

        event OnConnectionEnded OnConnectionEndedEvent;

        UpdateGUI GUIHandler { get; set; }
    }

    public static class ListenerFactory
    {
        public static IListener CreateListener(string pListenerStoragePath, string pAETitle, ushort pPort, DICOMListenerOnError onError)
        {
            DICOMListener l = new DICOMListener(pListenerStoragePath, pAETitle, pPort);
            if (onError != null)
                l.OnErrorHandler += onError;
            return l;
        }
    }

    enum Timeouts
    {
        INFINITLY,
        LOOP_TIMEOUT = 5,
        DEFAULT_TIMEOUT = 180
    }

    class DICOMListener : IListener
    {
        DCXACC acc;

        public event OnConnectionEvent OnConnection;

        public event DICOMListenerOnError OnErrorHandler;

        public event OnConnectionStarted OnConnectionStartedEvent;

        public event OnConnectionEnded OnConnectionEndedEvent;

        private bool _IsRunning = false;

        public bool IsRunning
        {
            get { return _IsRunning; }
        }

        private UpdateGUI _UpdateGUI;
        public UpdateGUI GUIHandler 
        {
            get { return _UpdateGUI; }
            set { _UpdateGUI = value; } 
        }

        //Declare  worker thread 
        protected Thread _WorkerThread;

        private bool IsTimeout;

        private bool AbortActive;

        private string ListenerStoragePath;
        private string AETitle;
        private ushort Port;

        public DICOMListener(string pListenerStoragePath, string pAETitle, ushort pPort)
        {
            this.ListenerStoragePath = pListenerStoragePath;
            this.AETitle = pAETitle;
            this.Port = pPort;
        }

        //Starts listening
        public void Start()
        {
            if (!Directory.Exists(this.ListenerStoragePath))
            {
                Directory.CreateDirectory(this.ListenerStoragePath);
            }

            this._IsRunning = true;
            this.IsTimeout = false;
            this.AbortActive = false;

            if ((this._WorkerThread == null) ||
                ((this._WorkerThread.ThreadState &
                (System.Threading.ThreadState.Unstarted | System.Threading.ThreadState.Stopped)) != 0))
            {
                this._WorkerThread = new Thread(new ThreadStart(WorkerMethod));
                this._WorkerThread.Start();
                Thread.CurrentThread.Join(500);
            }

        }

        private void WorkerMethod()
        {
            // Loop until application/service is stopped
            while (this._IsRunning)
            {
                // Create the acceptor object here
                acc = new DCXACC();

                // Connect all the events to the callback methods
                acc.OnConnection += new IDCXACCEvents_OnConnectionEventHandler(OnConnectionEventHandler);
                acc.OnStoreSetup += new IDCXACCEvents_OnStoreSetupEventHandler(OnStoreSetupEventHandler);
                acc.OnTimeout += new IDCXACCEvents_OnTimeoutEventHandler(OnTimeoutEventHandler);
                acc.OnStoreDone += new IDCXACCEvents_OnStoreDoneEventHandler(OnStoreDoneEventHandler);
                acc.OnCommitResult += new IDCXACCEvents_OnCommitResultEventHandler(OnCommitResultEventHandler);



                // Catch any error, report it and continue
                try
                {
                    // wait for connections
                    if (acc.WaitForConnection(this.AETitle,
                                              this.Port,
                                              (int)Timeouts.LOOP_TIMEOUT))
                    {
                        if (OnConnectionStartedEvent != null)
                            OnConnectionStartedEvent();

                        // service the commands
                        int timeout = (int)Timeouts.DEFAULT_TIMEOUT;
                        while (this._IsRunning && timeout > 0)
                        {
                            if (this.AbortActive)
                            {
                                this.AbortActive = false;
                                break;
                            }
                            if (acc.WaitForCommand((int)Timeouts.LOOP_TIMEOUT))
                            {
                                this.IsTimeout = false;
                                timeout = (int)Timeouts.DEFAULT_TIMEOUT; // reset the timer after a command was performed
                            }
                            else
                                if (this.IsTimeout)
                                {
                                    timeout -= (int)Timeouts.LOOP_TIMEOUT; // Timeout - continue
                                }
                                else
                                {
                                    this.IsTimeout = false;
                                    break; // No command and not a timeout - stop the association
                                }
                        }

                        if (OnConnectionEndedEvent != null)
                            OnConnectionEndedEvent();
                    }
                    else
                    {
                        this.AbortActive = false;
                    }
                }
                catch (Exception ex)
                {
                    if (OnConnectionEndedEvent != null)
                        OnConnectionEndedEvent();

                    this._IsRunning = false;
                    if (OnErrorHandler != null)
                        this.OnErrorHandler(ex);
                    else
                        throw;
                }

                acc.OnConnection -= new IDCXACCEvents_OnConnectionEventHandler(OnConnectionEventHandler);
                acc.OnStoreSetup -= new IDCXACCEvents_OnStoreSetupEventHandler(OnStoreSetupEventHandler);
                acc.OnTimeout -= new IDCXACCEvents_OnTimeoutEventHandler(OnTimeoutEventHandler);
                acc.OnStoreDone -= new IDCXACCEvents_OnStoreDoneEventHandler(OnStoreDoneEventHandler);
                acc.OnCommitResult -= new IDCXACCEvents_OnCommitResultEventHandler(OnCommitResultEventHandler);

                ReleaseComObject(acc);
            }

        }


        //Stop listening
        public void Stop()
        {
            this._IsRunning = false;
            if ((this._WorkerThread != null) && (this._WorkerThread.IsAlive))
            {
                try
                {
                    Thread.CurrentThread.Join((int)Timeouts.LOOP_TIMEOUT * 1000);
                    this._WorkerThread = null;
                }
                catch (Exception)
                {
                }

            }
        }

        public void AbortActiveAssociations()
        {
            this.AbortActive = true;
        }


        /* This has to be called for all the com objects to release the
        memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        /* Callback method when a connection is recieved
           Here you can check the calling ae and accept or 
           reject the association*/
        private void OnConnectionEventHandler(string callingTitle,
                                       string calledTitle,
                                       string callingHost,
                                       ref bool acceptConnection)
        {
            if (OnConnection != null)
                OnConnection(calledTitle, calledTitle, callingHost);
            acceptConnection = true;
        }

        private int _filecount = 0;

        /* When a store command is recieved, you can provide the
           filename to store to*/
        private void OnStoreSetupEventHandler(ref string filename)
        {
            filename = Path.Combine(ListenerStoragePath, (++this._filecount).ToString() + ".dcm");
        }

        /* Handle the on timeout event here */
        private void OnTimeoutEventHandler()
        {
            this.IsTimeout = true;
        }

        /* When store is done, handle the file here and respond if storage went well */
        private void OnStoreDoneEventHandler(string filename, bool status, ref bool response_accept)
        {
            //// bool newSeries = false;
            string study_uid = String.Empty;
            string series_uid = String.Empty;
            string instance_uid = String.Empty;

            DCXOBJ o = new DCXOBJ();
            o.openFile(filename);

            DCXELM el = o.getElementByTag((int)DICOM_TAGS_ENUM.studyInstanceUID);
            if (el != null)
                study_uid = el.Value as string;
            el = o.getElementByTag((int)DICOM_TAGS_ENUM.seriesInstanceUID);
            if (el != null)
                series_uid = el.Value as string;
            el = o.getElementByTag((int)DICOM_TAGS_ENUM.seriesInstanceUID);
            if (el != null)
                series_uid = el.Value as string; ;
            el = o.getElementByTag((int)DICOM_TAGS_ENUM.sopInstanceUID);
            if (el != null)
                instance_uid = el.Value as string; ;

            string newFileName = Path.Combine(this.ListenerStoragePath, study_uid, series_uid);
            Directory.CreateDirectory(newFileName);
            newFileName = Path.Combine(newFileName, instance_uid + ".dcm");

            // So the file is not locked, make sure to close the object before moving the file
            ReleaseComObject(o);

            // Check if the file exists and if so delete it
            if (File.Exists(newFileName))
            {
                File.Delete(newFileName);

            }

            // Move the file to its final location
            try
            {
                File.Move(filename, newFileName);
                if (this._UpdateGUI != null)
                    this._UpdateGUI("File stored: " + newFileName);
                 response_accept = true; // All ok
            }
            catch (Exception ex)
            {
                if (this._UpdateGUI != null)
                {
                    this._UpdateGUI(string.Format("Can't move file %1 to new location %2", filename, newFileName));
                    this._UpdateGUI(ex.Message);
                }
                response_accept = false; // tell SCU that it went wrong
            }

        }

        /* When a result for the storage commit is recieved 
           this event occurs. Handle the succeeded and failed instances here*/
        private void OnCommitResultEventHandler(bool status,
                                                string transactionUID,
                                                string succeededInstances,
                                                string failedInstances)
        {

        }

    }
}
