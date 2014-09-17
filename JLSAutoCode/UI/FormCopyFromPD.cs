using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode.UI
{
    public partial class FormCopyFromPD : Form
    {
        public string Result = "";

        public FormCopyFromPD()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = txtContent.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
