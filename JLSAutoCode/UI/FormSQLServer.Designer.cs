namespace JLSAutoCode
{
    partial class FormSQLServer
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.btnGetAllTable = new System.Windows.Forms.Button();
            this.lvwTable = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAspx = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(91, 12);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(173, 21);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "gisserver";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据库名称";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(91, 39);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(173, 21);
            this.txtDB.TabIndex = 2;
            this.txtDB.Text = "HiGISGeoFramework";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "用户名";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(91, 66);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(173, 21);
            this.txtUser.TabIndex = 4;
            this.txtUser.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "密码";
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(91, 93);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(173, 21);
            this.txtPw.TabIndex = 6;
            this.txtPw.Text = "sasa";
            // 
            // btnGetAllTable
            // 
            this.btnGetAllTable.Location = new System.Drawing.Point(91, 120);
            this.btnGetAllTable.Name = "btnGetAllTable";
            this.btnGetAllTable.Size = new System.Drawing.Size(122, 23);
            this.btnGetAllTable.TabIndex = 8;
            this.btnGetAllTable.Text = "获取所有表";
            this.btnGetAllTable.UseVisualStyleBackColor = true;
            this.btnGetAllTable.Click += new System.EventHandler(this.btnGetAllTable_Click);
            // 
            // lvwTable
            // 
            this.lvwTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwTable.FullRowSelect = true;
            this.lvwTable.GridLines = true;
            this.lvwTable.Location = new System.Drawing.Point(24, 149);
            this.lvwTable.Name = "lvwTable";
            this.lvwTable.Size = new System.Drawing.Size(240, 318);
            this.lvwTable.TabIndex = 9;
            this.lvwTable.UseCompatibleStateImageBehavior = false;
            this.lvwTable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "表名";
            this.columnHeader1.Width = 205;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(24, 473);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(207, 21);
            this.txtLocation.TabIndex = 10;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(237, 473);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(26, 23);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(91, 500);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(122, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "生成ORM代码";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAspx
            // 
            this.btnAspx.Location = new System.Drawing.Point(91, 529);
            this.btnAspx.Name = "btnAspx";
            this.btnAspx.Size = new System.Drawing.Size(122, 23);
            this.btnAspx.TabIndex = 13;
            this.btnAspx.Text = "生成aspx代码";
            this.btnAspx.UseVisualStyleBackColor = true;
            this.btnAspx.Click += new System.EventHandler(this.btnAspx_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 613);
            this.Controls.Add(this.btnAspx);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lvwTable);
            this.Controls.Add(this.btnGetAllTable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "代码生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.Button btnGetAllTable;
        private System.Windows.Forms.ListView lvwTable;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAspx;
    }
}

