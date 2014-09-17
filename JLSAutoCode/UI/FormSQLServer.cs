using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace JLSAutoCode
{
    public partial class FormSQLServer : Form
    {
        public FormSQLServer()
        {
            InitializeComponent();
        }

        private OleDbConnection GetConn()
        {
            string server = txtServer.Text;
            string DB = txtDB.Text;
            string user = txtUser.Text;
            string pw = txtPw.Text;

            string connString = "Provider=SQLOLEDB;Persist Security Info=False;Data Source=" + server + ";Initial Catalog=" + DB + ";User ID=" + user + ";Password=" + pw + ";";

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();

            return conn;
        }

        private void btnGetAllTable_Click(object sender, EventArgs e)
        {
            lvwTable.Items.Clear();
         
            OleDbConnection conn = GetConn();
           
            string sql = "select [name],[id] from [sysobjects] where [type] = 'u' order  by name";

            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                string tableName = Convert.ToString(dr[0]);
                if (!string.IsNullOrEmpty(tableName))
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = tableName;

                    ACTable at = new ACTable();
                    at.Name = tableName;
                    at.Id = Convert.ToString(dr[1]);
                    lvItem.Tag = at;

                    lvwTable.Items.Add(lvItem);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = f.SelectedPath;
            }
        }

        public List<ACTable> GetSelectTable()
        {
            List<ACTable> tables = new List<ACTable>();
            foreach (ListViewItem lvItem in lvwTable.SelectedItems)
            {
                tables.Add(lvItem.Tag as ACTable);
            }

            OleDbConnection conn = GetConn();

            foreach (ACTable table in tables)
            {
                table.Fields.Clear();

                //string sql = "select [name],[type] from [syscolumns] where [id] = " + table.Id;
                string sql = "select col.name as ColumnName, col.max_length as DataLength,col.is_nullable as IsNullable, " + "t.name as DataType, ep.value as Description  " +
                    " from sys.objects obj " +
                     "inner join sys.columns col " +
                     "on obj.object_id=col.object_id " +
                     "left join sys.types t " +
                     "on t.user_type_id=col.user_type_id " +
                     "left join sys.extended_properties ep " +
                     "on ep.major_id=obj.object_id  " +
                     "and ep.minor_id=col.column_id " +
                     "and ep.name='MS_Description' " +
                     "where obj.name='" + table.Name + "'";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {                  
                    ACField field = new ACField();
                    field.Name = Convert.ToString(dr[0]);
                    field.Comment = Convert.ToString(dr[4]);
                    string typeStr = Convert.ToString(dr[3]);
                    //字符类型字段
                    if (typeStr == "nvarchar")
                    {
                        field.Type = ACFieldType.text;
                    }
                    //数值类型字段
                    else if (typeStr == "int")
                    {
                        field.Type = ACFieldType.number;
                    }
                    //其他类型
                    else
                    {
                        field.Type = ACFieldType.other;
                    }

                    table.Fields.Add(field);
                }
            }

            conn.Close();

            return tables;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<ACTable> tables = GetSelectTable();
           

            MessageBox.Show("生成完毕");
        }

        private void btnAspx_Click(object sender, EventArgs e)
        {
            List<ACTable> tables = GetSelectTable();
            string rootFolder = txtLocation.Text;

            foreach (ACTable table in tables)
            {
                string filePath = Path.Combine(rootFolder, table.Name + ".aspx.cs");

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    foreach (ACField field in table.Fields)
                    {
                        sw.WriteLine("<tr bgcolor=\"#eeeeee\"> " +
                        " <td align=\"right\" style=\"width: 186px; height: 30px;\">");
                        sw.WriteLine(field.Comment + "：</td>");
                        sw.WriteLine("<td width=\"80%\" style=\"height: 30px\"> " +
                            " &nbsp; &nbsp;");
                        sw.WriteLine("<asp:TextBox ID=\"txt" + field.Name + "\" runat=\"server\"></asp:TextBox>");
                        sw.WriteLine("</td></tr>");
                    }

                    sw.WriteLine("****************************");

                    foreach (ACField field in table.Fields)
                    {
                        sw.WriteLine("txt" + field.Name + ".Text = XXX" + table.Name + "." + field.Name + ";");
                    }

                    sw.WriteLine("****************************");

                    foreach (ACField field in table.Fields)
                    {
                        sw.WriteLine("XXX" + table.Name + "." + field.Name + " = txt" + field.Name + ".Text;");
                    }

                    sw.Close();
                }
            }

            MessageBox.Show("生成完毕");
        }
    }
}
