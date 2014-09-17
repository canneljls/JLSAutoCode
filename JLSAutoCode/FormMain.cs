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
    public partial class FormMain : Form
    {
        public int DBType = -1;

        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DBType = 0;
            //this.Close();

            //Form1 form = new Form1();
            FormSQLServer form = new FormSQLServer();
            form.ShowDialog();

            //form.Show();

            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DBType = 1;
            //this.Close();

            FormMySQL form = new FormMySQL();
            form.ShowDialog();

            //form.Show();

            //this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBType = 2;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormHManager form = new FormHManager();
            form.ShowDialog();

            //DBType = 3;
            //this.Close();
        }
    }
}
