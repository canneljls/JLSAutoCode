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
    public partial class FormHDetail : Form
    {
        private DetailFormUseState m_UseState = DetailFormUseState.Add;

        public HField Entity
        {
            get;
            set;
        }

        public FormHDetail(DetailFormUseState useState)
        {
            m_UseState = useState;

            InitializeComponent();

            cmbType.DataSource = System.Enum.GetNames(typeof(HFieldType));
        }

        private void FormHDetail_Load(object sender, EventArgs e)
        {
            if (m_UseState == DetailFormUseState.Edit)
            {
                if (this.Entity != null)
                {
                    txtCHName.Text = Entity.CHName;
                    txtENName.Text = Entity.ENName;
                    cmbType.Text = Entity.Type.ToString();
                    nudLength.Value = Entity.Length;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Entity == null)
                Entity = new HField();

            Entity.CHName = txtCHName.Text;
            Entity.ENName = txtENName.Text;
            Entity.Type = (HFieldType)Enum.Parse(typeof(HFieldType), cmbType.SelectedItem.ToString(), false);
            Entity.Length = (int)nudLength.Value;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
