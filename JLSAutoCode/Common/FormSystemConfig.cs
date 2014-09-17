using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode.Common
{
    public partial class FormSystemConfig : Form
    {
        public FormSystemConfig()
        {
            InitializeComponent();
        }

        private void FormSystemConfig_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> templatePaths = Directory.GetDirectories(CommonConst.TemplatePath).ToList();
                foreach (string templatePath in templatePaths)
                {
                    DirectoryInfo di = new DirectoryInfo(templatePath);

                    cmbTemplateName.Items.Add(di.Name);
                }

                cmbTemplateName.Text = SystemConfig.SystemConfigTag.TemplateName;
                cmbDBType.Text = SystemConfig.SystemConfigTag.DBType;
                txtOpenSaveDefaultPath.Text = SystemConfig.SystemConfigTag.OpenSaveDefaultPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SystemConfig.SystemConfigTag.TemplateName = cmbTemplateName.Text;
                SystemConfig.SystemConfigTag.DBType = cmbDBType.Text;
                SystemConfig.SystemConfigTag.OpenSaveDefaultPath = txtOpenSaveDefaultPath.Text;

                SystemConfig.Save();

                MessageBox.Show("保存成功");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
