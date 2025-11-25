/*
 * Copyright (C) 2005 - 2011, Roni Zaharia
 */

/// \page CSTestApplications C# Test Applications
/// The C# test applications can be downloaded from www.roniza.com/downloads
/// \section MWLSCU Modality Worklist Query Demo
/// Modality Worklist Query Demo
/// This example shows how to make a Modality Worklist Query

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;
using System.Runtime.InteropServices;

/// \example ModalityWorklistSCUExampleForm.cs
/// A C# DICOM Query/Retrieve SCU Example
namespace ModalityWorklistSCU
{
    public partial class ModalityWorklistSCUExampleForm : Form
    {
        public ModalityWorklistSCUExampleForm()
        {
            InitializeComponent();
        }

        /* Callback when a response from the called ae for the 
           query command is recieved */
        public void OnQueryResponseRecievedAction(DCXOBJ obj)
        {
            // Do something
            ReleaseComObject(obj);
        }

        /* Callback when a response from the called ae for the 
           move command is recieved */
        public void OnMoveResponseRecievedAction(bool status, DCXOBJ obj)
        {
            // Do something
            ReleaseComObject(obj);
        }

        private string TryGetString(DCXOBJ obj, DICOM_TAGS_ENUM tag)
        {
            try
            {
                DCXELM e = obj.getElementByTag((int)tag);
                if (e != null && e.Value != null)
                    return obj.getElementByTag((int)tag).Value.ToString();
                else
                    return "N/A";
            }
            catch (COMException)
            {
                return "N/A";
            }
        }

        private void CreateGridsDataLayout()
        {
            // Requested Procedure Table
            DataTable rq = new DataTable("RQ");

            rq.Columns.Add(new DataColumn("Patient Name", typeof(string)));
            rq.Columns.Add(new DataColumn("Accession Number", typeof(string)));
            rq.Columns.Add(new DataColumn("Requested Procedure ID", typeof(string)));
            rq.Columns.Add(new DataColumn("Requested Procedure Description", typeof(string)));
            rq.Columns.Add(new DataColumn("OBJ", typeof(DCXOBJ)));

            DataTable sps = new DataTable("SPS");

            sps.Columns.Add(new DataColumn("Requested Procedure ID", typeof(string)));
            sps.Columns.Add(new DataColumn("Modality", typeof(string)));
            sps.Columns.Add(new DataColumn("Scheduled Station AE Title", typeof(string)));
            sps.Columns.Add(new DataColumn("Scheduled Procedure Step Description", typeof(string)));
            sps.Columns.Add(new DataColumn("SPS ID", typeof(string)));
            sps.Columns.Add(new DataColumn("OBJ", typeof(DCXOBJ)));

            DataTable mpps = new DataTable("MPPS");
            mpps.Columns.Add(new DataColumn("SPS ID", typeof(string)));
            mpps.Columns.Add(new DataColumn("SOP Instance UID", typeof(string)));
            mpps.Columns.Add(new DataColumn("State", typeof(string)));
            mpps.Columns.Add(new DataColumn("OBJ", typeof(MPPS)));

            DataSet data = new DataSet();
            data.Tables.Add(rq);
            data.Tables.Add(sps);
            data.Tables.Add(mpps);

            DataRelation relation1 = new DataRelation("RP_SPS",
                data.Tables["RQ"].Columns["Requested Procedure ID"],
                data.Tables["SPS"].Columns["Requested Procedure ID"]);
            data.Relations.Add(relation1);
            DataRelation relation2 = new DataRelation("SPS_MPPS",
                data.Tables["SPS"].Columns["SPS ID"],
                data.Tables["MPPS"].Columns["SPS ID"]);
            data.Relations.Add(relation2);

            // Bind the master data connector 
            bsRP.DataSource = data;
            bsRP.DataMember = "RQ";

            // Bind the details data connector 
            // using the DataRelation name to filter the information in the 
            // details table based on the current row in the master table. 
            bsSPS.DataSource = bsRP;
            bsSPS.DataMember = "RP_SPS";

            // Bind the MPPS
            bsMPPS.DataSource = bsSPS;
            bsMPPS.DataMember = "SPS_MPPS";

            // Resize the master DataGridView columns to fit the newly loaded data.
            dgvRP.AutoResizeColumns();
            dgvRP.Columns["OBJ"].Visible = false;

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            dgvSPS.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSPS.Columns["OBJ"].Visible = false;

            dgvMPPS.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells; 
            dgvMPPS.Columns["OBJ"].Visible = false;
        }

        private void LoadResultsToGrid(DCXOBJIterator it)
        {
            // Iterate over the query results
            DataTable rq = ((DataSet)(bsRP.DataSource)).Tables["RQ"];
            DataTable sps = ((DataSet)(bsRP.DataSource)).Tables["SPS"];
            DataTable mpps = ((DataSet)(bsRP.DataSource)).Tables["MPPS"];
            mpps.Rows.Clear();
            sps.Rows.Clear();
            rq.Rows.Clear();
            DataRow rqRow;
            DataRow spsRow;
            for (; !it.AtEnd(); it.Next())
            {
                DCXOBJ currObj = it.Get();
                rqRow = rq.NewRow();
                rqRow["Patient Name"] = 
                    TryGetString(currObj, DICOM_TAGS_ENUM.patientName);
                rqRow["Accession Number"] = 
                    TryGetString(currObj,DICOM_TAGS_ENUM.AccessionNumber);
                rqRow["Requested Procedure ID"] = 
                    TryGetString(currObj,DICOM_TAGS_ENUM.RequestedProcedureID);
                rqRow["Requested Procedure Description"] = 
                    TryGetString(currObj,DICOM_TAGS_ENUM.RequestedProcedureDescription);
                rqRow["OBJ"] = currObj;
                rq.Rows.Add(rqRow);

                // Now iterate over the SPS of this RQ
                DCXOBJIterator spsIt =
                    currObj.getElementByTag((int)DICOM_TAGS_ENUM.ScheduledProcedureStepSequence).Value as DCXOBJIterator; ;
                for (; !spsIt.AtEnd(); spsIt.Next())
                {
                    DCXOBJ spsObj = spsIt.Get();
                    spsRow = sps.NewRow();
                    spsRow["Requested Procedure ID"] = 
                        TryGetString(currObj, DICOM_TAGS_ENUM.RequestedProcedureID);
                    spsRow["Modality"] = 
                        TryGetString(spsObj, DICOM_TAGS_ENUM.Modality);
                    spsRow["Scheduled Station AE Title"] = 
                        TryGetString(spsObj, DICOM_TAGS_ENUM.ScheduledStationAETitle);
                    spsRow["Scheduled Procedure Step Description"] = 
                        TryGetString(spsObj, DICOM_TAGS_ENUM.ScheduledProcedureStepDescription);
                    spsRow["SPS ID"] =
                        TryGetString(spsObj, DICOM_TAGS_ENUM.ScheduledProcedureStepID);
                    spsRow["OBJ"] = spsObj;
                    sps.Rows.Add(spsRow);

                }
            }

        }

        private void AddRowToMPPSGrid(MPPS mpps)
        {
            //DataTable mpps = new DataTable("MPPS");
            //mpps.Columns.Add(new DataColumn("SPS ID", typeof(string)));
            //mpps.Columns.Add(new DataColumn("SOP Instance UID", typeof(string)));
            //mpps.Columns.Add(new DataColumn("State", typeof(string)));
            //mpps.Columns.Add(new DataColumn("OBJ", typeof(DCXOBJ)));

            DataTable table = ((DataSet)(bsRP.DataSource)).Tables["MPPS"];
            DataRow row = table.NewRow();
            row["SPS ID"] = TryGetString(mpps.sps, DICOM_TAGS_ENUM.ScheduledProcedureStepID);
            row["SOP Instance UID"] = mpps.SOPInstanceUID;
            row["State"] = mpps.State.ToString();
            row["OBJ"] = mpps;
            table.Rows.Add(row);
        }

        private DCXOBJ BuildQuery()
        {
            DCXOBJIterator it = null;
            DCXOBJ rp = null;
            DCXOBJ sps = null;
            DCXELM el = null;
            DCXOBJIterator spsIt = null;

            // Fill the query object
            rp = new DCXOBJ();
            sps = new DCXOBJ();
            el = new DCXELM();

            // Build the Scheduled procedure Step (SPS) item
            el.Init((int)DICOM_TAGS_ENUM.ScheduledStationAETitle);
            el.Value = StationNameEdit.Text;
            sps.insertElement(el);

            // A lot of code to handle all the cases of date and time matching
            // that eventually goes into the elements: ScheduledProcedureStepStartDate and ScheduledProcedureStepStartTime
            el.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepStartDate);
            if (checkDateExact.Checked)
            {
                el.Value = dateExact.Value;
                sps.insertElement(el);
            }
            else
            {
                if (checkDateTo.Checked && checkDateFrom.Checked)
                {
                    string dateRange = dateStartDate.Value.ToString("yyyyMMdd") + "-" + dateEndDate.Value.ToString("yyyyMMdd");
                    el.Value = dateRange;
                    sps.insertElement(el);
                }
                else if (checkDateFrom.Checked && !checkDateTo.Checked)
                {
                    string dateRange = dateStartDate.Value.ToString("yyyyMMdd") + "-";
                    el.Value = dateRange;
                    sps.insertElement(el);
                }
                else if (!checkDateTo.Checked && checkDateFrom.Checked)
                {
                    string dateRange = "-" + dateEndDate.Value.ToString("yyyyMMdd");
                    el.Value = dateRange;
                    sps.insertElement(el);
                }
            }

            /// This adds a filter for time
            el.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepStartTime);
            if (checkTimeExact.Checked)
            {
                el.Value = timeExact.Value;
                sps.insertElement(el);
            }
            else
            {
                if (checkTimeFrom.Checked && checkTimeTo.Checked)
                {
                    string dateRange = timeFrom.Value.ToString("HHmmss") + "-" + timeTo.Value.ToString("HHmmss");
                    el.Value = dateRange;
                    sps.insertElement(el);
                }
                else if (checkTimeFrom.Checked && !checkTimeTo.Checked)
                {
                    string range = timeFrom.Value.ToString("HHmmss") + "-";
                    el.Value = range;
                    sps.insertElement(el);
                }
                else if (!checkTimeFrom.Checked && checkTimeTo.Checked)
                {
                    string range = "-" + timeTo.Value.ToString("HHmmss");
                    el.Value = range;
                    sps.insertElement(el);
                }
            }

            // Handle the modality Combo Box
            el.Init((int)DICOM_TAGS_ENUM.Modality);
            if (comboBoxModality.SelectedItem != null)
                el.Value = comboBoxModality.SelectedItem.ToString();
            sps.insertElement(el);

            // Ask for SPS description
            el.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepDescription);
            sps.insertElement(el);

            // Ask for SPS ID
            el.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepID);
            sps.insertElement(el);

            // (0040,0006) - scheduled performing physicians name
            el.Init((int)DICOM_TAGS_ENUM.ScheduledPerformingPhysiciansName);
            sps.insertElement(el);

            // Now we put it as an item to sequence
            spsIt = new DCXOBJIterator();
            spsIt.Insert(sps);

            // and add the sequence Scheduled Procedure Step Sequence to the requested procedure (parent) object
            el.Init((int)DICOM_TAGS_ENUM.ScheduledProcedureStepSequence);
            el.Value = spsIt;
            rp.insertElement(el);


            /// Add the Requested Procedure attributes that we would like to get
            el.Init((int)DICOM_TAGS_ENUM.RequestedProcedureID);
            rp.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.RequestedProcedureDescription);
            rp.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.studyInstanceUID);
            rp.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.PatientsName);
            rp.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.patientID);
            rp.insertElement(el);

            el.Init((int)DICOM_TAGS_ENUM.AccessionNumber);
            rp.insertElement(el);

            rp.saveFile("ModalityWorklistQuery.dcm");

            return rp;
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DCXOBJ q;
                if (txtQueryFilePath.Text == null || txtQueryFilePath.Text.Length == 0)
                    q = BuildQuery();
                else
                {
                    q = new DCXOBJ();
                    q.openFile(txtQueryFilePath.Text);
                }
                //q.Dump("query.txt");

                // Create the requester object and connect it's callback to our method
                DCXREQ req = null;
                req = new DCXREQClass(); 
                req.OnQueryResponseRecieved += new IDCXREQEvents_OnQueryResponseRecievedEventHandler(OnQueryResponseRecievedAction);
                DCXOBJIterator it = req.Query(LocalAEEdit.Text,
                        TargetAEEdit.Text,
                        HostEdit.Text,
                        ushort.Parse(PortEdit.Text),
                        "1.2.840.10008.5.1.4.31",  /// Modality Worklist SOP Class
                        q);

                LoadResultsToGrid(it);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query failed: " + ex.Message);
            }
        }

        /* This has to be called for all the com objects to release the
           memory!!! */
        private void ReleaseComObject(object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDateTo.Checked)
            {
                checkDateExact.Checked = false;
                dateEndDate.Enabled = true;
            }
            else
                dateEndDate.Enabled = false;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDateFrom.Checked)
            {
                checkDateExact.Checked = false;
                dateStartDate.Enabled = true;
            }
            else
                dateStartDate.Enabled = false;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDateExact.Checked)
            {
                dateExact.Enabled = true;
                checkDateTo.Checked = false;
                checkDateFrom.Checked = false;
            }
            else
            {
                dateExact.Enabled = false;
            }
        }

        private void checkTimeExact_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTimeExact.Checked)
            {
                timeExact.Enabled = true;
                checkTimeFrom.Checked = false;
                checkTimeTo.Checked = false;
            }
            else
            {
                timeExact.Enabled = false;
            }
        }

        private void checkTimeFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTimeFrom.Checked)
            {
                checkTimeExact.Checked = false;
                timeFrom.Enabled = true;
            }
            else
                timeFrom.Enabled = false;
        }

        private void checkTimeTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTimeTo.Checked)
            {
                checkTimeExact.Checked = false;
                timeTo.Enabled = true;
            }
            else
                timeTo.Enabled = false;
        }

        private void ModalityWorklistSCUExampleForm_Load(object sender, EventArgs e)
        {
            // Bind the DataGridView controls to the BindingSource
            // components and load the data from the database.
            dgvRP.DataSource = bsRP;
            dgvSPS.DataSource = bsSPS;
            dgvMPPS.DataSource = bsMPPS;
            CreateGridsDataLayout();
            enableMPPSButtons(false);
        }

        private NetConnectionInfo GetConnectionInfo()
        {
            NetConnectionInfo conn = new NetConnectionInfo();
            conn.CalledETitle = TargetAEEdit.Text;
            conn.CallingAETitle = LocalAEEdit.Text;
            conn.Host = HostEdit.Text;
            conn.Port = UInt16.Parse(PortEdit.Text);
            return conn;
        }

        /// <summary>
        /// Create a MPPS and add it to the grid. Make it selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mppsStartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRP.SelectedRows.Count > 0)
                {
                    DCXOBJ rp = dgvRP.SelectedRows[0].Cells["OBJ"].Value as DCXOBJ;
                    if (dgvSPS.SelectedRows.Count > 0)
                    {
                        DCXOBJ sps = dgvSPS.SelectedRows[0].Cells["OBJ"].Value as DCXOBJ;
                        MPPS mpps = new MPPS(rp, sps);
                        mpps.Create(GetConnectionInfo());
                        AddRowToMPPSGrid(mpps);
                        foreach (DataGridViewRow row in dgvMPPS.SelectedRows)
                            row.Selected = false;
                        dgvMPPS.Rows[dgvMPPS.Rows.Count - 1].Selected = true;
                        MessageBox.Show("Done");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mppsCompleteBtn_Click(object sender, EventArgs e)
        {
            SendNSet(true);
        }

        private void mppsAbortBtn_Click(object sender, EventArgs e)
        {
            SendNSet(false);
        }

        private void SendNSet(bool completed)
        {
            try
            {
                if (dgvMPPS.SelectedRows.Count == 1)
                {
                    MPPS mpps = (dgvMPPS.SelectedRows[0].Cells["OBJ"].Value as MPPS);
                    mpps.Set(completed, GetConnectionInfo());
                    dgvMPPS.SelectedRows[0].Cells["State"].Value = mpps.State.ToString();
                    MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvQueryResults_SelectionChanged(object sender, EventArgs e)
        {
            enableMPPSButtons(dgvRP.SelectedRows.Count * dgvSPS.SelectedRows.Count != 0);
            if (dgvSPS.Rows.Count == 1)
                dgvSPS.Rows[0].Selected = true;
        }

        private void dataGridSPS_SelectionChanged(object sender, EventArgs e)
        {
            enableMPPSButtons(dgvRP.SelectedRows.Count * dgvSPS.SelectedRows.Count != 0);
        }

        private void enableMPPSButtons(bool enabled)
        {
            mppsStartBtn.Enabled = enabled;
            mppsCompleteBtn.Enabled = enabled;
            mppsAbortBtn.Enabled = enabled;
        }

        private void btnBrowseForQuery_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtQueryFilePath.Text = openFileDialog1.FileName;
            else
                txtQueryFilePath.Text = null;
        }
    }
}