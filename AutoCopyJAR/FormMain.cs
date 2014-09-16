using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCopyJAR
{
    public partial class FormMain : Form
    {
        private MainConfig m_MainConfig = null;

        private List<JarPath> m_Jars = null;

        private List<TarPath> m_Tars = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(CommonConst.ConfigFilePath))
            {
                ResourceHelper resourceHelper = new ResourceHelper(CommonConst.ConfigFilePath);

                m_MainConfig = resourceHelper.GetObject(CommonConst.MainConfigKey) as MainConfig;

                m_Jars = resourceHelper.GetObject(CommonConst.JarPathKey) as List<JarPath>;

                m_Tars = resourceHelper.GetObject(CommonConst.TarPathKey) as List<TarPath>;
            }

            if (m_MainConfig == null) m_MainConfig = new MainConfig();
            if (m_Jars == null) m_Jars = new List<JarPath>();
            if (m_Tars == null) m_Tars = new List<TarPath>();

            RefreshUI();
        }

        private void RefreshUI()
        {
            lvwJar.Items.Clear();
            lvwTar.Items.Clear();

            this.Text = m_MainConfig.Title + " - Jar自动复制工具";
            txtTitle.Text = m_MainConfig.Title;

            foreach (JarPath jar in m_Jars)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = jar.Path;
                lvItem.Checked = jar.Select;
                lvItem.Tag = jar;

                lvwJar.Items.Add(lvItem);
            }

            foreach (TarPath tar in m_Tars)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = tar.Path;               
                lvItem.Tag = tar;

                lvwTar.Items.Add(lvItem);
            }
        }

        private void btnJarAdd_Click(object sender, EventArgs e)
        {
            FormSelectPath form = new FormSelectPath("", fbdJar);
            if (form.ShowDialog() == DialogResult.OK)
            {
                JarPath jar = new JarPath();
                jar.Path = form.Path;
                jar.Select = true;

                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = jar.Path;
                lvItem.Checked = jar.Select;
                lvItem.Tag = jar;

                lvwJar.Items.Add(lvItem);
                m_Jars.Add(jar);
            }
        }

        private void btnJarDel_Click(object sender, EventArgs e)
        {
            if (lvwJar.SelectedItems.Count < 1) return;

            ListViewItem lvItem = lvwJar.SelectedItems[0];

            m_Jars.Remove(lvItem.Tag as JarPath);
            lvwJar.Items.Remove(lvItem);
        }

        private void btnTarAdd_Click(object sender, EventArgs e)
        {
            FormSelectPath form = new FormSelectPath("", fbdTar);
            if (form.ShowDialog() == DialogResult.OK)
            {
                TarPath jar = new TarPath();
                jar.Path = form.Path;             

                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = jar.Path;            
                lvItem.Tag = jar;

                lvwTar.Items.Add(lvItem);
                m_Tars.Add(jar);
            }
        }

        private void btnTarDel_Click(object sender, EventArgs e)
        {
            if (lvwTar.SelectedItems.Count < 1) return;

            ListViewItem lvItem = lvwTar.SelectedItems[0];

            m_Tars.Remove(lvItem.Tag as TarPath);
            lvwTar.Items.Remove(lvItem);
        }

        private void chkJar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvwJar.Items)
            {
                lvItem.Checked = chkJar.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_MainConfig.Title = txtTitle.Text;
            this.Text = m_MainConfig.Title + " - Jar自动复制工具";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResourceHelper resourceHelper = new ResourceHelper(CommonConst.ConfigFilePath);

            m_MainConfig.Title = txtTitle.Text;

            resourceHelper.SetObject(CommonConst.MainConfigKey, m_MainConfig);

            resourceHelper.SetObject(CommonConst.JarPathKey, m_Jars);

            resourceHelper.SetObject(CommonConst.TarPathKey, m_Tars);

            resourceHelper.Save();

            MessageBox.Show("保存完成");
        }

        private void lvwJar_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem lvItem = e.Item;

            JarPath jar = lvItem.Tag as JarPath;
            jar.Select = lvItem.Checked;
        }

        private void btnAutoCopy_Click(object sender, EventArgs e)
        {
            List<string> targetPaths = new List<string>();
            foreach (TarPath tar in m_Tars)
            {
                targetPaths.Add(tar.Path);
            }

            foreach (JarPath jar in m_Jars)
            {
                if (jar.Select == false) continue;

                List<string> jarFiles = Directory.GetFiles(jar.Path, "*.jar").ToList();

                foreach (string jarFile in jarFiles)
                {
                    string fileName = Path.GetFileName(jarFile);

                    foreach (string targetPath in targetPaths)
                    {
                        string fileTargetName = Path.Combine(targetPath, fileName);

                        if (File.Exists(fileTargetName))
                        {
                            File.Delete(fileTargetName);
                        }

                        File.Copy(jarFile, fileTargetName);
                    }
                }
            }

            MessageBox.Show("复制完成");
        }       
    }
}
