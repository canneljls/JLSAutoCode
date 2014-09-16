namespace AutoCopyJAR
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkJar = new System.Windows.Forms.CheckBox();
            this.lvwJar = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnJarDel = new System.Windows.Forms.Button();
            this.btnJarAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvwTar = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTarDel = new System.Windows.Forms.Button();
            this.btnTarAdd = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAutoCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbdJar = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdTar = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkJar);
            this.groupBox1.Controls.Add(this.lvwJar);
            this.groupBox1.Controls.Add(this.btnJarDel);
            this.groupBox1.Controls.Add(this.btnJarAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(716, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "jar包目录";
            // 
            // chkJar
            // 
            this.chkJar.AutoSize = true;
            this.chkJar.Location = new System.Drawing.Point(182, 24);
            this.chkJar.Name = "chkJar";
            this.chkJar.Size = new System.Drawing.Size(72, 16);
            this.chkJar.TabIndex = 4;
            this.chkJar.Text = "批量选择";
            this.chkJar.UseVisualStyleBackColor = true;
            this.chkJar.CheckedChanged += new System.EventHandler(this.chkJar_CheckedChanged);
            // 
            // lvwJar
            // 
            this.lvwJar.CheckBoxes = true;
            this.lvwJar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwJar.FullRowSelect = true;
            this.lvwJar.GridLines = true;
            this.lvwJar.Location = new System.Drawing.Point(6, 49);
            this.lvwJar.MultiSelect = false;
            this.lvwJar.Name = "lvwJar";
            this.lvwJar.Size = new System.Drawing.Size(704, 222);
            this.lvwJar.TabIndex = 3;
            this.lvwJar.UseCompatibleStateImageBehavior = false;
            this.lvwJar.View = System.Windows.Forms.View.Details;
            this.lvwJar.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwJar_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "路径";
            this.columnHeader1.Width = 667;
            // 
            // btnJarDel
            // 
            this.btnJarDel.Location = new System.Drawing.Point(74, 20);
            this.btnJarDel.Name = "btnJarDel";
            this.btnJarDel.Size = new System.Drawing.Size(62, 23);
            this.btnJarDel.TabIndex = 2;
            this.btnJarDel.Text = "删除";
            this.btnJarDel.UseVisualStyleBackColor = true;
            this.btnJarDel.Click += new System.EventHandler(this.btnJarDel_Click);
            // 
            // btnJarAdd
            // 
            this.btnJarAdd.Location = new System.Drawing.Point(6, 20);
            this.btnJarAdd.Name = "btnJarAdd";
            this.btnJarAdd.Size = new System.Drawing.Size(62, 23);
            this.btnJarAdd.TabIndex = 0;
            this.btnJarAdd.Text = "新增";
            this.btnJarAdd.UseVisualStyleBackColor = true;
            this.btnJarAdd.Click += new System.EventHandler(this.btnJarAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvwTar);
            this.groupBox2.Controls.Add(this.btnTarDel);
            this.groupBox2.Controls.Add(this.btnTarAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(716, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "复制目标目录";
            // 
            // lvwTar
            // 
            this.lvwTar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvwTar.FullRowSelect = true;
            this.lvwTar.GridLines = true;
            this.lvwTar.Location = new System.Drawing.Point(6, 49);
            this.lvwTar.MultiSelect = false;
            this.lvwTar.Name = "lvwTar";
            this.lvwTar.Size = new System.Drawing.Size(704, 95);
            this.lvwTar.TabIndex = 3;
            this.lvwTar.UseCompatibleStateImageBehavior = false;
            this.lvwTar.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "路径";
            this.columnHeader2.Width = 667;
            // 
            // btnTarDel
            // 
            this.btnTarDel.Location = new System.Drawing.Point(74, 20);
            this.btnTarDel.Name = "btnTarDel";
            this.btnTarDel.Size = new System.Drawing.Size(62, 23);
            this.btnTarDel.TabIndex = 2;
            this.btnTarDel.Text = "删除";
            this.btnTarDel.UseVisualStyleBackColor = true;
            this.btnTarDel.Click += new System.EventHandler(this.btnTarDel_Click);
            // 
            // btnTarAdd
            // 
            this.btnTarAdd.Location = new System.Drawing.Point(6, 20);
            this.btnTarAdd.Name = "btnTarAdd";
            this.btnTarAdd.Size = new System.Drawing.Size(62, 23);
            this.btnTarAdd.TabIndex = 0;
            this.btnTarAdd.Text = "新增";
            this.btnTarAdd.UseVisualStyleBackColor = true;
            this.btnTarAdd.Click += new System.EventHandler(this.btnTarAdd_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnAutoCopy});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(973, 25);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 21);
            this.btnSave.Text = "保存配置";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAutoCopy
            // 
            this.btnAutoCopy.Name = "btnAutoCopy";
            this.btnAutoCopy.Size = new System.Drawing.Size(68, 21);
            this.btnAutoCopy.Text = "自动复制";
            this.btnAutoCopy.Click += new System.EventHandler(this.btnAutoCopy_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOK);
            this.groupBox3.Controls.Add(this.txtTitle);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(734, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 435);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "其他配置";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(16, 20);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(51, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "应用";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(82, 49);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(136, 21);
            this.txtTitle.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "窗体标题";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 470);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip2);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jar自动复制工具";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvwJar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnJarDel;
        private System.Windows.Forms.Button btnJarAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvwTar;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnTarDel;
        private System.Windows.Forms.Button btnTarAdd;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnAutoCopy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkJar;
        private System.Windows.Forms.FolderBrowserDialog fbdJar;
        private System.Windows.Forms.FolderBrowserDialog fbdTar;
    }
}

