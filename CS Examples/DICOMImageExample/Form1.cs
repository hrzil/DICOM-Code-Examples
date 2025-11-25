using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using rzdcxLib;

namespace DICOMImageExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                dcmfile.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                bmpFile.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DCXIMG img = new DCXIMG();
            img.LoadFile(dcmfile.Text);
            img.SaveBitmap(0, bmpFile.Text);
        }
    }
}
