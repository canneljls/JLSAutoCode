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
    public partial class FormMySQL : Form
    {
        public FormMySQL()
        {
            InitializeComponent();
        }

        private MySqlConnection GetConn()
        {
            string server = txtServer.Text;
            string port = txtPort.Text;
            string DB = txtDB.Text;
            string user = txtUser.Text;
            string pw = txtPw.Text;

            //string connString = "Provider=SQLOLEDB;Persist Security Info=False;Data Source=" + server + ";Initial Catalog=" + DB + ";User ID=" + user + ";Password=" + pw + ";";

            string connString = "Data Source=" + server + ";" + "port=" + port + ";" + "Database=" + DB + ";" + "User Id=" + user + ";" + "Password=" + pw + ";" + "CharSet = utf8";

            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            return conn;
        }

        private void btnGetAllTable_Click(object sender, EventArgs e)
        {
            lvwTable.Items.Clear();

            MySqlConnection conn = GetConn();

            string DB = txtDB.Text;
            string sql = "select table_name from INFORMATION_SCHEMA.TABLES Where table_schema = '" + DB + "' ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);            

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
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
                    at.DatabaseName = DB;
                    //at.Id = Convert.ToString(dr[1]);
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

            MySqlConnection conn = GetConn();

            foreach (ACTable table in tables)
            {
                table.Fields.Clear();

                //string sql = "select [name],[type] from [syscolumns] where [id] = " + table.Id;
                string sql = "Select COLUMN_NAME, DATA_TYPE " +
                    " from INFORMATION_SCHEMA.COLUMNS " +
                    " Where table_name = '" + table.Name + "' " +
                    " AND table_schema = '" + table.DatabaseName + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    ACField field = new ACField();
                    field.Name = Convert.ToString(dr[0]);
                    //field.Comment = Convert.ToString(dr[4]);
                    //string typeStr = Convert.ToString(dr[3]);
                    ////字符类型字段
                    //if (typeStr == "nvarchar")
                    //{
                    //    field.Type = ACFieldType.text;
                    //}
                    ////数值类型字段
                    //else if (typeStr == "int")
                    //{
                    //    field.Type = ACFieldType.number;
                    //}
                    ////其他类型
                    //else
                    //{
                    //    field.Type = ACFieldType.other;
                    //}

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

        private void button1_Click(object sender, EventArgs e)
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
                        sw.Write(field.Name + "=" + field.Name + ",");
                    }
                    sw.WriteLine("");
                    sw.WriteLine("****************************");

                    foreach (ACField field in table.Fields)
                    {
                        sw.Write(field.Name + ",");
                    }
                    sw.WriteLine("");
                    sw.WriteLine("****************************");

                    sw.Close();
                }
            }

            MessageBox.Show("生成完毕");
        }
    }
}
