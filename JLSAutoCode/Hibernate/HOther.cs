using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JLSAutoCode
{
    public class HOther
    {
        public static string ReadTemplate(string rootFolder,string templateName)
        {
            StreamReader objReader = new StreamReader(rootFolder + templateName);
            string sLine = "";
            string result = "";

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    result += sLine + Environment.NewLine;
            }
            objReader.Close();

            return result;
        }

        public static void SaveStringToFile(string str, string filePath)
        {
            using (StreamWriter tStreamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                tStreamWriter.WriteLine(str);
            }
        }

        /// <summary>
        /// 首字母小写处理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstWordLower(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string result;
                result = str.Substring(0, 1).ToLower();

                if (str.Length > 1)
                {
                    result += str.Substring(1);
                }

                return result;
            }

            return str;
        }

        /// <summary>
        /// 去掉实体类名的第一个下划线前的内容（例如AM_TABLE修改为TABLE）
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string RemoveSystemName(string tableName)
        {
            if (tableName.Contains("_"))
            {
                tableName = tableName.Substring(tableName.IndexOf("_") + 1);

                return tableName;
            }
            else
            {
                return tableName;
            }
        }
    }
}
