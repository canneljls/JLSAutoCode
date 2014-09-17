using JLSAutoCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JLSAutoCode
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SystemConfig.Init();

            FormMain form = new FormMain();
            form.ShowDialog();

            //int dbType = form.DBType;

            //if (dbType == 0)
            //{
            //    Application.Run(new FormSQLServer());
            //}
            //else if (dbType == 1)
            //{
            //    Application.Run(new FormMySQL());
            //}
            //else if (dbType == 2)
            //{
            //    //Application.Run(new FormOracle());
            //}
            //else if (dbType == 3)
            //{
            //    Application.Run(new FormHManager());
            //}
        }
    }
}
