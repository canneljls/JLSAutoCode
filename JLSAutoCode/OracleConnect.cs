using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode
{
    public class OracleConnect
    {
        private string db_Datasource;
        private string db_IP;
        protected string m_Str_Orclcon = "";
        protected OleDbConnection My_con;
        private string passWord;
        private string userName;

        public void con_close()
        {
            if (this.My_con.State == ConnectionState.Open)
            {
                this.My_con.Close();
                this.My_con.Dispose();
            }
        }

        public ArrayList GetColName(string tbName)
        {
            ArrayList arrColName = new ArrayList();
            string realTbName = tbName.ToUpper();
            string sqlGetColName = "select column_name from user_tab_columns where table_name='" + realTbName + "'";
            DataTable dt = this.getDataTableFromSql(sqlGetColName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arrColName.Add(dt.Rows[i][0].ToString());
            }
            return arrColName;
        }

        public virtual OleDbConnection getcon()
        {
            try
            {
                if ((((this.m_Str_Orclcon == "") && (this.db_IP == "")) && ((this.db_Datasource == "") && (this.userName == ""))) && (this.passWord == ""))
                {
                    return null;
                }
                if ((((this.m_Str_Orclcon == "") && (this.db_IP != "")) && ((this.db_Datasource != "") && (this.userName != ""))) && (this.passWord != ""))
                {
                    this.m_Str_Orclcon = "Provider=OraOLEDB.Oracle.1;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + this.db_IP + ")  (PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=" + this.db_Datasource + ")));Persist Security Info=True;user id=" + this.userName + ";password=" + this.passWord;
                }
                this.My_con = new OleDbConnection(this.m_Str_Orclcon);
                this.My_con.Open();
                return this.My_con;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet getDataSet(string sqlStr, string tableName)
        {
            this.getcon();
            OleDbDataAdapter ORCLda = new OleDbDataAdapter(sqlStr, this.My_con);
            DataSet myDataSet = new DataSet();
            ORCLda.Fill(myDataSet, tableName);
            this.con_close();
            return myDataSet;
        }

        public DataSet getDataSetWithOutTbName(string sqlStr)
        {
            this.getcon();
            OleDbDataAdapter ORCLda = new OleDbDataAdapter(sqlStr, this.My_con);
            DataSet myDataSet = new DataSet();
            ORCLda.Fill(myDataSet);
            this.con_close();
            return myDataSet;
        }

        public DataTable getDataTableFromSql(string sqlStr)
        {
            return this.getDataSetWithOutTbName(sqlStr).Tables[0];
        }

        private string GetExp(string expStr)
        {
            if (expStr.Contains("ORA-00001"))
            {
                return "数据已存在";
            }
            return expStr;
        }

        public string GetFieldType(string tbName, string FieldName)
        {
            string sql = "select data_type from all_tab_columns where table_name='" + tbName + "' and column_name='" + FieldName + "'";
            return this.getUniqueValueFromSql(sql);
        }

        public int GetMaxID(string tbName, string idField)
        {
            try
            {
                string sqlstr = "select max(" + idField + ")+1 from " + tbName;
                return Convert.ToInt32(this.getUniqueValueFromSql(sqlstr));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetMetaTable(string sql)
        {
            this.getcon();
            OleDbCommand My_com = this.My_con.CreateCommand();
            My_com.CommandText = sql;
            return My_com.ExecuteReader().GetSchemaTable();
        }

        public int getorclcom(string sqlStr)
        {
            this.getcon();
            OleDbCommand ORCLcom = new OleDbCommand(sqlStr, this.My_con);
            int i = Convert.ToInt32(ORCLcom.ExecuteNonQuery());
            ORCLcom.Dispose();
            this.con_close();
            return i;
        }

        public ArrayList GetTableName()
        {
            ArrayList TablesList = new ArrayList();
            string SQL = "select table_name from user_tables";
            this.getcon();
            OleDbCommand My_com = this.My_con.CreateCommand();
            My_com.CommandText = SQL;
            OleDbDataReader My_read = My_com.ExecuteReader();
            while (My_read.Read())
            {
                TablesList.Add(My_read["TABLE_NAME"].ToString().Trim());
            }
            return TablesList;
        }

        public string getUniqueValueFromSql(string sqlStr)
        {
            DataTable dtValues = this.getDataTableFromSql(sqlStr);
            if (dtValues.Rows.Count > 0)
            {
                return dtValues.Rows[0][0].ToString();
            }
            return "";
        }

        public string InsertDtToOrcl(string orclConStr, DataTable dt, string tbName)
        {
            int i;
            string returnMsg = "";
            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder sb = new StringBuilder();
            string colName = string.Empty;
            string colNames = string.Empty;
            string colNamePramas = string.Empty;
            string colType = string.Empty;
            for (i = 0; i < cols; i++)
            {
                colName = dt.Columns[i].ColumnName.ToString();
                colType = dt.Columns[i].DataType.ToString();
                colType = this.NetDataTypeToOracleType(colType);
                if (i == 0)
                {
                    colNames = colNames + colName;
                    colNamePramas = colNamePramas + ":" + colName;
                }
                else
                {
                    colNames = colNames + "," + colName;
                    colNamePramas = colNamePramas + ",:" + colName;
                }
            }
            sb.Append(")");
            if (colNames == string.Empty)
            {
                return "数据集的列数必须大于0";
            }
            using (OracleConnection objConn = new OracleConnection(orclConStr))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                sb.Remove(0, sb.Length);
                sb.Append(" insert into " + tbName + " (" + colNames + ") values(" + colNamePramas + " )");
                objCmd.CommandText = sb.ToString();
                OracleParameterCollection param = objCmd.Parameters;
                i = 0;
                while (i < cols)
                {
                    colType = dt.Columns[i].DataType.ToString();
                    colName = dt.Columns[i].ColumnName.ToString();
                    if (colType == "System.String")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.VarChar));
                    }
                    else if (colType == "System.DateTime")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.DateTime));
                    }
                    else if (colType == "System.Boolean")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Byte));
                    }
                    else if (colType == "System.Decimal")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Number));
                    }
                    else if (colType == "System.Double")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Double));
                    }
                    else if (colType == "System.Single")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Float));
                    }
                    else if (colType == "System.Single")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Float));
                    }
                    else
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Int32));
                    }
                    i++;
                }
                foreach (DataRow row in dt.Rows)
                {
                    for (i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }
                    try
                    {
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        objConn.Close();
                    }
                    catch (Exception wron)
                    {
                        string rec = row[1].ToString();
                        string str = returnMsg;
                        returnMsg = str + "数据:[" + rec + "]     " + this.GetExp(wron.Message);
                        objConn.Close();
                        continue;
                    }
                }
            }
            if (returnMsg == "")
            {
                returnMsg = returnMsg + "成功";
            }
            return returnMsg;
        }

        private string NetDataTypeToOracleType(string DataType)
        {
            if (DataType.ToString() == "System.String")
            {
                return "Varchar(255)";
            }
            if (DataType.ToString() == "System.Decimal")
            {
                return "Number";
            }
            if (DataType.ToString() == "System.DateTime")
            {
                return "Date";
            }
            if (DataType.ToString() == "System.Double")
            {
                return "Number";
            }
            return "int";
        }

        public int SaveOracleFile(string connStr, string tabName, DataTable outTable)
        {
            int i;
            int saveRes = -1;
            int rows = outTable.Rows.Count;
            int cols = outTable.Columns.Count;
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE " + tabName + " (");
            string colName = string.Empty;
            string colNames = string.Empty;
            string colNamePramas = string.Empty;
            string colType = string.Empty;
            for (i = 0; i < cols; i++)
            {
                colName = outTable.Columns[i].ColumnName.ToString();
                colType = outTable.Columns[i].DataType.ToString();
                colType = this.NetDataTypeToOracleType(colType);
                if (i == 0)
                {
                    sb.Append(colName + " " + colType);
                    colNames = colNames + colName;
                    colNamePramas = colNamePramas + ":" + colName;
                }
                else
                {
                    sb.Append("," + colName + " " + colType);
                    colNames = colNames + "," + colName;
                    colNamePramas = colNamePramas + ",:" + colName;
                }
            }
            sb.Append(")");
            if (colNames == string.Empty)
            {
                MessageBox.Show("数据集的列数必须大于0");
            }
            using (OracleConnection objConn = new OracleConnection(connStr))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = sb.ToString();
                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch
                {
                    return saveRes;
                }
                sb.Remove(0, sb.Length);
                sb.Append(" insert into " + tabName + " (" + colNames + ") values(" + colNamePramas + " )");
                objCmd.CommandText = sb.ToString();
                OracleParameterCollection param = objCmd.Parameters;
                i = 0;
                while (i < cols)
                {
                    colType = outTable.Columns[i].DataType.ToString();
                    colName = outTable.Columns[i].ColumnName.ToString();
                    if (colType == "System.String")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.VarChar));
                    }
                    else if (colType == "System.DateTime")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.DateTime));
                    }
                    else if (colType == "System.Boolean")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Byte));
                    }
                    else if (colType == "System.Decimal")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Number));
                    }
                    else if (colType == "System.Double")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Double));
                    }
                    else if (colType == "System.Single")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Float));
                    }
                    else if (colType == "System.Single")
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Float));
                    }
                    else
                    {
                        param.Add(new OracleParameter(":" + colName, OracleType.Int32));
                    }
                    i++;
                }
                foreach (DataRow row in outTable.Rows)
                {
                    for (i = 0; i < param.Count; i++)
                    {
                        param[i].Value = row[i];
                    }
                    try
                    {
                        objCmd.ExecuteNonQuery();
                    }
                    catch (Exception wron)
                    {
                        MessageBox.Show(wron.ToString(), "错误提示");
                        return saveRes;
                    }
                }
            }
            return 1;
        }

        public void synchDataSetWithDtSource(DataSet dsSynDs, string sqlStr, string srcTable)
        {
            this.getcon();
            new OleDbDataAdapter(sqlStr, this.My_con).Update(dsSynDs, srcTable);
            this.con_close();
        }

        public void synchDataTableWithDtSource(DataTable dtSynDt, string sqlStr)
        {
            this.getcon();
            new OleDbDataAdapter(sqlStr, this.My_con).Update(dtSynDt);
            this.con_close();
        }

        public string Db_Datasource
        {
            get
            {
                return this.db_Datasource;
            }
            set
            {
                this.db_Datasource = value;
            }
        }

        public string Db_IP
        {
            get
            {
                return this.db_IP;
            }
            set
            {
                this.db_IP = value;
            }
        }

        public string M_Str_Orclcon
        {
            get
            {
                return this.m_Str_Orclcon;
            }
            set
            {
                this.m_Str_Orclcon = value;
            }
        }

        public string PassWord
        {
            get
            {
                return this.passWord;
            }
            set
            {
                this.passWord = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}
