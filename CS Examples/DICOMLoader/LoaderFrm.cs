using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using rzdcxLib;
using System.IO;

namespace DICOMLoader
{
    public partial class LoaderFrm : Form
    {
        public LoaderFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Execute some GUI action from another thread
        /// </summary>
        /// <param name="a"></param>
        private void DoUiStuff(Action a)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(a));
                else
                    a();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Read Sop Instance UID from the given DCXOBJ and add it to the list
        /// </summary>
        /// <param name="obj"></param>
        private void OnFileLoad(DCXOBJ obj)
        {
            DoUiStuff(
                    () =>
                    {
                        string SOPInstanceUID = Utils.GetElementValueAsString(obj, (int)DICOM_TAGS_ENUM.sopInstanceUID);
                        this.lbResults.Items.Add(SOPInstanceUID);
                    }
                    );

        }

        private void NotDICOMFile(FileInfo finfo, int errCode, string message)
        {
            DoUiStuff(
                () =>
                {
                    this.lbNotDICOM.Items.Add(
                        "[" + errCode.ToString() + "]" + 
                        "[" + message + "]" + 
                        "[" + finfo.FullName + "]"
                        );
                }
                );
        }

        private void DICOMDIRFound()
        {
            // TODO: Implement
        }

        private void btnLoadFolder_Click(object sender, EventArgs e)
        {
            if (this.fbd.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = this.fbd.SelectedPath;
                this.lbResults.Items.Clear();
                Thread th = new Thread(this.StartLoadFolder);
                th.Start();

            }
        }

        /// <summary>
        /// Start loading of the folder selected by user
        /// </summary>
        private void StartLoadFolder()
        {
            DICOMLoader loader = CreateLoader();
            loader.LoadDirectory(this.fbd.SelectedPath);
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (this.ofd.ShowDialog() == DialogResult.OK)
            {
                this.lbResults.Items.Clear();
                Thread th = new Thread(this.StartLoadFiles);
                th.Start();
            }
        }

        /// <summary>
        /// Start loading of the files selected by user
        /// </summary>
        private void StartLoadFiles()
        {
            DICOMLoader loader = CreateLoader();
            loader.LoadFiles(this.ofd.FileNames);
        }

        private DICOMLoader CreateLoader()
        {
            DICOMLoader loader = new DICOMLoader();
            loader.OnFileLoad += this.OnFileLoad;
            loader.OnNotDICOMFile += this.NotDICOMFile;
            loader.OnLoadFileError += Loader_OnLoadFileError;

            return loader;
        }

        private void Loader_OnLoadFileError(FileInfo fileInfo, Exception e)
        {
            // You may want to handle it differently
            lbNotDICOM.Items.Add(fileInfo.FullName);
        }
    }
}
