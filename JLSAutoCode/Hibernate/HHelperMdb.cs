using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode.Hibernate
{
    /// <summary>
    /// 生成代码类（Mdb）
    /// </summary>
    public class HHelperMdb : HHelperDefault
    {
        public override void BuildCode(List<HField> m_Datas, HTable table, string rootFolder)
        {
            //实体类
            BuildEntity(m_Datas, table, rootFolder);
            //实体类
            BuildHBM(m_Datas, table, rootFolder);
            //Detail窗体代码
            BuildFormCRUDCode(m_Datas, table, rootFolder);

            MessageBox.Show("导出成功");
        }

        protected override void BuildHBM(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(m_TemplatePath, "hbm.xml");

            templateString = templateString.Replace("{||ClassName||}", table.ClassName);

            templateString = templateString.Replace("{||TableName||}", table.ENName);

            StringBuilder sb = new StringBuilder();

            //FieldList
            foreach (HField field in m_Datas)
            {
                //private decimal m_ID;

                //固定第一个字段是主键
                if (field == m_Datas[0])
                {
                    sb.Append("<id name=\"" + field.ENName + "\" column=\"" + field.ENName + "\" type=\"Decimal\" unsaved-value=\"0\">" + Environment.NewLine);
                    sb.Append("<generator class=\"identity\" />" + Environment.NewLine);
                    sb.Append("</id>" + Environment.NewLine);
                }
                else
                {
                    string f1 = GetHBMCSType(field);
                    sb.Append("<property column=\"" + field.ENName + "\" type=\"" + f1 + "\" name=\"" + field.ENName + "\"  />" + Environment.NewLine);
                }
            }

            templateString = templateString.Replace("{||FieldList||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + ".hbm.xml");
        }

        private void BuildFormCRUDCode(List<HField> m_Datas, HTable table, string rootFolder)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Detail窗体，Form_Load代码" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            //txtFBFBM.Text = tFBF.FBFBM;
            foreach (HField field in m_Datas)
            {
                sb.Append("txt" + field.ENName + ".Text = t" + HOther.RemoveSystemName(table.ClassName) + "." + field.ENName + ";" + Environment.NewLine);
            }

            sb.Append("" + Environment.NewLine);
            sb.Append("Detail窗体，保存实体代码" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);

            //tFBF.FBFBM = txtFBFBM.Text;
            foreach (HField field in m_Datas)
            {
                sb.Append("t" + HOther.RemoveSystemName(table.ClassName) + "." + field.ENName + "=txt" + field.ENName + ".Text;" + Environment.NewLine);
            }
            sb.Append("" + Environment.NewLine);

            HOther.SaveStringToFile(sb.ToString(), rootFolder + "\\" + table.ClassName + "Detail窗体代码，复制使用.txt");
        }

        protected override string GetCSType(HField field)
        {
            string f1 = "";
            if (field.Type == HFieldType.NVARCHAR2 || field.Type == HFieldType.CLOB)
            {
                f1 = "string ";
            }
            else if (field.Type == HFieldType.NUMBER)
            {
                //通过长度是否大于0，判断是整数还是小数
                if (field.Length > 0)
                {
                    f1 = "int? ";
                }
                else
                {
                    f1 = "decimal? ";
                }
            }
            else if (field.Type == HFieldType.FLOAT)
            {
                f1 = "decimal? ";
            }
            else if (field.Type == HFieldType.INT)
            {
                f1 = "int? ";
            }
            else if (field.Type == HFieldType.DATE)
            {
                f1 = "DateTime? ";
            }
            else if (field.Type == HFieldType.BINARY)
            {
                f1 = "Byte[] ";
            }

            return f1;
        }

        protected override string GetHBMCSType(HField field)
        {
            string f1 = "";
            if (field.Type == HFieldType.NVARCHAR2 || field.Type == HFieldType.CLOB)
            {
                f1 = "String";
            }
            else if (field.Type == HFieldType.NUMBER)
            {
                //通过长度是否大于0，判断是整数还是小数
                if (field.Length > 0)
                {
                    f1 = "int";
                }
                else
                {
                    f1 = "Decimal";
                }
            }
            else if (field.Type == HFieldType.FLOAT)
            {
                f1 = "Decimal";
            }
            else if (field.Type == HFieldType.INT)
            {
                f1 = "int";
            }
            else if (field.Type == HFieldType.DATE)
            {
                f1 = "DateTime";
            }
            else if (field.Type == HFieldType.BINARY)
            {
                f1 = "BinaryBlob";
            }

            return f1;
        }
    }
}
