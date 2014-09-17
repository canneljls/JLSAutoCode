using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace JLSAutoCode
{
    public class SQLServerConnect : OracleConnect
    {
        public ArrayList GetColName(string tbName)
        {
            ArrayList TablesList = new ArrayList();
            OleDbConnection Conn = this.getcon();
            DataTable dt = new DataTable();
            object[] objs = new object[4];
            objs[2] = tbName;
            dt = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, objs);
            foreach (DataRow dtRow in dt.Rows)
            {
                TablesList.Add(dtRow["COLUMN_NAME"].ToString().Trim());
            }
            base.con_close();
            return TablesList;
        }

        public override OleDbConnection getcon()
        {
            try
            {
                if ((base.m_Str_Orclcon == "") && (base.Db_Datasource == ""))
                {
                    return null;
                }
                if ((base.m_Str_Orclcon == "") && (base.Db_Datasource != ""))
                {
                    //base.m_Str_Orclcon = "Provider=SQLOLEDB;Date Source=" + base.Db_IP + ";Initial Catalog=" + base.Db_Datasource + ";uid=" + base.UserName + ";pwd=" + base.PassWord;
                    //base.m_Str_Orclcon = "Provider=SQLOLEDB;Date Source=" + base.Db_IP + ";Initial Catalog=" + base.Db_Datasource + ";User ID=" + base.UserName + ";Password=" + base.PassWord;
                    //base.m_Str_Orclcon = "Provider=SQLOLEDB;Persist Security Info=False;Date Source=192.168.16.16;Initial Catalog=jtdasjs_KongBai_JN;User ID=sa;Password=123;";
                    base.m_Str_Orclcon = "Provider=SQLOLEDB;Persist Security Info=False;Data Source=" + base.Db_IP + ";Initial Catalog=" + base.Db_Datasource + ";User ID=" + base.UserName + ";Password=" + base.PassWord + ";";
                }
                base.My_con = new OleDbConnection(base.m_Str_Orclcon);
                base.My_con.Open();
                return base.My_con;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ArrayList GetTableName()
        {
            ArrayList TablesList = new ArrayList();
            OleDbConnection Conn = this.getcon();
            DataTable dt = new DataTable();
            dt = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[1]);
            foreach (DataRow dtRow in dt.Rows)
            {
                if (dtRow["TABLE_TYPE"].ToString().Trim() == "TABLE")
                {
                    TablesList.Add(dtRow["TABLE_NAME"].ToString().Trim());
                }
            }
            base.con_close();
            return TablesList;
        }
    }
}
