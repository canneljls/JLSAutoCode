using JLSAutoCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JLSAutoCode.Hibernate
{
    /// <summary>
    /// 默认
    /// 应对不同情况和不同类型数据库有两种方法，可同时使用，1：继承此类再修改，2：用不同模板
    /// </summary>
    public class HHelperDefault
    {
        /// <summary>
        /// 模板路径
        /// </summary>
        protected string m_TemplatePath = HConst.TemplatePath + SystemConfig.SystemConfigTag.TemplateName + "\\";

        public virtual void BuildCode(List<HField> m_Datas, HTable table, string rootFolder)
        {
            //实体类
            BuildEntity(m_Datas, table, rootFolder);
            //实体类
            BuildHBM(m_Datas, table, rootFolder);          

            MessageBox.Show("导出成功");
        }

        protected virtual void BuildEntity(List<HField> m_Datas, HTable table, string rootFolder)
        {
            string templateString = HOther.ReadTemplate(m_TemplatePath, "entity.cs");

            templateString = templateString.Replace("{||ClassName||}", table.ClassName);

            templateString = templateString.Replace("{||CnName||}", table.CHName);

            HField fieldPK = m_Datas[0];
            templateString = templateString.Replace("{||m_ID||}", "m_" + fieldPK.ENName);
            templateString = templateString.Replace("{||ID||}", fieldPK.ENName);

            StringBuilder sb = new StringBuilder();

            //FieldListPrivate
            foreach (HField field in m_Datas)
            {
                //protected decimal m_ID;

                //固定第一个字段是主键
                if (field == m_Datas[0])
                {
                    sb.Append("private " + " decimal m_" + field.ENName + ";" + Environment.NewLine);
                }
                else
                {
                    string f1 = GetCSType(field);
                    sb.Append("private " + f1 + " m_" + field.ENName + ";" + Environment.NewLine);
                }
            }

            templateString = templateString.Replace("{||FieldListPrivate||}", sb.ToString());


            //FieldListInit
            sb = new StringBuilder();
            foreach (HField field in m_Datas)
            {
                //m_DATASET_ID = null;

                //固定第一个字段是主键
                if (field == m_Datas[0])
                {
                    sb.Append("m_" + field.ENName + "=0;" + Environment.NewLine);
                }
                else
                {
                    sb.Append("m_" + field.ENName + "=null;" + Environment.NewLine);
                }
            }

            templateString = templateString.Replace("{||FieldListInit||}", sb.ToString());

            //FieldListPublic
            sb = new StringBuilder();
            foreach (HField field in m_Datas)
            {
                sb.Append(@"/// <summary>" + Environment.NewLine);
                sb.Append(@"/// " + field.CHName + Environment.NewLine);
                sb.Append(@"/// </summary>	" + Environment.NewLine);

                //固定第一个字段是主键
                if (field == m_Datas[0])
                {
                    sb.Append(@"public virtual decimal " + field.ENName + Environment.NewLine);
                    sb.Append(@"{" + Environment.NewLine);
                    sb.Append(@"get { return m_" + field.ENName + "; }" + Environment.NewLine);
                    sb.Append(@"set { m_IsChanged |= (m_" + field.ENName + " != value); m_" + field.ENName + " = value; }" + Environment.NewLine);
                    sb.Append(@"}" + Environment.NewLine);
                }
                else
                {
                    if (field.Type == HFieldType.NVARCHAR2)
                    {
                        sb.Append(@"public virtual string " + field.ENName + Environment.NewLine);
                        sb.Append(@"{" + Environment.NewLine);
                        sb.Append(@"get { return m_" + field.ENName + "; }" + Environment.NewLine);
                        sb.Append(@"set	" + Environment.NewLine);
                        sb.Append(@"{" + Environment.NewLine);
                        sb.Append(@"if ( value != null)" + Environment.NewLine);
                        if (field.Length > 0)
                        {
                            sb.Append(@"if( value.Length > " + field.Length + ")" + Environment.NewLine);
                            sb.Append("throw new ArgumentOutOfRangeException(\"Invalid value for " + field.ENName + "\", value, value.ToString());" + Environment.NewLine);
                        }
                        sb.Append(@"m_IsChanged |= (m_" + field.ENName + " != value); m_" + field.ENName + " = value;" + Environment.NewLine);
                        sb.Append(@"}" + Environment.NewLine);
                        sb.Append(@"}" + Environment.NewLine);
                    }
                    else
                    {
                        string f1 = GetCSType(field);
                        sb.Append(@"public virtual " + f1 + " " + field.ENName + Environment.NewLine);
                        sb.Append(@"{" + Environment.NewLine);
                        sb.Append(@"get { return m_" + field.ENName + "; }" + Environment.NewLine);
                        sb.Append(@"set { m_IsChanged |= (m_" + field.ENName + " != value); m_" + field.ENName + " = value; }" + Environment.NewLine);
                        sb.Append(@"}" + Environment.NewLine);
                    }
                }
            }

            templateString = templateString.Replace("{||FieldListPublic||}", sb.ToString());

            HOther.SaveStringToFile(templateString, rootFolder + "\\" + table.ClassName + ".cs");
        }

        protected virtual void BuildHBM(List<HField> m_Datas, HTable table, string rootFolder)
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
                    sb.Append("<generator class=\"increment\" />" + Environment.NewLine);
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

        protected virtual string GetCSType(HField field)
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
            else if (field.Type == HFieldType.DATE)
            {
                f1 = "DateTime? ";
            }

            return f1;
        }

        protected virtual string GetHBMCSType(HField field)
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
            else if (field.Type == HFieldType.DATE)
            {
                f1 = "DateTime";
            }

            return f1;
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        protected static string GetRandomNumber(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
