using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using JLSAutoCode.Common;
using JLSAutoCode.Hibernate;
using JLSAutoCode.UI;

namespace JLSAutoCode
{
    public partial class FormHManager : Form
    {
        private List<HField> m_Datas = new List<HField>();

        private HTable m_HTable = new HTable();

        public FormHManager()
        {
            InitializeComponent();
        }

        private void FormHManager_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            lvwMain.Items.Clear();

            foreach (HField field in m_Datas)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = field.CHName;
                lvItem.SubItems.Add(field.ENName);
                lvItem.SubItems.Add(field.Type.ToString());
                lvItem.SubItems.Add(field.Length.ToString());
                lvItem.Tag = field;

                lvwMain.Items.Add(lvItem);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            m_HTable = new HTable();

            txtENName.Text = "";
            txtCHName.Text = "";
            txtClassName.Text = "";

            m_Datas = new List<HField>();

            this.Text = "";

            lvwMain.Items.Clear();

            saveFileDialog1.FileName = "";
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHDetail form = new FormHDetail(DetailFormUseState.Add);
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_Datas.Add(form.Entity);

                RefreshList();
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwMain.SelectedItems.Count <= 0) return;

            HField field = lvwMain.SelectedItems[0].Tag as HField;
            FormHDetail form = new FormHDetail(DetailFormUseState.Edit);
            form.Entity = field;
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshList();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwMain.SelectedItems.Count <= 0) return;

            HField field = lvwMain.SelectedItems[0].Tag as HField;

            m_Datas.Remove(field);

            RefreshList();
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            m_Datas.Clear();

            RefreshList();
        }

        private void 生成结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                GetHTableFromUI();

                HHelperDefault helper = HHelperFactory.GetHHelper(SystemConfig.SystemConfigTag.DBType);

                helper.BuildCode(m_Datas, m_HTable, folderBrowserDialog1.SelectedPath);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SystemConfig.SystemConfigTag.OpenSaveDefaultPath))
            {
                //从配置读取初始路径，避免每次打开或保存文件都要选择路径
                saveFileDialog1.InitialDirectory = SystemConfig.SystemConfigTag.OpenSaveDefaultPath;
            }

            if (string.IsNullOrEmpty(saveFileDialog1.FileName))
            {
                saveFileDialog1.FileName = txtClassName.Text + "(" + txtCHName.Text + ")";
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ResourceHelper resourceHelper = new ResourceHelper(saveFileDialog1.FileName);

                resourceHelper.SetObject(HConst.FieldListKey, m_Datas);

                GetHTableFromUI();

                resourceHelper.SetObject(HConst.HTableKey, m_HTable);

                resourceHelper.Save();

                this.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SystemConfig.SystemConfigTag.OpenSaveDefaultPath))
            {
                //从配置读取初始路径，避免每次打开或保存文件都要选择路径
                openFileDialog1.InitialDirectory = SystemConfig.SystemConfigTag.OpenSaveDefaultPath;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ResourceHelper resourceHelper = new ResourceHelper(openFileDialog1.FileName);

                m_HTable = resourceHelper.GetObject(HConst.HTableKey) as HTable;

                if (m_HTable == null) m_HTable = new HTable();

                txtENName.Text = m_HTable.ENName;
                txtCHName.Text = m_HTable.CHName;
                txtClassName.Text = m_HTable.ClassName;

                m_Datas = resourceHelper.GetObject(HConst.FieldListKey) as List<HField>;

                RefreshList();

                this.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }
        }

        private void GetHTableFromUI()
        {
            if (m_HTable == null) m_HTable = new HTable();

            m_HTable.ENName = txtENName.Text;
            m_HTable.CHName = txtCHName.Text;
            m_HTable.ClassName = txtClassName.Text;
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwMain.SelectedItems.Count <= 0) return;

            HField field = lvwMain.SelectedItems[0].Tag as HField;

            int idx = m_Datas.IndexOf(field);
            if (idx > 0)
            {
                m_Datas.Remove(field);
                m_Datas.Insert(idx - 1, field);
                RefreshList();
            }
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwMain.SelectedItems.Count <= 0) return;

            HField field = lvwMain.SelectedItems[0].Tag as HField;

            int idx = m_Datas.IndexOf(field);
            if (idx < m_Datas.Count - 1)
            {
                m_Datas.Remove(field);
                m_Datas.Insert(idx + 1, field);
                RefreshList();
            }
        }

        private void lvwMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            修改ToolStripMenuItem_Click(null, null);
        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogCsv.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamReader tStreamReader = new System.IO.StreamReader(openFileDialogCsv.FileName, Encoding.Default))
                    {
                        //从csv读取字段信息
                        tStreamReader.Peek();
                        while (tStreamReader.Peek() > 0)
                        {
                            string str = tStreamReader.ReadLine();
                            string[] split = str.Split(',');
                            if (split[0] != "" && split[1] != "")
                            {
                                AddFieldByText(split);
                            }
                        }
                    }

                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCopyFromPD_Click(object sender, EventArgs e)
        {
            try
            {
                FormCopyFromPD form = new FormCopyFromPD();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string value = form.Result;

                    List<string> lines = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                    //去掉第一行，第一行是字段名
                    if (lines.Count > 0)
                        lines.RemoveAt(0);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrEmpty(line)) continue;

                        string[] cells = line.Split(new string[] { "	" }, StringSplitOptions.None);
                        if (cells.Length >= 4)
                        {
                            AddFieldByText(cells);
                        }
                    }

                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStstemConfig_Click(object sender, EventArgs e)
        {
            FormSystemConfig form = new FormSystemConfig();
            form.ShowDialog();
        }
   
        /// <summary>
        /// 从文本中添加字段
        /// </summary>
        /// <param name="cells"></param>
        private void AddFieldByText(string[] cells)
        {
            HField Entity = new HField();

            Entity.CHName = cells[0];
            Entity.ENName = cells[1];

            string strType = cells[2];
            strType = strType.ToUpper();
            HFieldType type = HFieldType.NVARCHAR2;
            if (strType.Contains("NUMBER") || strType.Contains("COUNTER"))
            {
                type = HFieldType.NUMBER;
            }
            else if (strType.Contains("NVARCHAR2") || strType.Contains("TEXT") || strType.Contains("MEMO"))
            {
                type = HFieldType.NVARCHAR2;
            }
            else if (strType.Contains("DATE") || strType.Contains("DATETIME"))
            {
                type = HFieldType.DATE;
            }
            else if (strType.Contains("FLOAT"))
            {
                type = HFieldType.FLOAT;
            }
            else if (strType.Contains("INTEGER"))
            {
                type = HFieldType.INT;
            }
            else if (strType.Contains("CLOB"))
            {
                type = HFieldType.CLOB;
            }
            else if (strType.Contains("VARBINARY"))
            {
                type = HFieldType.BINARY;
            }
            Entity.Type = type;

            if (cells.Length >= 4)
            {
                string strLength = cells[3];
                int length = 0;
                if (!string.IsNullOrEmpty(strLength) && int.TryParse(strLength, out length) == true)
                {
                    Entity.Length = length;
                }
            }

            m_Datas.Add(Entity);
        }     
    }
}
