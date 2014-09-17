using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode
{
    public partial class FormHResult : Form
    {
        public void SetText(string content)
        {
            txtMain.Text = content;
        }

        public FormHResult()
        {
            InitializeComponent();
        }
    }
}
