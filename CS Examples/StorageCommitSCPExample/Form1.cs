/// \page CSTestApplications C# Test Applications
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// \section StorageCommitSCPDemo Storage Commit SCP Demo
/// Storage Commit SCP Demo
/// Waits for a storage commit request

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

namespace StorageCommitSCPExample
{
    public partial class Form1 : Form
    {
        public Form1()
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