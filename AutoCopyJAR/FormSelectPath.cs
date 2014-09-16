using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCopyJAR
{
    public partial class FormSelectPath : Form
    {
        private string m_Path = "";

        public string Path
        {
            get
            {
                return m_Path;
            }
        }

        private FolderBrowserDialog m_Fbd = null;

        public FormSelectPath(string path, FolderBrowserDialog fbd)
        {
            m_Path = path;
            m_Fbd = fbd;

            InitializeComponent();
        }

        private void FormSelectPath_Load(object sender, EventArgs e)
        {
            txtPath.Text = m_Path;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (m_Fbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = m_Fbd.SelectedPath;           
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_Path = txtPath.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
