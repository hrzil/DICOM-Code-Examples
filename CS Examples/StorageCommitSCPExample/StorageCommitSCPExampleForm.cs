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
/// \section StorageCommitSCPDemo Storage Commit SCP Demo
/// Storage Commit SCP Demo
/// Waits for a storage commit request
/// Waits for Q\R request from SCU as well

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example StorageCommitSCPExampleForm.cs
/// A C# Storage Commitment SCP Example
namespace StorageCommitSCPExample
{
    public partial class StorageCommitSCPExampleForm : Form
    {
        public StorageCommitSCPExampleForm()
        {
            InitializeComponent();
        }

        /* Callback method when a connection is recieved
           Here you can check the calling ae and accept or 
           reject the association*/
        private void OnConnectionEventHandler(string callingTitle,
                                       string calledTitle,
                                       string callingHost,
                                       ref bool acceptConnection)
        {
            acceptConnection = true;
        }

        /* Handle the on timeout event here */
        private void OnTimeoutEventHandler()
        {
            MessageBox.Show("A timeout occurred");
        }

        /* When a result for the storage commit is recieved 
           this event occurs. Handle the succeeded and failed instances here*/
        private void OnCommitResultEventHandler(bool status,
                                                string transactionUID,
                                                string succeededInstances,
                                                string failedInstances)
        {
            MessageBox.Show("Commit result: Status: " + status + "\n" +
                            "TransactionUID: " + transactionUID + "\n" +
                            "SucceededInstances: " + succeededInstances + "\n" +
                            "FailedInstances: " + failedInstances);
        }

        /* When a request for the storage commit is recieved 
           this event occurs.*/
        private void OnStorageCommitRequestEventHandler(string transactionUID,
                                                string instances,
                                                ref bool acceptStorageCommit)
        {
            MessageBox.Show("Commit Request: " +
                            "TransactionUID: " + transactionUID + "\n" +
                            "Instances: " + instances
                            );

            acceptStorageCommit = true;
        }

        public class MyIterator : IDCXOBJIterator
        {
            int _i = 0;
            DCXOBJ _results;

            public MyIterator(DCXOBJ query)
            {
                //_results = query;
                _results = new DCXOBJ();
            }
            int i = 0;
             void IDCXOBJIterator.Next()
            {
                _i++;
            }

             bool IDCXOBJIterator.AtEnd()
            {
                return _i++ < 3;
            }

             DCXOBJ IDCXOBJIterator.Get()
            {
                DCXELM e = new DCXELM();
                e.Init((int)DICOM_TAGS_ENUM.AccessionNumber);
                e.Value = _i+1;
                _results.insertElement(e);
                return _results;
            }

             void IDCXOBJIterator.Insert(DCXOBJ inObject)
            {
                throw new NotImplementedException();
            }
        }

        private DCXOBJIterator OnFindRequestEventHandler(DCXOBJ query)
        {
            // Take the identifier, translate it to SQL
            // Run the SQL command on my database
            // Put the results into a new MyIterator and return it to the user
            
            query.Dump("find.txt");
	        DCXOBJIterator i = new DCXOBJIterator();
            DCXELM e = new DCXELM();
	        e.Init(0x100010); // Patient Name
	        e.Value = "Patient A";
	        query.insertElement(e);
	        e.Init((int)rzdcxLib.DICOM_TAGS_ENUM.patientID);
	        e.Value = "PID1";
	        query.insertElement(e);
	        i.Insert((DCXOBJ)query);

            e.Init(0x100010); // Patient Name
            e.Value = "Patient B";
            query.insertElement(e);
            e.Init((int)rzdcxLib.DICOM_TAGS_ENUM.patientID);
            e.Value = "PID2";
            query.insertElement(e);
            i.Insert((DCXOBJ)query);

            e.Init(0x100010); // Patient Name
            e.Value = "Patient C";
            query.insertElement(e);
            e.Init((int)rzdcxLib.DICOM_TAGS_ENUM.patientID);
            e.Value = "PID3";
            query.insertElement(e);
            i.Insert((DCXOBJ)query);
	        //query->Release();
	       return i;

          //  return outResults;
        }

        private void OnEchoEventHandler()
        {
            MessageBox.Show("Hi");
        }
        private void ListenBtn_Click(object sender, EventArgs e)
        {
            DCXAPP app = new DCXAPP();
            app.StartLogging("c:\\rzdcxLog.txt");

            // Create the acceptor object here
            DCXACC acc = new DCXACC();

            // Connect all the events to the callback methods
            acc.OnConnection += new IDCXACCEvents_OnConnectionEventHandler(OnConnectionEventHandler);
            acc.OnTimeout += new IDCXACCEvents_OnTimeoutEventHandler(OnTimeoutEventHandler);
            acc.OnCommitResult += new IDCXACCEvents_OnCommitResultEventHandler(OnCommitResultEventHandler);
            acc.OnStorageCommitRequest += new IDCXACCEvents_OnStorageCommitRequestEventHandler(OnStorageCommitRequestEventHandler);
            acc.OnEcho += new IDCXACCEvents_OnEchoEventHandler(OnEchoEventHandler);
            //acc.OnFind += new IDCXACCEvents_OnFindEventHandler(OnFindRequestEventHandler);
            acc.OnFind += new IDCXACCEvents_OnFindEventHandler(OnFindRequestEventHandler);

            // This is the standard loop to wait for connections and commands
            while (true)
            {
                if (acc.WaitForConnection(LocalAEEdit.Text, ushort.Parse(PortEdit.Text), 30))
                {
                    // Can go to new thread
                    bool res;
                    do
                    {
                        if (acc.WaitForCommand(30))
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    } while (res);
                }
            }

            ReleaseComObject(acc);

            app.StopLogging();

            ReleaseComObject(app);
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        }
}